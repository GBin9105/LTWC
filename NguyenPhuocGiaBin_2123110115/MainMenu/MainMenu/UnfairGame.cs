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
        List<Hazard> hazards = new();


        System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer hazardTimer = new System.Windows.Forms.Timer();

        int vx, vy;
        int gravity = 2;
        int jumpPower = -18;

        bool left, right, jumping;
        int deathCount = 0;

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

            // 🎨 BACKGROUND
            BackgroundImage = Properties.Resources.bg;
            BackgroundImageLayout = ImageLayout.Stretch;

            SetupLevel();

            gameTimer.Interval = 16;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            hazardTimer.Interval = 3000;
            hazardTimer.Tick += (s, e) => SpawnHazards();
            hazardTimer.Start();
        }

        // ================= LEVEL =================
        void SetupLevel()
        {
            Controls.Clear();
            realGrounds.Clear();
            fakeGrounds.Clear();
            hazards.Clear();

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

            // ===== TẦNG DƯỚI =====
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

            // ===== LÊN TẦNG TRÊN =====
            AddRealGround(220, 470, 60);
            AddRealGround(280, 420, 60);
            AddRealGround(340, 370, 60);

            // ===== TẦNG TRÊN =====
            AddRealGround(420, 260, 160);
            AddFakeGround(580, 260, 100);
            AddRealGround(680, 260, 160);
            AddFakeGround(840, 260, 120);
            AddRealGround(960, 260, 220);

            AddBlock(560, 165);
            AddBlock(820, 165);

            // ===== ĐOẠN SAU =====
            AddFakeGround(1180, 260, 120);
            AddRealGround(1300, 260, 180);
            AddFakeGround(1480, 260, 120);
            AddBlock(1380, 165);

            CreateInvisibleStairs();
        }

        // ================= STAIRS =================
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

        // ================= HAZARDS =================
        void SpawnHazards()
        {
            int px = player.Left + player.Width / 2;
            SpawnSpikeUp(px);
            SpawnFallingHand(px);
        }

        void SpawnSpikeUp(int x)
        {
            var box = new PictureBox
            {
                Size = new Size(30, 60),
                BackColor = Color.DarkRed,
                Location = new Point(x - 15, 520 + MAP_OFFSET_Y)
            };

            Controls.Add(box);
            hazards.Add(new Hazard { Box = box, IsFalling = false, SpawnTime = Environment.TickCount });
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
            hazards.Add(new Hazard { Box = box, IsFalling = true, SpawnTime = Environment.TickCount });
        }

        // ================= GAME LOOP =================
        void GameLoop(object sender, EventArgs e)
        {
            vx = left ? -6 : right ? 6 : 0;

            player.Left += vx;
            player.Top += vy;
            vy += gravity;

            foreach (var g in realGrounds)
            {
                if (player.Bounds.IntersectsWith(g.Bounds) && vy >= 0)
                {
                    player.Top = g.Top - player.Height;
                    vy = 0;
                    jumping = false;
                }
            }

            for (int i = hazards.Count - 1; i >= 0; i--)
            {
                var h = hazards[i];

                if (h.IsFalling)
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

            // 🔥 FIX QUAN TRỌNG: RƠI KHỎI MAP
            if (player.Top > Height)
            {
                Die();
                return;
            }

            if (player.Bounds.IntersectsWith(goal.Bounds))
            {
                gameTimer.Stop();
                hazardTimer.Stop();
                MessageBox.Show($"You won.\nDeaths: {deathCount}", "Finally.");
                Close();
            }
        }

        // ================= DIE =================
        void Die()
        {
            deathCount++;

            gameTimer.Stop();
            hazardTimer.Stop();

            left = right = jumping = false;
            vx = vy = 0;

            MessageBox.Show("Again.", "😈");

            player.Location = new Point(80, 420 + MAP_OFFSET_Y);
            player.BringToFront();

            foreach (var h in hazards)
            {
                Controls.Remove(h.Box);
                h.Box.Dispose();
            }
            hazards.Clear();

            gameTimer.Start();
            hazardTimer.Start();
        }

        // ================= ADD =================
        void AddRealGround(int x, int y, int w = 180)
        {
            var g = new PictureBox
            {
                Size = new Size(w, 40),
                BackColor = Color.Gray,
                Location = new Point(x, y + MAP_OFFSET_Y)
            };
            realGrounds.Add(g);
            Controls.Add(g);
        }

        void AddFakeGround(int x, int y, int w)
        {
            var g = new PictureBox
            {
                Size = new Size(w, 40),
                BackColor = Color.Gray,
                Location = new Point(x, y + MAP_OFFSET_Y)
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

        // ================= INPUT =================
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) left = true;
            if (e.KeyCode == Keys.D) right = true;

            if (e.KeyCode == Keys.Enter && !jumping)
            {
                vy = jumpPower;
                jumping = true;
            }

            if (e.KeyCode == Keys.Escape) Close();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) left = false;
            if (e.KeyCode == Keys.D) right = false;
        }
    }
}
