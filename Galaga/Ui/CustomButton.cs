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
        protected override void OnPaint(PaintEventArgs pevent)

        {

            base.OnPaint(pevent);

            //pevent.Graphics.FillRectangle(new SolidBrush(ButtonColor), 0, 0, Width, Height);
            pevent.Graphics.DrawRectangle(new Pen(Color.White, 3), new Rectangle(0, 0, Width, Height));
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            TextRenderer.DrawText(pevent.Graphics, Text, Font, new Point(Width + 3, Height / 2), ForeColor, flags);

        }


        protected override void OnMouseDown(MouseEventArgs mevent)

        {

            base.OnMouseDown(mevent);
            
            this.BackColor = Color.FromArgb(100, Color.White);
            this.ForeColor = Color.Black;

            Invalidate();

        }

        protected override void OnMouseUp(MouseEventArgs mevent)

        {

            base.OnMouseUp(mevent);
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;

            Invalidate();

        }

    }
}
