using System;
using System.IO;
using System.Windows.Forms;

namespace Article05
{
    public partial class Article05 : Form
    {
        string path = Application.StartupPath + @"\Key_Logger.txt";

        public Article05()
        {
            InitializeComponent();
            this.KeyPreview = true; // BẮT BUỘC
        }

        // BẮT PHÍM
        private void Article05_KeyUp(object sender, KeyEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{DateTime.Now:HH:mm:ss} - {e.KeyCode}");
            }
        }
    }
}
