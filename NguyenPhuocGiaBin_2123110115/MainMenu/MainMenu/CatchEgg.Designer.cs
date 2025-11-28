namespace MainMenu
{
    partial class CatchEgg
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pbEgg1 = new System.Windows.Forms.PictureBox();
            this.pbEgg2 = new System.Windows.Forms.PictureBox();
            this.pbEgg3 = new System.Windows.Forms.PictureBox();
            this.pbBasket = new System.Windows.Forms.PictureBox();
            this.lblScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasket)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pbEgg1
            // 
            this.pbEgg1.BackColor = System.Drawing.Color.White;
            this.pbEgg1.Size = new System.Drawing.Size(25, 25);
            this.pbEgg1.Location = new System.Drawing.Point(50, -100);
            // 
            // pbEgg2
            // 
            this.pbEgg2.BackColor = System.Drawing.Color.White;
            this.pbEgg2.Size = new System.Drawing.Size(25, 25);
            this.pbEgg2.Location = new System.Drawing.Point(150, -200);
            // 
            // pbEgg3
            // 
            this.pbEgg3.BackColor = System.Drawing.Color.White;
            this.pbEgg3.Size = new System.Drawing.Size(25, 25);
            this.pbEgg3.Location = new System.Drawing.Point(250, -150);
            // 
            // pbBasket
            // 
            this.pbBasket.BackColor = System.Drawing.Color.Blue;
            this.pbBasket.Size = new System.Drawing.Size(120, 20);
            this.pbBasket.Location = new System.Drawing.Point(150, 380);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblScore.Location = new System.Drawing.Point(10, 10);
            this.lblScore.Text = "Điểm: 0";
            // 
            // CatchEgg
            // 
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pbBasket);
            this.Controls.Add(this.pbEgg3);
            this.Controls.Add(this.pbEgg2);
            this.Controls.Add(this.pbEgg1);
            this.Name = "CatchEgg";
            this.Text = "Game Bắt Trứng";
            this.Load += new System.EventHandler(this.CatchEgg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasket)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pbEgg1;
        private System.Windows.Forms.PictureBox pbEgg2;
        private System.Windows.Forms.PictureBox pbEgg3;
        private System.Windows.Forms.PictureBox pbBasket;
        private System.Windows.Forms.Label lblScore;
    }
}
