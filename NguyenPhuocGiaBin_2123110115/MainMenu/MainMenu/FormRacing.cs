// FormRacing.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class FormRacing : Form
    {
        // game state
        private bool moveLeft = false;
        private bool moveRight = false;

        private float playerX;     // vị trí thực để di chuyển mượt
        private float playerSpeed = 14f;

        // STARTING SPEED (yêu cầu: bắt đầu nhanh)
        private int obstacleSpeed = 30;

        private int score = 0;
        private int lives = 3;

        private Random rand = new Random();
        private List<PictureBox> obstacles = new List<PictureBox>();

        // spawn control - now single-spawn continuous
        private int spawnCounter = 0;
        private int spawnInterval = 10; // ticks (gameTimer interval = 20ms) -> 10*20ms = 200ms per car approx

        // Lanes: sử dụng 3 lanes (left, center, right)
        private int lanesCount = 3;
        private int[] laneCentersX; // tính khi spawn/resize

        // control to ensure occasional player-lane danger
        private int spawnSinceLastPlayerLane = 0;
        private int forcePlayerLaneEvery = 6; // mỗi ~6 spawn sẽ force 1 spawn ở player lane

        public FormRacing()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void FormRacing_Load(object sender, EventArgs e)
        {
            // compute lanes initial
            ComputeLaneCenters();
            ResetGame();
            gameTimer.Start();
        }

        private void ComputeLaneCenters()
        {
            if (roadPanel == null) return;

            laneCentersX = new int[lanesCount];

            // lấy left của roadPanel (tương đối Form)
            int rpLeft = roadPanel.Left;
            int rpWidth = Math.Max(1, roadPanel.ClientSize.Width);

            // chia roadPanel thành lanesCount cột đều nhau, lấy center X cho mỗi lane
            for (int i = 0; i < lanesCount; i++)
            {
                float frac = (float)(i + 1) / (lanesCount + 1); // ví dụ 1/4, 2/4, 3/4 cho 3 lanes
                int cx = rpLeft + (int)(frac * rpWidth);
                laneCentersX[i] = cx;
            }
        }

        private void ResetGame()
        {
            // remove obstacles
            foreach (var o in obstacles)
                RemoveAndDispose(o);
            obstacles.Clear();

            // set player size bigger
            playerCar.Width = 80;
            playerCar.Height = 120;

            // recompute lanes (in case window resized)
            ComputeLaneCenters();

            // place player in center lane
            int centerLane = lanesCount / 2;
            if (laneCentersX == null || laneCentersX.Length == 0)
                ComputeLaneCenters();

            playerX = laneCentersX[centerLane] - playerCar.Width / 2f;

            // clamp inside roadPanel
            float minX = roadPanel.Left + 8;
            float maxX = roadPanel.Right - playerCar.Width - 8;
            playerX = Math.Max(minX, Math.Min(maxX, playerX));
            playerCar.Left = (int)playerX;
            playerCar.Top = roadPanel.Bottom - playerCar.Height - 12;

            score = 0;
            lives = 3;

            // START with high speed per request
            obstacleSpeed = 30;

            // spawn controls
            spawnInterval = 10; // spawn one car every ~200ms (20ms tick * 10)
            spawnCounter = 0;
            spawnSinceLastPlayerLane = 0;

            if (lblSpeed != null) lblSpeed.Visible = false; // ẩn tốc độ theo yêu cầu trước đó
            if (lblGameOver != null) lblGameOver.Visible = false;

            UpdateHud();
        }

        private void RemoveAndDispose(Control c)
        {
            if (c == null) return;
            if (Controls.Contains(c)) Controls.Remove(c);
            try { c.Dispose(); } catch { }
        }

        private void UpdateHud()
        {
            if (lblScore != null) lblScore.Text = $"Điểm: {score}";
            if (lblLives != null) lblLives.Text = $"Mạng: {lives}";
            // lblSpeed intentionally hidden
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // smooth movement
            if (moveLeft) playerX -= playerSpeed * 0.9f;
            if (moveRight) playerX += playerSpeed * 0.9f;

            // clamp inside road
            float minX = roadPanel.Left + 8;
            float maxX = roadPanel.Right - playerCar.Width - 8;

            if (playerX < minX) playerX = minX;
            if (playerX > maxX) playerX = maxX;

            playerCar.Left = (int)playerX;

            // spawn obstacles continuously (single spawn per interval)
            spawnCounter++;
            if (spawnCounter >= spawnInterval)
            {
                spawnCounter = 0;
                SpawnSingleObstacle();
            }

            // move obstacles
            for (int i = obstacles.Count - 1; i >= 0; i--)
            {
                var o = obstacles[i];
                o.Top += obstacleSpeed;

                // collision
                if (playerCar.Bounds.IntersectsWith(o.Bounds))
                {
                    RemoveAndDispose(o);
                    obstacles.RemoveAt(i);
                    LoseLife();
                    continue;
                }

                // off screen => score++ (1 point per car passed)
                if (o.Top > roadPanel.Bottom + 50)
                {
                    RemoveAndDispose(o);
                    obstacles.RemoveAt(i);

                    score++;

                    // mỗi 2 điểm tăng tốc
                    if (score % 2 == 0)
                    {
                        obstacleSpeed++;
                    }

                    UpdateHud();
                }
            }

            if (lives <= 0)
                GameOver();

            // draw dashed road
            roadPanel.Invalidate();
        }

        /// <summary>
        /// Spawn a single obstacle. With a periodic bias to spawn in player's lane to create danger.
        /// </summary>
        private void SpawnSingleObstacle()
        {
            if (laneCentersX == null || laneCentersX.Length != lanesCount)
                ComputeLaneCenters();

            // decide lane: usually random across lanes, but every 'forcePlayerLaneEvery' spawns, force player lane
            int playerLane = GetNearestLaneIndexToPlayer();

            bool forcePlayer = false;
            spawnSinceLastPlayerLane++;
            if (spawnSinceLastPlayerLane >= forcePlayerLaneEvery)
            {
                forcePlayer = true;
                spawnSinceLastPlayerLane = 0;
            }

            int laneIdx;
            if (forcePlayer)
            {
                laneIdx = playerLane;
            }
            else
            {
                // random lane but prefer variability: avoid always same lane
                // pick uniformly among lanes but with slight chance to pick player lane as well
                int r = rand.Next(0, 100);
                if (r < 20)
                {
                    laneIdx = playerLane; // 20% chance
                }
                else
                {
                    // pick a side lane more likely
                    List<int> choices = new List<int>();
                    for (int i = 0; i < lanesCount; i++) choices.Add(i);
                    // remove player lane occasionally to encourage alternation
                    if (r < 70) choices.Remove(playerLane);
                    laneIdx = choices[rand.Next(choices.Count)];
                }
            }

            int w = rand.Next(50, 100);
            int h = rand.Next(60, 130);

            PictureBox obs = new PictureBox();
            obs.BackColor = Color.FromArgb(70, 70, 80);
            obs.Size = new Size(w, h);

            // center X of lane
            int laneCenterX = laneCentersX[laneIdx];
            // choose x so obstacle centered in lane with a small random jitter
            int jitter = rand.Next(-12, 13);
            int x = laneCenterX - (w / 2) + jitter;

            // clamp x to road bounds
            int minX = roadPanel.Left + 10;
            int maxX = roadPanel.Right - w - 10;
            if (x < minX) x = minX + rand.Next(0, 10);
            if (x > maxX) x = Math.Max(minX, maxX - rand.Next(0, 10));

            // spawn Y just above the top, stagger a little vertically to avoid perfect overlap
            int y = roadPanel.Top - h - rand.Next(8, 60);

            obs.Left = x;
            obs.Top = y;
            obs.Tag = "obstacle";

            Controls.Add(obs);
            obs.BringToFront();
            // ensure player is visible on top of obstacles
            playerCar.BringToFront();

            obstacles.Add(obs);
        }

        private int GetNearestLaneIndexToPlayer()
        {
            if (laneCentersX == null || laneCentersX.Length == 0)
                ComputeLaneCenters();

            int pcx = (int)(playerX + playerCar.Width / 2f);
            int best = 0;
            int bestDist = Math.Abs(pcx - laneCentersX[0]);
            for (int i = 1; i < laneCentersX.Length; i++)
            {
                int d = Math.Abs(pcx - laneCentersX[i]);
                if (d < bestDist)
                {
                    best = i;
                    bestDist = d;
                }
            }
            return best;
        }

        private void LoseLife()
        {
            if (lives <= 0) return;
            lives--;
            UpdateHud();
            _ = FlashPlayer();
        }

        private async Task FlashPlayer()
        {
            for (int i = 0; i < 6; i++)
            {
                playerCar.Visible = !playerCar.Visible;
                await Task.Delay(120);
            }
            playerCar.Visible = true;
        }

        private void GameOver()
        {
            gameTimer.Stop();
            if (lblGameOver != null)
            {
                lblGameOver.Visible = true;
                lblGameOver.BringToFront();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetGame();
            gameTimer.Start();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRacing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) moveLeft = true;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) moveRight = true;
        }

        private void FormRacing_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) moveLeft = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) moveRight = false;
        }

        private void roadPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            int centerX = roadPanel.ClientSize.Width / 2;
            int dashH = 40, gap = 26;

            using (var pen = new Pen(Color.WhiteSmoke, 6))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                pen.DashPattern = new float[] { dashH, gap };

                // simple motion offset to simulate forward movement
                int offset = (score * 4) % (dashH + gap);
                g.DrawLine(pen,
                    centerX,
                    -dashH + offset,
                    centerX,
                    roadPanel.ClientSize.Height + dashH + gap);
            }
        }

        // Optional: recompute lane centers on resize so lanes stay aligned
        private void FormRacing_Resize(object sender, EventArgs e)
        {
            ComputeLaneCenters();

            // ensure player remains inside road after resize
            float minX = roadPanel.Left + 8;
            float maxX = roadPanel.Right - playerCar.Width - 8;
            playerX = Math.Max(minX, Math.Min(maxX, playerX));
            playerCar.Left = (int)playerX;

            // reposition GameOver label if needed (designer may handle it)
            if (lblGameOver != null)
            {
                lblGameOver.Left = (this.ClientSize.Width - lblGameOver.Width) / 2;
                lblGameOver.Top = (this.ClientSize.Height - lblGameOver.Height) / 2;
            }
        }
    }
}
