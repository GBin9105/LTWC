namespace MainMenu
{
    partial class ChickenGame
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
            this.pbChicken = new System.Windows.Forms.PictureBox();
            this.pbBasket = new System.Windows.Forms.PictureBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblMiss = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbChicken)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasket)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pbChicken
            // 
            this.pbChicken.BackColor = System.Drawing.Color.Yellow;
            this.pbChicken.Location = new System.Drawing.Point(150, 40);
            this.pbChicken.Name = "pbChicken";
            this.pbChicken.Size = new System.Drawing.Size(80, 40);
            this.pbChicken.TabIndex = 0;
            this.pbChicken.TabStop = false;
            // 
            // pbBasket
            // 
            this.pbBasket.BackColor = System.Drawing.Color.DarkCyan;
            this.pbBasket.Location = new System.Drawing.Point(160, 360);
            this.pbBasket.Name = "pbBasket";
            this.pbBasket.Size = new System.Drawing.Size(70, 70);
            this.pbBasket.TabIndex = 1;
            this.pbBasket.TabStop = false;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblScore.Location = new System.Drawing.Point(10, 10);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(89, 25);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "Điểm: 0";
            // 
            // lblMiss
            // 
            this.lblMiss.AutoSize = true;
            this.lblMiss.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMiss.Location = new System.Drawing.Point(150, 10);
            this.lblMiss.Name = "lblMiss";
            this.lblMiss.Size = new System.Drawing.Size(105, 25);
            this.lblMiss.TabIndex = 3;
            this.lblMiss.Text = "Rơi Mất: 0";
            // 
            // ChickenGame
            // 
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.lblMiss);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pbBasket);
            this.Controls.Add(this.pbChicken);
            this.Name = "ChickenGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Gà Đẻ Trứng";
            this.Load += new System.EventHandler(this.ChickenGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbChicken)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasket)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pbChicken;
        private System.Windows.Forms.PictureBox pbBasket;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblMiss;
    }
}
