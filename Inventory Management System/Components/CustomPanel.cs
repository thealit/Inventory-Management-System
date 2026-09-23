using Inventory_Management_System.DesignRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Components
{
    public class CustomPanel : Panel, IBorderStyle
    {
        private int _borderSize = 0;
        private int _borderRadius = 0;
        private Color _borderColor = Color.PaleVioletRed;
        // caching initial data for reuse
        private System.Drawing.Region? _cachedRegion;
        private System.Drawing.Drawing2D.GraphicsPath? _cachedPath;
        private Rectangle _cachedRect;
        // caching initial data for reuse

        public CustomPanel()
        {
            // Turn on double buffering for this specific custom control
            this.DoubleBuffered = true;

            // This strict style setup completely prevents background flickering during custom painting
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor,
                true);

            this.UpdateStyles();
        }


        [Category("Border Styles")]
        public int BorderSize
        {
            get { return _borderSize; }
            set
            {
                _borderSize = value;
                InvalidateCache(); // caching initial data for reuse
                this.Invalidate();
            }
        }

        [Category("Border Styles")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                InvalidateCache(); // caching initial data for reuse
                this.Invalidate();
            }
        }

        [Category("Border Styles")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }


        [Category("Border Styles")]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //BorderRenderer.DrawBorder(this, pevent, this); //orig
            
            // caching initial data for reuse
            // Check if cache needs to be invalidated due to size change
            Rectangle currentRect = this.ClientRectangle;
            if (_cachedRect != currentRect)
            {
                InvalidateCache();
                _cachedRect = currentRect;
            }

            BorderRenderer.DrawBorder(this, pevent, this, _cachedPath, _cachedRegion);

            // Cache the path and region after drawing
            if (_cachedPath == null || _cachedRegion == null)
            {
                _cachedPath = BorderRenderer.GetCachedPath(this, _borderRadius, _borderSize);
                if (_cachedPath != null) 
                    _cachedRegion = new System.Drawing.Region(_cachedPath);
            }
        }

        private void InvalidateCache()
        {
            _cachedRegion?.Dispose();
            _cachedPath?.Dispose();
            _cachedRegion = null;
            _cachedPath = null;
        }
        // caching initial data for reuse


    }
}
