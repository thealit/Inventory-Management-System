/**/using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.DesignRenderers
{
    public interface IBorderStyle
    {
        int BorderSize { get; set; }
        int BorderRadius { get; set; }
        Color BorderColor { get; set; }
    }


    public static class BorderRenderer
    {
        
        // Cache pens to reduce allocation overhead
        private static readonly Dictionary<string, Pen> _penCache = new();

        private static Pen GetCachedPen(Color color, float width, string key)
        {
            if (!_penCache.TryGetValue(key, out var pen))
            {
                pen = new Pen(color, width);
                _penCache[key] = pen;
            }
            return pen;
        }

        public static void DrawBorder(Control control, PaintEventArgs e, IBorderStyle style, GraphicsPath? cachedPath = null, Region? cachedRegion = null)
        {
            if (style.BorderSize <= 0) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int smoothSize = 2;
            if (style.BorderSize > 0) smoothSize = style.BorderSize;

            Rectangle rectSurface = control.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -style.BorderSize, -style.BorderSize);

            if (style.BorderRadius > 2)
            {
                GraphicsPath pathSurface = cachedPath ?? GetRoundedPath(rectSurface, style.BorderRadius);
                GraphicsPath pathBorder = GetRoundedPath(rectBorder, style.BorderRadius - style.BorderSize);

                string penSurfaceKey = $"surface_{control.Parent?.BackColor.ToArgb() ?? 0}_{smoothSize}";
                string penBorderKey = $"border_{style.BorderColor.ToArgb()}_{style.BorderSize}";

                Pen penSurface = GetCachedPen(control.Parent?.BackColor ?? Color.White, smoothSize, penSurfaceKey);
                Pen penBorder = GetCachedPen(style.BorderColor, style.BorderSize, penBorderKey);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if (cachedRegion != null)
                {
                    control.Region = cachedRegion;
                }
                else
                {
                    control.Region = new Region(pathSurface);
                }

                e.Graphics.DrawPath(penSurface, pathSurface);
                if (style.BorderSize >= 1)
                    e.Graphics.DrawPath(penBorder, pathBorder);

                // Only dispose paths we created (not cached ones)
                if (cachedPath == null)
                    pathSurface?.Dispose();
                pathBorder?.Dispose();
            }
            else
            {
                string penKey = $"rect_{style.BorderColor.ToArgb()}_{style.BorderSize}";
                Pen penBorder = GetCachedPen(style.BorderColor, style.BorderSize, penKey);
                penBorder.Alignment = PenAlignment.Inset;

                e.Graphics.DrawRectangle(
                    penBorder,
                    0, 0,
                    control.Width - 1,
                    control.Height - 1);
            }
        }

        public static GraphicsPath? GetCachedPath(Control control, int borderRadius, int borderSize)
        {
            if (borderRadius <=2) return null;

            Rectangle rectSurface = control.ClientRectangle;
            return GetRoundedPath(rectSurface, borderRadius);
        }

        private static GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            float maxRadius = Math.Min(rect.Width, rect.Height) / 2f;
            float actualRadius = Math.Min(radius, maxRadius);

            // corner curve
            float curve = radius * 2F;
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curve, curve, 180, 90);
            path.AddArc(rect.Right - curve, rect.Y, curve, curve, 270, 90);
            path.AddArc(rect.Right - curve, rect.Bottom - curve, curve, curve, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curve, curve, curve, 90, 90);
            path.CloseFigure();

            return path;
        }

    }
}
/**/

/*orig
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory_Management_System.DesignRenderers
{
    public interface IBorderStyle
    {
        int BorderSize { get; set; }
        int BorderRadius { get; set; }
        Color BorderColor { get; set; }
    }


    public static class BorderRenderer
    {
        public static void DrawBorder(Control control, PaintEventArgs e, IBorderStyle style)
        {
            if (style.BorderSize <= 0) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int smoothSize = 2;
            if (style.BorderSize > 0) smoothSize = style.BorderSize;


            Rectangle rectSurface = control.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -style.BorderSize, -style.BorderSize);

            if (style.BorderRadius > 2)
            {
                using var pathSurface = GetRoundedPath(rectSurface, style.BorderRadius);
                using var pathBorder = GetRoundedPath(rectBorder, style.BorderRadius - style.BorderSize);
                using Pen penSurface = new Pen(control.Parent.BackColor, smoothSize);
                using Pen penBorder = new Pen(style.BorderColor, style.BorderSize);
                
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                control.Region = new Region(pathSurface);
                

                e.Graphics.DrawPath(penSurface, pathSurface);
                if (style.BorderSize >= 1)
                    e.Graphics.DrawPath(penBorder, pathBorder);

                control.Region = new Region(pathSurface);
                e.Graphics.DrawPath(penBorder, pathBorder);
            }
            else
            {
                using var penBorder = new Pen(style.BorderColor, style.BorderSize)
                {
                    Alignment = PenAlignment.Inset
                };

                e.Graphics.DrawRectangle(
                    penBorder,
                    0, 0,
                    control.Width - 1,
                    control.Height - 1);
            }
        }

        private static GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            // corner curve
            float curve = radius * 2F;
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curve, curve, 180, 90);
            path.AddArc(rect.Right - curve, rect.Y, curve, curve, 270, 90);
            path.AddArc(rect.Right - curve, rect.Bottom - curve, curve, curve, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curve, curve, curve, 90, 90);
            path.CloseFigure();

            return path;
        }

    }
}
*/

