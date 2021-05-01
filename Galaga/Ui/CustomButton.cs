using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Galaga.Ui
{
    class CustomButton : Control
    {
        public CustomButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs eventArgs)

        {
            base.OnPaint(eventArgs);

            eventArgs.Graphics.DrawRectangle(new Pen(Color.White, 4), new Rectangle(0, 0, Width, Height));

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(eventArgs.Graphics, Text, Font, new Point(Width, Height / 2), ForeColor, flags);
        }


        protected override void OnMouseDown(MouseEventArgs eventArgs)

        {
            base.OnMouseDown(eventArgs);
            
            this.BackColor = Color.FromArgb(100, Color.White);
            this.ForeColor = Color.Black;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs eventArgs)

        {
            base.OnMouseUp(eventArgs);

            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;

            Invalidate();
        }

    }
}
