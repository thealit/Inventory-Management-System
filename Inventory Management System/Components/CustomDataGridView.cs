using Inventory_Management_System.DesignRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Components
{
    public partial class CustomDataGridView : DataGridView, IBorderStyle
    {
        // BorderStyle Fields
        private int _borderSize = 0;
        private int _borderRadius = 0;
        private Color _borderColor = Color.PaleVioletRed;


        public CustomDataGridView()
        {
            this.BorderStyle = BorderStyle.None;
            this.BackgroundColor = Color.White;
            this.DoubleBuffered = true;
        }


        // Border Style Properties
        [Category("Border Styles")]
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; this.Invalidate(); }
        }
        [Category("Border Styles")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value; this.Invalidate(); }
        }
        [Category("Border Styles")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

       
        // ── Overrides ─────────────────────────────────────────────────────────
        protected override void OnResize(EventArgs e) { 
            base.OnResize(e); 
            this.Invalidate();  
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            BorderRenderer.DrawBorder(this, pevent, this);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);


            // Force columns to recalculate fill-widths on every resize
            if (this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
            {
                this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            this.Invalidate();
            this.Update(); // force immediate repaint — prevents cropping artifact
        }
       
    }
}
