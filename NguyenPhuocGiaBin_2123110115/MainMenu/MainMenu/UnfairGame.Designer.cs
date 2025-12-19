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
            this.components = new Container();
            this.SuspendLayout();

            // 
            // UnfairGame
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Black;
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            this.Name = "UnfairGame";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Almost There";

            this.ResumeLayout(false);
        }

        #endregion
    }
}
