namespace MainMenu
{
    partial class Question
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
            this.lblQuestion = new System.Windows.Forms.Label();
            this.btnA = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblQuestion.Location = new System.Drawing.Point(20, 20);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(460, 60);
            this.lblQuestion.Text = "Câu hỏi";
            // 
            // btnA
            // 
            this.btnA.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnA.Location = new System.Drawing.Point(40, 120);
            this.btnA.Size = new System.Drawing.Size(400, 40);
            this.btnA.Text = "A";
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnB
            // 
            this.btnB.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnB.Location = new System.Drawing.Point(40, 180);
            this.btnB.Size = new System.Drawing.Size(400, 40);
            this.btnB.Text = "B";
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // btnC
            // 
            this.btnC.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnC.Location = new System.Drawing.Point(40, 240);
            this.btnC.Size = new System.Drawing.Size(400, 40);
            this.btnC.Text = "C";
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // btnD
            // 
            this.btnD.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnD.Location = new System.Drawing.Point(40, 300);
            this.btnD.Size = new System.Drawing.Size(400, 40);
            this.btnD.Text = "D";
            this.btnD.Click += new System.EventHandler(this.btnD_Click);
            // 
            // lblScore
            // 
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScore.Location = new System.Drawing.Point(20, 380);
            this.lblScore.Size = new System.Drawing.Size(200, 30);
            this.lblScore.Text = "Điểm: 0";
            // 
            // Question Form
            // 
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.btnD);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.btnB);
            this.Controls.Add(this.btnA);
            this.Controls.Add(this.lblQuestion);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thi Trắc Nghiệm";
            this.Load += new System.EventHandler(this.Question_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Label lblScore;
    }
}
