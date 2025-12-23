using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class UnfairGameLv2 : Form
    {
        const int MAP_OFFSET_Y = 60;

        // Map tile-grid
        const int GRID_W = 37;
        const int GRID_H = 21;

        // Doors + spawn (tile coords)
        const int FAKE_DOOR_TX = 25, FAKE_DOOR_TY = 3;
        const int REAL_DOOR_TX = 32, REAL_DOOR_TY = 17;

        const int SPAWN_TX = 1, SPAWN_TY = 14;

        // ✅ Reduce hidden random pits to 2
        const int RANDOM_PITS_TARGET = 2;

        PictureBox player, goal, fakeGoal;

        List<PictureBox> realGrounds = new();
        List<PictureBox> fakeGrounds = new();          // fake pits: display or hide, non-solid
        List<PictureBox> randomFakeGrounds = new();    // random pits (spawn on top of real ground)
        List<Rectangle> fakeSpawnZones = new();

        List<Hazard> hazards = new();
        Random rng = new Random();

        System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer hazardTimer = new System.Windows.Forms.Timer();

        // Delay timer for background music (avoid Lv1 stop)
        System.Windows.Forms.Timer bgDelayTimer = new System.Windows.Forms.Timer();

        int vx, vy;
        int gravity = 4;
        int jumpPower = -28;

        int prevPlayerBottom;
        int prevPlayerLeft;
        int prevPlayerTop; // ✅ used to fix upward collision (head bump)

        bool left, right, jumping;
        int deathCount = 0;
        bool isEnding = false;

        int tileSize;
        int mapOriginX;
        int mapOriginY;

        bool _initialized = false;

        class Hazard
        {
            public PictureBox Box;
            public int SpawnTime;
        }

        // ===== Map data (tile units) =====
        static readonly (int x, int y, int w, int h)[] GREEN = new[]
        {
            (10, 4, 1, 1),
            (12, 4, 5, 1),
            (18, 4, 5, 1),
            (24, 4, 2, 1),

            (9, 8, 2, 1),
            (12, 8, 1, 1),

            (12, 9, 2, 1),
            (12,10, 3, 1),
            (12,11, 6, 1),
            (19,11, 3, 1),

            (0,14, 5, 1),
            (6,14, 3, 1),

            // left vertical green column (L-shape)
            (8,15, 1, 5),

            // right platform near real door
            (27,18, 3, 1),
            (31,18, 2, 1),
        };

        // Red = fake pits (look like ground but non-solid)
        static readonly (int x, int y, int w, int h)[] RED = new[]
        {
            (11, 4, 1, 1),
            (17, 4, 1, 1),
            (23, 4, 1, 1),

            (11, 8, 1, 1),

            (18,11, 1, 1),

            (5,14, 1, 1),

            (30,18, 1, 1),

            // bottom hidden path pits (per sketch) -> should be hidden
            (11,20, 1, 1),
            (15,20, 1, 1),
        };

        // Yellow = hidden solids (Visible=false)
        static readonly (int x, int y, int w, int h)[] YELLOW = new[]
        {
            (8, 0, 1, 6),
            (9, 6, 1, 2),

            (25,11, 1, 2),
            (23,13, 3, 1),
            (25,14, 1, 3),
            (20,15, 2, 1),
            (23,17, 3, 1),
            (25,18, 1, 3),

            (13,18, 1, 2),
            (18,18, 1, 2),

            (20,19, 2, 1),

            (8,20, 3, 1),
            (12,20, 3, 1),
            (16,20, 4, 1),
        };

        // ✅ GRAY obstacles updated to match sketch (image 2)
        // (These are "block" solids)
        static readonly (int x, int y, int w, int h)[] GRAY = new[]
        {
            // Top cluster
            (13, 2, 1, 2),  // left vertical pillar (touches platform y=4)
            (16, 2, 3, 1),  // horizontal bar
            (21, 1, 1, 2),  // right vertical pillar (floating, as sketch)

            // Middle pillar
            (19, 9, 1, 2),

            // Bottom-left pillar
            (7, 12, 1, 2),
        };

        public UnfairGameLv2()
        {
            InitializeComponent();

            this.Load += UnfairGameLv2_Load;
            this.Shown += UnfairGameLv2_Shown;

            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            DoubleBuffered = true;

            BackgroundImage = Properties.Resources.bg;
            BackgroundImageLayout = ImageLayout.Stretch;

            gameTimer.Interval = 16;
            gameTimer.Tick += GameLoop;

            hazardTimer.Interval = 3000;
            hazardTimer.Tick += (s, e) => SpawnHazards();
        }

        private void UnfairGameLv2_Load(object sender, EventArgs e)
        {
            if (_initialized) return;
            _initialized = true;

            SetupLevel2();

            gameTimer.Start();
            hazardTimer.Start();
        }

        // Play music in Shown + delay to avoid Lv1 stop killing it
        private void UnfairGameLv2_Shown(object sender, EventArgs e)
        {
            bgDelayTimer.Stop();
            bgDelayTimer.Interval = 350;
            bgDelayTimer.Tick -= BgDelayTimer_Tick;
            bgDelayTimer.Tick += BgDelayTimer_Tick;
            bgDelayTimer.Start();
        }

        private void BgDelayTimer_Tick(object sender, EventArgs e)
        {
            bgDelayTimer.Stop();
            SoundManager.PlayBackground("bg.wav");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            gameTimer.Stop();
            hazardTimer.Stop();
            bgDelayTimer.Stop();
            SoundManager.StopBackground();
            base.OnFormClosing(e);
        }

        // ================= SETUP =================

        void SetupLevel2()
        {
            Controls.Clear();

            realGrounds.Clear();
            fakeGrounds.Clear();
            randomFakeGrounds.Clear();
            fakeSpawnZones.Clear();
            hazards.Clear();

            ComputeTileAndOrigin();

            // --- Build map first ---
            foreach (var r in GREEN) AddRealRect(r.x, r.y, r.w, r.h);
            foreach (var r in YELLOW) AddHiddenRect(r.x, r.y, r.w, r.h);
            foreach (var r in GRAY) AddBlockRect(r.x, r.y, r.w, r.h);

            // Fake pits (RED)
            foreach (var r in RED)
            {
                // bottom hidden path pits should be invisible (do not render)
                bool visible = (r.y != 20);
                AddFakeRect(r.x, r.y, r.w, r.h, visible);
            }

            // --- Doors (create before random pits so pits can avoid doors) ---
            goal = new PictureBox
            {
                Size = new Size(50, 60),
                Image = Properties.Resources.door,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            Controls.Add(goal);
            PlaceDoor(goal, REAL_DOOR_TX, REAL_DOOR_TY);

            fakeGoal = new PictureBox
            {
                Size = new Size(50, 60),
                Image = Properties.Resources.door,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            Controls.Add(fakeGoal);
            PlaceDoor(fakeGoal, FAKE_DOOR_TX, FAKE_DOOR_TY);

            // --- Random fake pits zones ---
            CreateFakeSpawnZones();
            SpawnRandomFakeGrounds();

            // --- Player (place AFTER pits so spawn can’t instantly drop) ---
            player = new PictureBox
            {
                Size = new Size(30, 30),
                BackColor = Color.White
            };
            Controls.Add(player);
            PlacePlayerAtSpawn();

            // ✅ Enforce layering:
            // Door + hand UNDER ground + obstacles
            ApplyZOrder();
        }

        void ComputeTileAndOrigin()
        {
            int maxTileW = (ClientSize.Width - 40) / GRID_W;
            int maxTileH = (ClientSize.Height - MAP_OFFSET_Y - 40) / GRID_H;

            tileSize = Math.Min(40, Math.Min(maxTileW, maxTileH));
            tileSize = Math.Max(24, tileSize);

            mapOriginX = (ClientSize.Width - (GRID_W * tileSize)) / 2;
            mapOriginY = MAP_OFFSET_Y;
        }

        void PlacePlayerAtSpawn()
        {
            int px = mapOriginX + SPAWN_TX * tileSize + 6;
            int py = mapOriginY + SPAWN_TY * tileSize - player.Height;
            player.Location = new Point(px, py);

            vy = 0;
            jumping = false;
        }

        void PlaceDoor(PictureBox door, int tx, int ty)
        {
            int baseX = mapOriginX + tx * tileSize + (tileSize - door.Width) / 2;
            int baseYBottom = mapOriginY + (ty + 1) * tileSize;
            door.Location = new Point(baseX, baseYBottom - door.Height);
        }

        // ✅ Spawn-safe zone: pits can NEVER spawn here
        Rectangle GetSpawnSafeRect()
        {
            int safeLeftTile = Math.Max(0, SPAWN_TX - 1);
            return new Rectangle(
                mapOriginX + safeLeftTile * tileSize,
                mapOriginY + SPAWN_TY * tileSize,
                tileSize * 3,
                tileSize
            );
        }

        // ✅ Centralized Z-order:
        // Door + hand UNDER ground + obstacles
        void ApplyZOrder()
        {
            // BACK: doors + hands
            if (goal != null) goal.SendToBack();
            if (fakeGoal != null) fakeGoal.SendToBack();

            foreach (var h in hazards)
                h.Box.SendToBack();

            // MID: real solids (ground + blocks + hidden solids)
            foreach (var g in realGrounds)
                g.BringToFront();

            // MID/TOP: fake visuals (pits)
            foreach (var f in fakeGrounds)
                f.BringToFront();
            foreach (var r in randomFakeGrounds)
                r.BringToFront();

            // TOP: player
            player?.BringToFront();
        }

        // ================= GAME LOOP =================

        void GameLoop(object sender, EventArgs e)
        {
            prevPlayerBottom = player.Bottom;
            prevPlayerLeft = player.Left;
            prevPlayerTop = player.Top;

            vx = left ? -6 : right ? 6 : 0;
            player.Left += vx;

            // Horizontal collision
            foreach (var g in realGrounds)
            {
                if (!player.Bounds.IntersectsWith(g.Bounds)) continue;

                if (vx > 0 && prevPlayerLeft + player.Width <= g.Left)
                    player.Left = g.Left - player.Width;
                else if (vx < 0 && prevPlayerLeft >= g.Right)
                    player.Left = g.Right;
            }

            // Gravity
            player.Top += vy;
            vy += gravity;

            // Vertical collision
            foreach (var g in realGrounds)
            {
                if (!player.Bounds.IntersectsWith(g.Bounds)) continue;

                // Skip collision if we are inside a random pit (hole)
                bool onRandomFake = false;
                foreach (var f in randomFakeGrounds)
                {
                    if (player.Bounds.IntersectsWith(f.Bounds))
                    {
                        onRandomFake = true;
                        break;
                    }
                }
                if (onRandomFake) continue;

                // Landing
                if (vy >= 0 && prevPlayerBottom <= g.Top)
                {
                    player.Top = g.Top - player.Height;
                    vy = 0;
                    jumping = false;
                }
                // ✅ Head bump (fixed)
                else if (vy < 0 && prevPlayerTop >= g.Bottom)
                {
                    player.Top = g.Bottom;
                    vy = 0;
                }
            }

            // Hazards (hand)
            for (int i = hazards.Count - 1; i >= 0; i--)
            {
                var h = hazards[i];
                h.Box.Top += 14;

                if (player.Bounds.IntersectsWith(h.Box.Bounds))
                {
                    Die();
                    return;
                }

                if (Environment.TickCount - h.SpawnTime >= 2000)
                {
                    Controls.Remove(h.Box);
                    h.Box.Dispose();
                    hazards.RemoveAt(i);
                }
            }

            if (player.Top > Height)
            {
                Die();
                return;
            }

            if (player.Bounds.IntersectsWith(fakeGoal.Bounds))
            {
                Die();
                return;
            }

            if (player.Bounds.IntersectsWith(goal.Bounds))
            {
                Win();
                return;
            }
        }

        // ================= STATE =================

        void Win()
        {
            if (isEnding) return;
            isEnding = true;

            gameTimer.Stop();
            hazardTimer.Stop();

            SoundManager.PlayEffect("win.wav");
            MessageBox.Show($"YOU WIN LEVEL 2\nDeaths: {deathCount}", "Finally.");
            Close();
        }

        void Die()
        {
            deathCount++;
            SoundManager.PlayEffect("die.wav");

            gameTimer.Stop();
            hazardTimer.Stop();

            foreach (var f in randomFakeGrounds)
                Controls.Remove(f);
            randomFakeGrounds.Clear();

            SpawnRandomFakeGrounds();
            PlacePlayerAtSpawn();

            foreach (var h in hazards)
            {
                Controls.Remove(h.Box);
                h.Box.Dispose();
            }
            hazards.Clear();

            // ✅ Re-apply correct layering (door + hands under ground/obstacles)
            ApplyZOrder();

            gameTimer.Start();
            hazardTimer.Start();
        }

        // ================= HAND =================

        void SpawnHazards()
        {
            int px = player.Left + player.Width / 2;
            SpawnFallingHand(px);
        }

        void SpawnFallingHand(int x)
        {
            var box = new PictureBox
            {
                Size = new Size(120, 190),
                Image = Properties.Resources.hand,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Location = new Point(x - 60, -200)
            };

            Controls.Add(box);

            hazards.Add(new Hazard
            {
                Box = box,
                SpawnTime = Environment.TickCount
            });

            SoundManager.PlayEffect("hand.wav");

            // ✅ Keep door + hand under ground + obstacles
            ApplyZOrder();
        }

        // ================= RANDOM FAKE PITS =================

        void CreateFakeSpawnZones()
        {
            fakeSpawnZones.Clear();

            int minWidth = tileSize * 4; // >= 4 tiles
            Rectangle spawnSafe = GetSpawnSafeRect();

            foreach (var g in realGrounds)
            {
                if (!g.Visible) continue;
                if ((g.Tag as string) != "ground") continue;

                // only 1 tile thick platforms
                if (g.Height != tileSize) continue;

                // avoid short steps
                if (g.Width < minWidth) continue;

                // ✅ never spawn random pits on the spawn platform
                if (g.Bounds.IntersectsWith(spawnSafe)) continue;

                fakeSpawnZones.Add(new Rectangle(g.Left, g.Top, g.Width, tileSize));
            }
        }

        void SpawnRandomFakeGrounds()
        {
            if (fakeSpawnZones.Count == 0) return;

            Rectangle spawnSafe = GetSpawnSafeRect();

            int attempts = 0;

            while (randomFakeGrounds.Count < RANDOM_PITS_TARGET && attempts < 250)
            {
                attempts++;

                var zone = fakeSpawnZones[rng.Next(fakeSpawnZones.Count)];

                // snap to tiles
                int tilesWide = zone.Width / tileSize;
                if (tilesWide < 5) continue;

                int maxPitTiles = Math.Min(3, tilesWide - 2);
                if (maxPitTiles < 1) continue;

                int pitTiles = rng.Next(1, maxPitTiles + 1);

                int startMin = 1;
                int startMax = tilesWide - pitTiles - 1;
                if (startMax < startMin) continue;

                int startTile = rng.Next(startMin, startMax + 1);

                var rect = new Rectangle(
                    zone.Left + startTile * tileSize,
                    zone.Top,
                    pitTiles * tileSize,
                    tileSize
                );

                // ✅ never spawn a pit where the player spawns
                if (rect.IntersectsWith(spawnSafe)) continue;

                // don't overlap existing pits
                bool overlap =
                    fakeGrounds.Exists(f => rect.IntersectsWith(f.Bounds)) ||
                    randomFakeGrounds.Exists(f => rect.IntersectsWith(f.Bounds));

                if (overlap) continue;

                // don't overlap obstacles
                bool hitBlock = realGrounds.Exists(b => (b.Tag as string) == "block" && rect.IntersectsWith(b.Bounds));
                if (hitBlock) continue;

                // don't overlap doors
                if (goal != null && rect.IntersectsWith(goal.Bounds)) continue;
                if (fakeGoal != null && rect.IntersectsWith(fakeGoal.Bounds)) continue;

                var fake = new PictureBox
                {
                    Bounds = rect,
                    BackgroundImage = Properties.Resources.ground_tiles,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Tag = "randompit"
                };

                randomFakeGrounds.Add(fake);
                Controls.Add(fake);
            }

            // ✅ Put pits above ground but below player, and doors/hands under ground
            ApplyZOrder();
        }

        // ================= INPUT =================

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) left = true;
            if (e.KeyCode == Keys.D) right = true;

            if (e.KeyCode == Keys.Enter && !jumping)
            {
                vy = jumpPower;
                jumping = true;
                SoundManager.PlayEffect("jump.wav");
            }

            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) left = false;
            if (e.KeyCode == Keys.D) right = false;
        }

        // ================= HELPERS =================

        void AddRealRect(int tx, int ty, int tw, int th)
        {
            var g = new PictureBox
            {
                Size = new Size(tw * tileSize, th * tileSize),
                Location = new Point(mapOriginX + tx * tileSize, mapOriginY + ty * tileSize),
                BackgroundImage = Properties.Resources.ground_tiles,
                BackgroundImageLayout = ImageLayout.Tile,
                Tag = "ground"
            };
            realGrounds.Add(g);
            Controls.Add(g);
        }

        void AddFakeRect(int tx, int ty, int tw, int th, bool visible)
        {
            var g = new PictureBox
            {
                Size = new Size(tw * tileSize, th * tileSize),
                Location = new Point(mapOriginX + tx * tileSize, mapOriginY + ty * tileSize),
                BackgroundImage = Properties.Resources.ground_tiles,
                BackgroundImageLayout = ImageLayout.Tile,
                Visible = visible,
                Tag = "fakepit"
            };
            fakeGrounds.Add(g);
            Controls.Add(g);
        }

        void AddHiddenRect(int tx, int ty, int tw, int th)
        {
            var g = new PictureBox
            {
                Size = new Size(tw * tileSize, th * tileSize),
                Location = new Point(mapOriginX + tx * tileSize, mapOriginY + ty * tileSize),
                Visible = false,
                Tag = "hidden"
            };
            realGrounds.Add(g);
            Controls.Add(g);
        }

        void AddBlockRect(int tx, int ty, int tw, int th)
        {
            var b = new PictureBox
            {
                Size = new Size(tw * tileSize, th * tileSize),
                Location = new Point(mapOriginX + tx * tileSize, mapOriginY + ty * tileSize),
                BackColor = Color.DimGray,
                Tag = "block"
            };
            realGrounds.Add(b);
            Controls.Add(b);
        }
    }
}
