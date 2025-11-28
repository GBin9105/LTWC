namespace MainMenu
{
    partial class EggDrop
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
            this.pbEgg = new System.Windows.Forms.PictureBox();
            this.pbBasket = new System.Windows.Forms.PictureBox();
            this.pbChicken = new System.Windows.Forms.PictureBox();
            this.lblScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChicken)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pbEgg
            // 
            this.pbEgg.BackColor = System.Drawing.Color.White;
            this.pbEgg.Location = new System.Drawing.Point(150, 120);
            this.pbEgg.Name = "pbEgg";
            this.pbEgg.Size = new System.Drawing.Size(30, 30);
            this.pbEgg.TabIndex = 0;
            this.pbEgg.TabStop = false;
            // 
            // pbBasket
            // 
            this.pbBasket.BackColor = System.Drawing.Color.Blue;
            this.pbBasket.Location = new System.Drawing.Point(140, 380);
            this.pbBasket.Name = "pbBasket";
            this.pbBasket.Size = new System.Drawing.Size(120, 20);
            this.pbBasket.TabIndex = 1;
            this.pbBasket.TabStop = false;
            // 
            // pbChicken
            // 
            this.pbChicken.BackColor = System.Drawing.Color.Yellow;
            this.pbChicken.Location = new System.Drawing.Point(140, 40);
            this.pbChicken.Name = "pbChicken";
            this.pbChicken.Size = new System.Drawing.Size(80, 50);
            this.pbChicken.TabIndex = 2;
            this.pbChicken.TabStop = false;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblScore.Location = new System.Drawing.Point(12, 9);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(89, 25);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "Điểm: 0";
            // 
            // EggDrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pbChicken);
            this.Controls.Add(this.pbBasket);
            this.Controls.Add(this.pbEgg);
            this.Name = "EggDrop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Trứng Rơi";
            this.Load += new System.EventHandler(this.EggDrop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbEgg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChicken)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pbEgg;
        private System.Windows.Forms.PictureBox pbBasket;
        private System.Windows.Forms.PictureBox pbChicken;
        private System.Windows.Forms.Label lblScore;
    }
}
