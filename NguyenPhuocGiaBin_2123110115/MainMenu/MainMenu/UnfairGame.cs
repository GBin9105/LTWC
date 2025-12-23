using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class UnfairGame : Form
    {
        const int MAP_OFFSET_Y = 60;
        const int STAIR_EXTRA_UP = 40;

        PictureBox player, goal;

        List<PictureBox> realGrounds = new();
        List<PictureBox> fakeGrounds = new();
        List<PictureBox> randomFakeGrounds = new();
        List<Hazard> hazards = new();

        List<Rectangle> fakeSpawnZones = new();
        Random rng = new Random();

        System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer hazardTimer = new System.Windows.Forms.Timer();

        int vx, vy;
        int gravity = 4;
        int jumpPower = -28;
        int prevPlayerBottom;
        int prevPlayerLeft;

        bool left, right, jumping;
        int deathCount = 0;
        bool isEnding = false;

        class Hazard
        {
            public PictureBox Box;
            public bool IsFalling;
            public int SpawnTime;
        }

        public UnfairGame()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            DoubleBuffered = true;

            BackgroundImage = Properties.Resources.bg;
            BackgroundImageLayout = ImageLayout.Stretch;

            SoundManager.PlayBackground("bg.wav");

            SetupLevel();

            gameTimer.Interval = 16;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            hazardTimer.Interval = 3000;
            hazardTimer.Tick += (s, e) => SpawnHazards();
            hazardTimer.Start();
        }

        void EndGame(bool isWin)
        {
            if (isEnding) return;
            isEnding = true;

            gameTimer.Stop();
            hazardTimer.Stop();
            SoundManager.StopBackground();

            foreach (var h in hazards)
            {
                Controls.Remove(h.Box);
                h.Box.Dispose();
            }
            hazards.Clear();

            if (isWin)
            {
                SoundManager.PlayEffect("win.wav");
                MessageBox.Show($"You won.\nDeaths: {deathCount}", "Finally.");
            }

            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            gameTimer.Stop();
            hazardTimer.Stop();
            SoundManager.StopBackground();
            base.OnFormClosing(e);
        }

        void SetupLevel()
        {
            Controls.Clear();
            realGrounds.Clear();
            fakeGrounds.Clear();
            randomFakeGrounds.Clear();
            hazards.Clear();
            fakeSpawnZones.Clear();

            player = new PictureBox
            {
                Size = new Size(30, 30),
                BackColor = Color.White,
                Location = new Point(80, 420 + MAP_OFFSET_Y)
            };

            goal = new PictureBox
            {
                Size = new Size(50, 60),
                Image = Properties.Resources.door,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Location = new Point(1780, 150 + MAP_OFFSET_Y)
            };

            Controls.Add(player);
            Controls.Add(goal);

            AddRealGround(0, 520);
            AddFakeGround(180, 520, 90);
            AddRealGround(270, 520, 140);
            AddFakeGround(410, 520, 110);
            AddRealGround(520, 520, 180);
            AddFakeGround(700, 520, 120);
            AddRealGround(820, 520, 260);

            AddBlock(350, 425);
            AddBlock(640, 425);
            AddBlock(940, 425);

            AddRealGround(220, 470, 60);
            AddRealGround(280, 420, 60);
            AddRealGround(340, 370, 60);

            AddRealGround(420, 260, 160);
            AddFakeGround(580, 260, 100);
            AddRealGround(680, 260, 160);
            AddFakeGround(840, 260, 120);
            AddRealGround(960, 260, 220);

            AddBlock(560, 165);
            AddBlock(820, 165);

            AddFakeGround(1180, 260, 120);
            AddRealGround(1300, 260, 180);
            AddFakeGround(1480, 260, 120);
            AddBlock(1380, 165);

            CreateInvisibleStairs();
            CreateFakeSpawnZones();
            SpawnRandomFakeGrounds();
        }

        void CreateFakeSpawnZones()
        {
            foreach (var g in realGrounds)
            {
                if (!g.Visible) continue;
                if (g.Height != 40) continue;

                fakeSpawnZones.Add(new Rectangle(g.Left, g.Top - 40, g.Width, 40));
            }
        }

        void SpawnRandomFakeGrounds()
        {
            if (fakeSpawnZones.Count == 0) return;

            const int TARGET = 4;
            int attempts = 0;

            while (randomFakeGrounds.Count < TARGET && attempts < 150)
            {
                attempts++;
                var zone = fakeSpawnZones[rng.Next(fakeSpawnZones.Count)];
                int width = rng.Next(60, 120);

                var rect = new Rectangle(
                    zone.Left + rng.Next(Math.Max(1, zone.Width - width)),
                    zone.Top,
                    width,
                    40
                );

                bool overlap =
                    fakeGrounds.Exists(f => rect.IntersectsWith(f.Bounds)) ||
                    randomFakeGrounds.Exists(f => rect.IntersectsWith(f.Bounds));

                if (overlap) continue;

                var fake = new PictureBox
                {
                    Bounds = rect,
                    BackgroundImage = Properties.Resources.ground_tiles,
                    BackgroundImageLayout = ImageLayout.Tile
                };

                randomFakeGrounds.Add(fake);
                Controls.Add(fake);
            }
        }

        void CreateInvisibleStairs()
        {
            int leftX = 1620;
            int rightX = 1760;
            int startY = 520;
            int gapY = 60;

            for (int i = 0; i < 4; i++)
            {
                int x = (i % 2 == 0) ? leftX : rightX;
                int y = startY - i * gapY + MAP_OFFSET_Y - STAIR_EXTRA_UP;

                var step = new PictureBox
                {
                    Size = new Size(70, 18),
                    Location = new Point(x, y),
                    Visible = false
                };

                realGrounds.Add(step);
                Controls.Add(step);
            }
        }

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
            SoundManager.PlayEffect("hand.wav");

            hazards.Add(new Hazard
            {
                Box = box,
                IsFalling = true,
                SpawnTime = Environment.TickCount
            });
        }

        void GameLoop(object sender, EventArgs e)
        {
            prevPlayerBottom = player.Bottom;
            prevPlayerLeft = player.Left;

            vx = left ? -6 : right ? 6 : 0;
            player.Left += vx;

            foreach (var g in realGrounds)
            {
                if (!player.Bounds.IntersectsWith(g.Bounds)) continue;

                if (vx > 0 && prevPlayerLeft + player.Width <= g.Left)
                    player.Left = g.Left - player.Width;
                else if (vx < 0 && prevPlayerLeft >= g.Right)
                    player.Left = g.Right;
            }

            player.Top += vy;
            vy += gravity;

            foreach (var g in realGrounds)
            {
                if (!player.Bounds.IntersectsWith(g.Bounds)) continue;

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

                if (vy >= 0 && prevPlayerBottom <= g.Top)
                {
                    player.Top = g.Top - player.Height;
                    vy = 0;
                    jumping = false;
                }
                else if (vy < 0 && player.Top >= g.Bottom)
                {
                    player.Top = g.Bottom;
                    vy = 0;
                }
            }

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

            if (player.Bounds.IntersectsWith(goal.Bounds))
            {
                GoToLevel2();
                return;
            }
        }

        void GoToLevel2()
        {
            gameTimer.Stop();
            hazardTimer.Stop();
            SoundManager.StopBackground();

            var lv2 = new UnfairGameLv2();
            lv2.Show();

            this.Close();
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
            player.Location = new Point(80, 420 + MAP_OFFSET_Y);

            foreach (var h in hazards)
            {
                Controls.Remove(h.Box);
                h.Box.Dispose();
            }
            hazards.Clear();

            gameTimer.Start();
            hazardTimer.Start();
        }

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
                EndGame(false);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) left = false;
            if (e.KeyCode == Keys.D) right = false;
        }

        void AddRealGround(int x, int y, int w = 180)
        {
            var g = new PictureBox
            {
                Size = new Size(w, 40),
                Location = new Point(x, y + MAP_OFFSET_Y),
                BackgroundImage = Properties.Resources.ground_tiles,
                BackgroundImageLayout = ImageLayout.Tile
            };
            realGrounds.Add(g);
            Controls.Add(g);
        }

        void AddFakeGround(int x, int y, int w)
        {
            var g = new PictureBox
            {
                Size = new Size(w, 40),
                Location = new Point(x, y + MAP_OFFSET_Y),
                BackgroundImage = Properties.Resources.ground_tiles,
                BackgroundImageLayout = ImageLayout.Tile
            };
            fakeGrounds.Add(g);
            Controls.Add(g);
        }

        void AddBlock(int x, int y)
        {
            var b = new PictureBox
            {
                Size = new Size(40, 95),
                Location = new Point(x, y + MAP_OFFSET_Y),
                BackColor = Color.DimGray
            };
            realGrounds.Add(b);
            Controls.Add(b);
        }

        private void UnfairGame_Load(object sender, EventArgs e)
        {

        }
    }
}
