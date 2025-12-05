using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenu
{
    public static class AnimationHelper
    {
        public static async Task Flash(Control ctrl)
        {
            Color old = ctrl.BackColor;

            for (int i = 0; i < 3; i++)
            {
                ctrl.BackColor = Color.White;
                await Task.Delay(60);
                ctrl.BackColor = old;
                await Task.Delay(60);
            }
        }

        public static async Task Shrink(Control ctrl)
        {
            for (int i = 0; i < 5; i++)
            {
                ctrl.Width -= 3;
                ctrl.Height -= 3;
                ctrl.Left += 2;
                ctrl.Top += 2;
                await Task.Delay(20);
            }
        }

        public static async Task Grow(Control ctrl)
        {
            for (int i = 0; i < 5; i++)
            {
                ctrl.Width += 3;
                ctrl.Height += 3;
                ctrl.Left -= 2;
                ctrl.Top -= 2;
                await Task.Delay(20);
            }
        }

        public static async Task TransformAnimation(Control ctrl, Action changeFormAction)
        {
            await Flash(ctrl);
            await Shrink(ctrl);

            changeFormAction();

            await Grow(ctrl);
        }
    }
}
