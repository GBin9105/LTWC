using System;
using System.Windows.Forms;

namespace Article10
{
    public partial class Form1 : Form
    {
        decimal workingMemory = 0;
        string opr = "";

        public Form1()
        {
            InitializeComponent();
        }

        // ===== BUTTON NUMBER =====
        private void bt0_Click(object sender, EventArgs e)
        {
            tbDisplay.Text += "0";
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            tbDisplay.Text += "1";
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            tbDisplay.Text += "2";
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            tbDisplay.Text += "3";
        }

        // ===== OPERATOR =====
        private void btPlus_Click(object sender, EventArgs e)
        {
            opr = "+";
            workingMemory = decimal.Parse(tbDisplay.Text);
            tbDisplay.Clear();
        }

        private void btMul_Click(object sender, EventArgs e)
        {
            opr = "*";
            workingMemory = decimal.Parse(tbDisplay.Text);
            tbDisplay.Clear();
        }

        // ===== EQUAL =====
        private void btEquals_Click(object sender, EventArgs e)
        {
            decimal secondValue = decimal.Parse(tbDisplay.Text);

            if (opr == "+")
                tbDisplay.Text = (workingMemory + secondValue).ToString();

            if (opr == "*")
                tbDisplay.Text = (workingMemory * secondValue).ToString();
        }
    }
}
