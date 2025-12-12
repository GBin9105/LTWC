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
        bool moveDown = false;

        int moveSpeed = 12;

        // PHYSICS
        int gravity = 2;
        int jumpSpeed = -26;
        int verticalSpeed = 0;

        // TRIPLE JUMP
        int jumpCount = 0;
        const int maxJump = 3;

        // LIVES
        int lives = 3;

        // INVINCIBILITY
        bool invincible = false;
        DateTime invincibleUntil;

        Random rand = new();
        int obstacleSpeed = 10;
        int score = 0;

        List<PictureBox> obstacles = new();

        // BOSS
        PictureBox boss;
        bool bossActive = false;
        DateTime lastBossSpawn = DateTime.Now;
        DateTime bossDespawnTime;
        bool bossHasFiredOnce = false;

        // LASER SYSTEM
        PictureBox laser;
        bool laserActive = false;
        int laserPhase = 0;
        int laserVx = 0;
        int sweepDirection = 0;       // 1 = down, -1 = up
        int sweepDistance = 0;        // random 80–150
        int sweepMoved = 0;

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
                int count = rand.Next(20, 40);
                int startX = Width + 200;

                for (int i = 0; i < count; i++)
                {
                    PictureBox obs = new();
                    obs.BackColor = Color.Brown;
                    obs.Width = 40 * rand.Next(1, 4);
                    obs.Height = 50;

                    obs.Top = ground.Top - obs.Height;
                    obs.Left = startX + i * rand.Next(60, 140);

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

            int missileCount = rand.Next(6, 10);
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

            obstacleSpeed = 10 + (score / 10);

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

            if (laserActive)
                UpdateLaser();

            CheckCollision();
        }

        // =====================================================================
        // PLAYER MOVEMENT
        // =====================================================================
        private void UpdatePlayer()
        {
            if (moveLeft) player.Left -= moveSpeed;
            if (moveRight) player.Left += moveSpeed;

            player.Left = Math.Max(0, Math.Min(Width - player.Width, player.Left));

            if (!isPtero)
            {
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
                        isJumping = false;
                        jumpCount = 0;
                        verticalSpeed = 0;
                        player.Top = ground.Top - player.Height - 5;
                    }
                }

                player.Height = moveDown ? 50 : 80;
            }
            else
            {
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
                PictureBox obs = obstacles[i];
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
        // HOMING MISSILE
        // =====================================================================
        private void SpawnHomingMissile()
        {
            PictureBox m = new();
            m.BackColor = Color.Red;
            m.Size = new Size(22, 22);

            m.Left = Width + rand.Next(50, 100);
            m.Top = rand.Next(hudPanel.Bottom + 20, ground.Top - 120);

            Controls.Add(m);
            m.BringToFront();

            int tx = player.Left + player.Width / 2;
            int ty = player.Top + player.Height / 2;

            double dx = tx - m.Left;
            double dy = ty - m.Top;
            double d = Math.Sqrt(dx * dx + dy * dy);

            int speed = 16;
            int vx = (int)(speed * dx / d);
            int vy = (int)(speed * dy / d);

            missiles.Add((m, vx, vy));
        }

        private void UpdateMissiles()
        {
            for (int i = missiles.Count - 1; i >= 0; i--)
            {
                var (m, vx, vy) = missiles[i];

                m.Left += vx;
                m.Top += vy;

                if (!invincible && player.Bounds.IntersectsWith(m.Bounds))
                {
                    LoseLife();
                    return;
                }

                if (m.Left < -80 || m.Left > Width + 80 ||
                    m.Top < -80 || m.Top > Height + 80)
                {
                    Controls.Remove(m);
                    missiles.RemoveAt(i);
                }
            }
        }

        // =====================================================================
        // BOSS SYSTEM
        // =====================================================================
        private void MaybeSpawnBoss()
        {
            if (!bossActive && (DateTime.Now - lastBossSpawn).TotalSeconds >= 15)
            {
                lastBossSpawn = DateTime.Now;
                bossHasFiredOnce = false;
                SpawnBoss();
            }
        }

        private void SpawnBoss()
        {
            boss = new PictureBox();
            boss.BackColor = Color.DarkRed;
            boss.Size = new Size(220, 140);

            boss.Top = rand.Next(hudPanel.Bottom + 50, ground.Top - 300);
            boss.Left = Width - 300;

            Controls.Add(boss);
            bossActive = true;

            bossDespawnTime = DateTime.Now.AddSeconds(30);

            SpawnBulletCone(rand.Next(20, 40));
            bossHasFiredOnce = true;

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

                if (bossHasFiredOnce && !laserActive)
                    ActivateLaser();

                SpawnBulletCone(rand.Next(20, 40));
            }
        }

        // =====================================================================
        // LASER SYSTEM
        // =====================================================================
        private void ActivateLaser()
        {
            laser = new PictureBox();
            laser.BackColor = Color.Cyan;
            laser.Height = 40;
            laser.Width = Width; // bắn dài hết màn hình

            laser.Top = boss.Top + boss.Height / 2 - laser.Height / 2;

            bool shootDown = rand.Next(0, 2) == 0;
            sweepDirection = shootDown ? 1 : -1;

            sweepDistance = rand.Next(80, 150);
            sweepMoved = 0;

            laser.Left = 0;

            Controls.Add(laser);
            laser.BringToFront();

            laserPhase = 0;
            laserActive = true;
        }

        private void UpdateLaser()
        {
            if (laser == null) return;

            // Phase 0 – đứng yên 1 tí trước khi quét
            if (laserPhase == 0)
            {
                laserPhase = 1;
            }
            else
            {
                // Phase 1 – Quét lên hoặc xuống
                int move = 4 * sweepDirection;
                laser.Top += move;
                sweepMoved += Math.Abs(move);

                if (sweepMoved >= sweepDistance)
                {
                    Controls.Remove(laser);
                    laser = null;
                    laserActive = false;
                }
            }

            if (!invincible && player.Bounds.IntersectsWith(laser.Bounds))
                LoseLife();
        }

        // =====================================================================
        // BOSS BULLETS
        // =====================================================================
        private void SpawnBulletCone(int count)
        {
            if (boss == null) return;

            for (int i = 0; i < count; i++)
            {
                int angle = rand.Next(-80, 81);
                double rad = angle * Math.PI / 180;

                int vx = (int)(12 * Math.Cos(rad));
                int vy = (int)(12 * Math.Sin(rad));

                PictureBox b = new();
                b.BackColor = Color.Yellow;
                b.Size = new Size(15, 15);

                b.Left = boss.Left;
                b.Top = boss.Top + boss.Height / 2;

                Controls.Add(b);
                bullets.Add((b, vx, vy));
            }
        }

        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var (b, vx, vy) = bullets[i];

                b.Left -= vx;
                b.Top += vy;

                if (!invincible && player.Bounds.IntersectsWith(b.Bounds))
                {
                    LoseLife();
                    return;
                }

                if (b.Right < 0 || b.Left > Width ||
                    b.Bottom < 0 || b.Top > Height)
                {
                    Controls.Remove(b);
                    bullets.RemoveAt(i);
                }
            }
        }

        // =====================================================================
        // COLLISION
        // =====================================================================
        private void CheckCollision()
        {
            foreach (var obs in obstacles)
                if (!invincible && player.Bounds.IntersectsWith(obs.Bounds))
                {
                    LoseLife();
                    return;
                }

            if (!invincible && boss != null && player.Bounds.IntersectsWith(boss.Bounds))
            {
                LoseLife();
                return;
            }
        }

        // =====================================================================
        // LIFE SYSTEM
        // =====================================================================
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

                if (isPtero)
                {
                    player.BackColor = Color.OrangeRed;
                    player.Size = new Size(110, 60);
                }
                else
                {
                    player.BackColor = Color.LimeGreen;
                    player.Size = new Size(80, 80);
                    jumpCount = 0;
                }
            });

            CreateObstacles();
        }
    }
}
