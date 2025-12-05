using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class DinoEvolution : Form
    {
        // ==========================
        // PLAYER STATE
        // ==========================
        bool isPtero = false;
        bool isJumping = false;
        bool isDucking = false;

        // ==========================
        // PHYSICS
        // ==========================
        int gravity = 2;
        int jumpSpeed = -26;
        int verticalSpeed = 0;

        int jumpCount = 0;
        const int maxJump = 2;

        // ==========================
        // OBSTACLES
        // ==========================
        Random rand = new();
        int obstacleSpeed = 10;
        int score = 0;

        List<PictureBox> obstacles = new();

        // ==========================
        // BOSS SYSTEM
        // ==========================
        PictureBox boss;
        bool bossActive = false;
        DateTime lastBossSpawn = DateTime.Now;
        DateTime bossDespawnTime;

        List<(PictureBox bullet, int vx, int vy)> bullets = new();

        public DinoEvolution()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void DinoEvolution_Load(object sender, EventArgs e)
        {
            player.Top = ground.Top - player.Height - 5;
            player.Left = this.Width / 10;

            CreateObstacles();
            gameTimer.Start();
        }

        // ==========================
        // CREATE OBSTACLES
        // ==========================
        private void CreateObstacles()
        {
            foreach (var obs in obstacles)
                Controls.Remove(obs);

            obstacles.Clear();

            if (!isPtero)
            {
                int count = rand.Next(1, 6);
                int startX = this.Width + 200;

                for (int i = 0; i < count; i++)
                {
                    PictureBox obs = new();
                    obs.BackColor = Color.Brown;

                    int size = rand.Next(1, Math.Min(4, score / 5 + 2));
                    obs.Width = 40 * size;
                    obs.Height = 50;

                    obs.Top = ground.Top - obs.Height;
                    obs.Left = startX + i * rand.Next(250, 450);

                    Controls.Add(obs);
                    obstacles.Add(obs);
                }
            }
            else
            {
                int count = rand.Next(10, 21);
                int startX = this.Width + 200;

                for (int i = 0; i < count; i++)
                {
                    PictureBox obs = new();
                    obs.BackColor = Color.MediumPurple;
                    obs.Size = new Size(40, 40);
                    obs.Top = rand.Next(80, ground.Top - 200);
                    obs.Left = startX + i * rand.Next(80, 160);

                    Controls.Add(obs);
                    obstacles.Add(obs);
                }
            }
        }

        // ==========================
        // GAME LOOP
        // ==========================
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            obstacleSpeed = 10 + score;

            lblScore.Text = $"Điểm: {score}";
            lblSpeed.Text = $"Tốc độ: {obstacleSpeed}";
            lblMode.Text = isPtero ? "Chế độ: Pterosaur" : "Chế độ: T-Rex";

            UpdatePlayer();
            UpdateObstacles();

            MaybeSpawnBoss();
            UpdateBoss();
            UpdateBullets();

            CheckCollision();
        }

        // ==========================
        // PLAYER
        // ==========================
        private void UpdatePlayer()
        {
            if (!isPtero)
            {
                if (isJumping)
                {
                    player.Top += verticalSpeed;
                    verticalSpeed += gravity;

                    if (player.Top >= ground.Top - player.Height - 5)
                    {
                        isJumping = false;
                        jumpCount = 0;
                        verticalSpeed = 0;
                        player.Top = ground.Top - player.Height - 5;
                    }
                }
            }
            else
            {
                player.Top += verticalSpeed;
                if (player.Top < 60) player.Top = 60;
                if (player.Bottom > ground.Top - 50)
                    player.Top = ground.Top - 50 - player.Height;
            }
        }

        // ==========================
        // OBSTACLES
        // ==========================
        private void UpdateObstacles()
        {
            for (int i = obstacles.Count - 1; i >= 0; i--)
            {
                var obs = obstacles[i];
                obs.Left -= obstacleSpeed;

                if (obs.Right < 0)
                {
                    Controls.Remove(obs);
                    obstacles.RemoveAt(i);
                }
            }

            if (obstacles.Count == 0)
            {
                score++;
                obstacleSpeed++;
                CreateObstacles();
            }
        }

        // ==========================
        // BOSS SPAWN
        // ==========================
        private void MaybeSpawnBoss()
        {
            if (bossActive) return;

            if ((DateTime.Now - lastBossSpawn).TotalSeconds >= 15)
            {
                lastBossSpawn = DateTime.Now;
                SpawnBoss();
            }
        }

        private void SpawnBoss()
        {
            boss = new PictureBox();
            boss.BackColor = Color.DarkRed;
            boss.Size = new Size(220, 140);

            boss.Top = rand.Next(100, ground.Top - 300);
            boss.Left = this.Width - 300; // spawn trong màn hình

            Controls.Add(boss);
            boss.BringToFront();

            bossActive = true;
            bossDespawnTime = DateTime.Now.AddSeconds(30);

            // bắn ngay khi xuất hiện
            SpawnBulletCone(rand.Next(10, 21));

            _ = BossFireLoop();
        }

        private async Task BossFireLoop()
        {
            while (bossActive && boss != null)
            {
                await Task.Delay(5000);
                if (bossActive && boss != null)
                    SpawnBulletCone(rand.Next(10, 21));
            }
        }

        // ==========================
        // BOSS UPDATE
        // ==========================
        private void UpdateBoss()
        {
            if (!bossActive || boss == null) return;

            boss.Left -= 2; // chậm lại → đủ thời gian bắn

            if (DateTime.Now >= bossDespawnTime)
            {
                Controls.Remove(boss);
                boss = null;
                bossActive = false;
            }
        }

        // ==========================
        // BULLETS
        // ==========================
        private void SpawnBulletCone(int count)
        {
            if (boss == null) return;

            for (int i = 0; i < count; i++)
            {
                int angle = rand.Next(-60, 61);
                double rad = angle * Math.PI / 180;

                int vx = (int)(12 * Math.Cos(rad));
                int vy = (int)(12 * Math.Sin(rad));

                PictureBox bullet = new();
                bullet.BackColor = Color.Yellow;
                bullet.Size = new Size(15, 15);

                bullet.Left = boss.Left;
                bullet.Top = boss.Top + boss.Height / 2;

                Controls.Add(bullet);
                bullet.BringToFront();

                bullets.Add((bullet, vx, vy));
            }
        }

        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var (b, vx, vy) = bullets[i];

                b.Left -= vx;
                b.Top += vy;

                if (player.Bounds.IntersectsWith(b.Bounds))
                {
                    GameOver();
                    return;
                }

                if (b.Right < 0 || b.Left > this.Width ||
                    b.Bottom < 0 || b.Top > this.Height)
                {
                    Controls.Remove(b);
                    bullets.RemoveAt(i);
                }
            }
        }

        // ==========================
        // COLLISION
        // ==========================
        private void CheckCollision()
        {
            foreach (var obs in obstacles)
                if (player.Bounds.IntersectsWith(obs.Bounds))
                    GameOver();

            if (boss != null &&
                player.Bounds.IntersectsWith(boss.Bounds))
                GameOver();
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show($"Game Over!\nĐiểm: {score}");
            this.Close();
        }

        // ==========================
        // INPUT HANDLERS
        // ==========================
        private void DinoEvolution_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isPtero)
            {
                if (e.KeyCode == Keys.Space && jumpCount < maxJump)
                {
                    isJumping = true;
                    verticalSpeed = jumpSpeed;
                    jumpCount++;
                }

                if (e.KeyCode == Keys.Down)
                {
                    isDucking = true;
                    player.Height = 50;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Up) verticalSpeed = -14;
                if (e.KeyCode == Keys.Down) verticalSpeed = 14;
            }

            if (e.KeyCode == Keys.Z)
                TransformMode();

            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void DinoEvolution_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                isDucking = false;
                player.Height = isPtero ? 60 : 80;
            }

            if (isPtero)
                verticalSpeed = 0;
        }

        // ==========================
        // TRANSFORM MODE
        // ==========================
        private async void TransformMode()
        {
            await AnimationHelper.TransformAnimation(player, () =>
            {
                isPtero = !isPtero;

                if (isPtero)
                {
                    player.BackColor = Color.OrangeRed;
                    player.Size = new Size(110, 60);
                    player.Top = ground.Top - 250;
                }
                else
                {
                    player.BackColor = Color.LimeGreen;
                    player.Size = new Size(80, 80);
                    player.Top = ground.Top - 85;
                    jumpCount = 0;
                }
            });

            CreateObstacles();
        }
    }
}
