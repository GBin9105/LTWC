using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class DinoEvolution : Form
    {
        bool isPtero = false;
        bool isJumping = false;

        bool moveLeft = false;
        bool moveRight = false;
        bool moveUp = false;
        bool moveDown = false; // chỉ dùng cho Pterosaur

        int moveSpeed = 12;

        // ================= PHYSICS =================
        int gravity = 1;
        int jumpSpeed = -30;
        int verticalSpeed = 0;

        // ================= JUMP =================
        int jumpCount = 0;
        const int maxJump = 3;

        // ================= LIVES =================
        int lives = 3;

        // ================= INVINCIBILITY =================
        bool invincible = false;
        DateTime invincibleUntil;

        Random rand = new();
        int obstacleSpeed = 10;
        int score = 0;

        List<PictureBox> obstacles = new();

        // ================= BOSS =================
        PictureBox boss;
        bool bossActive = false;
        DateTime lastBossSpawn = DateTime.Now;
        DateTime bossDespawnTime;

        // ================= BULLETS & MISSILES =================
        List<(PictureBox bullet, int vx, int vy)> bullets = new();
        List<(PictureBox missile, int vx, int vy)> missiles = new();

        public DinoEvolution()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void DinoEvolution_Load(object sender, EventArgs e)
        {
            player.Top = ground.Top - player.Height - 5;
            player.Left = Width / 10;

            CreateObstacles();
            gameTimer.Start();
        }

        // =====================================================================
        // CREATE OBSTACLES
        // =====================================================================
        private void CreateObstacles()
        {
            foreach (var o in obstacles)
                Controls.Remove(o);

            obstacles.Clear();

            if (!isPtero)
            {
                int count = rand.Next(10, 18);
                int startX = Width + 300;

                for (int i = 0; i < count; i++)
                {
                    PictureBox obs = new();
                    obs.BackColor = Color.Brown;
                    obs.Size = new Size(30 * rand.Next(1, 3), 40);

                    obs.Top = ground.Top - obs.Height;
                    obs.Left = startX + i * rand.Next(120, 200);

                    Controls.Add(obs);
                    obstacles.Add(obs);
                }
            }
            else
            {
                int count = rand.Next(40, 70);
                int startX = Width + 150;

                for (int i = 0; i < count; i++)
                {
                    PictureBox obs = new();
                    obs.BackColor = Color.MediumPurple;
                    obs.Size = new Size(40, 40);

                    obs.Top = rand.Next(hudPanel.Bottom + 20, ground.Top - 120);
                    obs.Left = startX + i * rand.Next(40, 90);

                    Controls.Add(obs);
                    obstacles.Add(obs);
                }
            }

            int missileCount = isPtero ? rand.Next(6, 10) : rand.Next(3, 5);
            for (int i = 0; i < missileCount; i++)
                SpawnHomingMissile();
        }

        // =====================================================================
        // GAME LOOP
        // =====================================================================
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            lblMode.Text = isPtero ? "Chế độ: Pterosaur" : "Chế độ: T-Rex";
            lblSpeed.Text = "Tốc độ: " + obstacleSpeed;
            lblScore.Text = $"Điểm: {score} | Mạng: {lives}";

            obstacleSpeed = isPtero
                ? 12 + (score / 10)
                : 8 + (score / 20);

            if (invincible && DateTime.Now >= invincibleUntil)
            {
                invincible = false;
                player.BackColor = isPtero ? Color.OrangeRed : Color.LimeGreen;
            }

            UpdatePlayer();
            UpdateObstacles();
            UpdateMissiles();
            MaybeSpawnBoss();
            UpdateBoss();
            UpdateBullets();
            CheckCollision();
        }

        // =====================================================================
        // PLAYER MOVEMENT (NO CROUCH)
        // =====================================================================
        private void UpdatePlayer()
        {
            if (moveLeft) player.Left -= moveSpeed;
            if (moveRight) player.Left += moveSpeed;

            player.Left = Math.Max(0, Math.Min(Width - player.Width, player.Left));

            if (!isPtero)
            {
                // ===== T-REX: CHỈ CHẠY + NHẢY =====
                if (moveUp && jumpCount < maxJump && !isJumping)
                {
                    isJumping = true;
                    verticalSpeed = jumpSpeed;
                    jumpCount++;
                }

                if (isJumping)
                {
                    player.Top += verticalSpeed;
                    verticalSpeed += gravity;

                    if (player.Top >= ground.Top - player.Height - 5)
                    {
                        player.Top = ground.Top - player.Height - 5;
                        isJumping = false;
                        verticalSpeed = 0;
                        jumpCount = 0;
                    }
                }

                // Chiều cao cố định – KHÔNG NGỒI
                player.Height = 80;
            }
            else
            {
                // ===== PTEROSAUR =====
                if (moveUp) player.Top -= moveSpeed;
                if (moveDown) player.Top += moveSpeed;

                if (player.Bottom > ground.Top - 40)
                    player.Top = ground.Top - 40 - player.Height;
            }

            if (player.Top < hudPanel.Bottom + 10)
                player.Top = hudPanel.Bottom + 10;
        }

        // =====================================================================
        // OBSTACLES UPDATE
        // =====================================================================
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
                    score++;
                }
            }

            if (obstacles.Count == 0)
                CreateObstacles();
        }

        // =====================================================================
        // MISSILES
        // =====================================================================
        private void SpawnHomingMissile()
        {
            PictureBox m = new();
            m.BackColor = Color.Red;
            m.Size = new Size(22, 22);

            m.Left = Width + rand.Next(50, 100);
            m.Top = rand.Next(hudPanel.Bottom + 20, ground.Top - 120);

            Controls.Add(m);

            double dx = (player.Left + player.Width / 2) - m.Left;
            double dy = (player.Top + player.Height / 2) - m.Top;
            double d = Math.Sqrt(dx * dx + dy * dy);

            missiles.Add((m, (int)(16 * dx / d), (int)(16 * dy / d)));
        }

        private void UpdateMissiles()
        {
            for (int i = missiles.Count - 1; i >= 0; i--)
            {
                var (m, vx, vy) = missiles[i];
                m.Left += vx;
                m.Top += vy;

                if (!invincible && m.Bounds.IntersectsWith(player.Bounds))
                {
                    LoseLife();
                    return;
                }

                if (!ClientRectangle.IntersectsWith(m.Bounds))
                {
                    Controls.Remove(m);
                    missiles.RemoveAt(i);
                }
            }
        }

        // =====================================================================
        // BOSS / BULLETS (GIỮ NGUYÊN)
        // =====================================================================
        private void MaybeSpawnBoss()
        {
            if (!bossActive && (DateTime.Now - lastBossSpawn).TotalSeconds >= 15)
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
            boss.Left = Width - 300;
            boss.Top = rand.Next(hudPanel.Bottom + 50, ground.Top - 300);

            Controls.Add(boss);
            bossActive = true;
            bossDespawnTime = DateTime.Now.AddSeconds(30);

            SpawnBulletCone(rand.Next(20, 40));
            _ = BossFireLoop();
        }

        private void UpdateBoss()
        {
            if (!bossActive || boss == null) return;

            boss.Left -= 2;
            if (DateTime.Now >= bossDespawnTime)
            {
                Controls.Remove(boss);
                boss = null;
                bossActive = false;
            }
        }

        private async Task BossFireLoop()
        {
            while (bossActive && boss != null)
            {
                await Task.Delay(3000);
                SpawnBulletCone(rand.Next(20, 40));
            }
        }

        private void SpawnBulletCone(int count)
        {
            if (boss == null) return;

            for (int i = 0; i < count; i++)
            {
                double rad = rand.Next(-80, 81) * Math.PI / 180;
                PictureBox b = new()
                {
                    BackColor = Color.Yellow,
                    Size = new Size(15, 15),
                    Left = boss.Left,
                    Top = boss.Top + boss.Height / 2
                };

                Controls.Add(b);
                bullets.Add((b, (int)(12 * Math.Cos(rad)), (int)(12 * Math.Sin(rad))));
            }
        }

        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var (b, vx, vy) = bullets[i];
                b.Left -= vx;
                b.Top += vy;

                if (!invincible && b.Bounds.IntersectsWith(player.Bounds))
                {
                    LoseLife();
                    return;
                }

                if (!ClientRectangle.IntersectsWith(b.Bounds))
                {
                    Controls.Remove(b);
                    bullets.RemoveAt(i);
                }
            }
        }

        private void CheckCollision()
        {
            foreach (var obs in obstacles)
                if (!invincible && obs.Bounds.IntersectsWith(player.Bounds))
                {
                    LoseLife();
                    return;
                }

            if (!invincible && boss != null && boss.Bounds.IntersectsWith(player.Bounds))
            {
                LoseLife();
                return;
            }
        }

        private async void LoseLife()
        {
            if (invincible) return;

            lives--;
            if (lives <= 0)
            {
                GameOver();
                return;
            }

            invincible = true;
            invincibleUntil = DateTime.Now.AddSeconds(1.5);
            await FlashInvincible();
        }

        private async Task FlashInvincible()
        {
            for (int i = 0; i < 8; i++)
            {
                player.Visible = !player.Visible;
                await Task.Delay(120);
            }
            player.Visible = true;
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show($"Bạn đã thua!\nĐiểm: {score}");
            Close();
        }

        // =====================================================================
        // INPUT
        // =====================================================================
        private void DinoEvolution_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) moveLeft = true;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) moveRight = true;
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) moveUp = true;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) moveDown = true;

            if (e.KeyCode == Keys.Enter)
                TransformMode();

            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void DinoEvolution_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) moveLeft = false;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) moveRight = false;
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) moveUp = false;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) moveDown = false;
        }

        private async void TransformMode()
        {
            await AnimationHelper.TransformAnimation(player, () =>
            {
                isPtero = !isPtero;
                player.Size = isPtero ? new Size(110, 60) : new Size(80, 80);
                player.BackColor = isPtero ? Color.OrangeRed : Color.LimeGreen;
                jumpCount = 0;
            });

            CreateObstacles();
        }
    }
}
