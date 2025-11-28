using System;
using System.Windows.Forms;

namespace MainMenu
{
    public partial class FinishForm : Form
    {
        public FinishForm(int score, int total)
        {
            InitializeComponent();

            lblName.Text = "Người thi: " + LoginForm.PlayerName;
            lblScore.Text = $"Số câu đúng: {score}/{total}";
            lblPercent.Text = $"Tỉ lệ: {(score * 100 / total)}%";
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            Question q = new Question();
            q.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
