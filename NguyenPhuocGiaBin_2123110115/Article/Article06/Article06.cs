using System;
using System.Drawing;
using System.Windows.Forms;

namespace Article06
{
    public partial class Article06 : Form
    {
        public Article06()
        {
            InitializeComponent();
        }

        // CLICK BUTTON
        private void bt_OK_Click(object sender, EventArgs e)
        {
            this.Text = "Article for Button";
            this.Size = new Size(500, 500);
        }
    }
}
