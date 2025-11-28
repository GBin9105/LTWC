using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MainMenu
{
    public partial class Question : Form
    {
        int currentIndex = 0;
        int score = 0;

        // Danh sách câu hỏi
        List<QuestionItem> questions = new List<QuestionItem>();

        public Question()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private void Question_Load(object sender, EventArgs e)
        {
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            // Bạn có thể thêm bao nhiêu câu cũng được
            questions.Add(new QuestionItem("Quốc gia nào có dân số lớn nhất thế giới?",
                "Ấn Độ", "Trung Quốc", "Hoa Kỳ", "Nga", "Ấn Độ"));

            questions.Add(new QuestionItem("Trái đất quay quanh gì?",
                "Mặt trời", "Mặt trăng", "Sao Hỏa", "Sao Kim", "Mặt trời"));

            questions.Add(new QuestionItem("1 + 1 = ?",
                "1", "2", "3", "4", "2"));

            questions.Add(new QuestionItem("Ngôn ngữ lập trình nào thuộc .NET?",
                "Python", "C#", "Java", "C++", "C#"));

            questions.Add(new QuestionItem("Phím tắt copy là gì?",
                "Ctrl + V", "Ctrl + C", "Ctrl + X", "Shift + C", "Ctrl + C"));
        }

        private void ShowQuestion()
        {
            if (currentIndex >= questions.Count)
            {
                EndGame();
                return;
            }

            var q = questions[currentIndex];

            lblQuestion.Text = $"Câu {currentIndex + 1}: " + q.Question;
            btnA.Text = q.AnsA;
            btnB.Text = q.AnsB;
            btnC.Text = q.AnsC;
            btnD.Text = q.AnsD;

            lblScore.Text = "Điểm: " + score;
        }

        private void CheckAnswer(string chosen)
        {
            var q = questions[currentIndex];

            if (chosen == q.Correct)
            {
                score++;
                MessageBox.Show("Đúng!", "Kết quả");
            }
            else
            {
                MessageBox.Show("Sai!\nĐáp án đúng: " + q.Correct);
            }

            currentIndex++;
            ShowQuestion();
        }

        private void EndGame()
        {
            MessageBox.Show($"Hoàn thành bài thi!\nĐiểm của bạn: {score}/{questions.Count}");
            this.Close();
        }

        // Sự kiện click 4 đáp án
        private void btnA_Click(object sender, EventArgs e) => CheckAnswer(btnA.Text);
        private void btnB_Click(object sender, EventArgs e) => CheckAnswer(btnB.Text);
        private void btnC_Click(object sender, EventArgs e) => CheckAnswer(btnC.Text);
        private void btnD_Click(object sender, EventArgs e) => CheckAnswer(btnD.Text);
    }

    // CLASS CÂU HỎI
    public class QuestionItem
    {
        public string Question;
        public string AnsA;
        public string AnsB;
        public string AnsC;
        public string AnsD;
        public string Correct;

        public QuestionItem(string q, string a, string b, string c, string d, string correct)
        {
            Question = q;
            AnsA = a;
            AnsB = b;
            AnsC = c;
            AnsD = d;
            Correct = correct;
        }
    }
}
