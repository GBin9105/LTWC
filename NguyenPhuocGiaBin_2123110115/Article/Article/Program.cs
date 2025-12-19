using System;
using System.Windows.Forms;

namespace Article02
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Article02());
        }
    }
}
