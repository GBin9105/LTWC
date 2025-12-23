using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    partial class UnfairGame
    {
        private IContainer components = null;

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
            SuspendLayout();
            // 
            // UnfairGame
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(1280, 720);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "UnfairGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Almost There";
            Load += UnfairGame_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}
