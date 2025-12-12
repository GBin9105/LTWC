using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenu
{
    public static class AnimationHelper
    {
        // =============================================================
        // FLASH EFFECT (chớp sáng)
        // =============================================================
        public static async Task Flash(Control ctrl)
        {
            Color original = ctrl.BackColor;

            for (int i = 0; i < 3; i++)
            {
                ctrl.BackColor = Color.White;
                await Task.Delay(70);

                ctrl.BackColor = original;
                await Task.Delay(70);
            }
        }

        // =============================================================
        // SHRINK (thu nhỏ mượt)
        // =============================================================
        public static async Task Shrink(Control ctrl)
        {
            for (int i = 0; i < 5; i++)
            {
                ctrl.Width -= 4;
                ctrl.Height -= 4;

                ctrl.Left += 2;
                ctrl.Top += 2;

                await Task.Delay(18);
            }
        }

        // =============================================================
        // GROW (mở rộng lại)
        // =============================================================
        public static async Task Grow(Control ctrl)
        {
            for (int i = 0; i < 5; i++)
            {
                ctrl.Width += 4;
                ctrl.Height += 4;

                ctrl.Left -= 2;
                ctrl.Top -= 2;

                await Task.Delay(18);
            }
        }

        // =============================================================
        // FIX POSITION (ngừa bị lệch 1px do float rounding)
        // =============================================================
        private static void ResetPositionFix(Control ctrl)
        {
            // làm tròn về số nguyên
            ctrl.Left = Convert.ToInt32(ctrl.Left);
            ctrl.Top = Convert.ToInt32(ctrl.Top);


            // nếu control có kích thước âm hoặc 0 → sửa lại để không crash
            if (ctrl.Width < 1) ctrl.Width = 1;
            if (ctrl.Height < 1) ctrl.Height = 1;
        }

        // =============================================================
        // MAIN TRANSFORM ANIMATION
        // =============================================================
        public static async Task TransformAnimation(Control ctrl, Action changeAction)
        {
            await Flash(ctrl);          // hiệu ứng nhấp nháy
            await Shrink(ctrl);         // thu nhỏ

            changeAction?.Invoke();     // đổi dạng player

            await Grow(ctrl);           // phóng to lại

            ResetPositionFix(ctrl);     // sửa sai số nếu có
        }
    }
}
