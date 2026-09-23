using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Components
{
    public class GradientPanelLogin : Panel
    {
        public Color gradientTop { get; set; }
        public Color gradientBottom { get; set; }

        public GradientPanelLogin()
        {
            Resize += GradientPanelLogin_Resize;
            this.DoubleBuffered = true;
        }

        private void GradientPanelLogin_Resize(object? sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush linear = new LinearGradientBrush (
                ClientRectangle, gradientTop, gradientBottom, 90);

            Graphics graphics = e.Graphics;

            graphics.FillRectangle(linear, ClientRectangle);

            base.OnPaint(e);
        }
    }
}
