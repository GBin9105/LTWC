namespace MainMenu
{
    partial class GameBall
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ball = new PictureBox();
            paddle = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            lblScore = new Label();
            ((System.ComponentModel.ISupportInitialize)ball).BeginInit();
            ((System.ComponentModel.ISupportInitialize)paddle).BeginInit();
            SuspendLayout();
            // 
            // ball
            // 
            ball.BackColor = Color.Red;
            ball.Location = new Point(257, 200);
            ball.Margin = new Padding(4, 5, 4, 5);
            ball.Name = "ball";
            ball.Size = new Size(29, 33);
            ball.TabIndex = 0;
            ball.TabStop = false;
            // 
            // paddle
            // 
            paddle.BackColor = Color.Blue;
            paddle.Location = new Point(214, 583);
            paddle.Margin = new Padding(4, 5, 4, 5);
            paddle.Name = "paddle";
            paddle.Size = new Size(143, 25);
            paddle.TabIndex = 1;
            paddle.TabStop = false;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 15;
            timer1.Tick += timer1_Tick;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblScore.Location = new Point(17, 15);
            lblScore.Margin = new Padding(4, 0, 4, 0);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(102, 32);
            lblScore.TabIndex = 2;
            lblScore.Text = "Điểm: 0";
            // 
            // GameBall
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 685);
            Controls.Add(lblScore);
            Controls.Add(paddle);
            Controls.Add(ball);
            Margin = new Padding(4, 5, 4, 5);
            Name = "GameBall";
            Text = "Game Bóng";
            Load += GameBall_Load;
            KeyDown += GameBall_KeyDown;
            KeyUp += GameBall_KeyUp;
            ((System.ComponentModel.ISupportInitialize)ball).EndInit();
            ((System.ComponentModel.ISupportInitialize)paddle).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.PictureBox paddle;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblScore;
    }
}
