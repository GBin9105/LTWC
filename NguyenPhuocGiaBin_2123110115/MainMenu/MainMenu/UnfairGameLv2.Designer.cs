using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MainMenu
{
    partial class UnfairGameLv2
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
            this.components = new Container();
            this.SuspendLayout();
            // 
            // UnfairGameLv2
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Black;
            this.ClientSize = new Size(1280, 720);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "UnfairGameLv2";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Unfair Game - Level 2";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
