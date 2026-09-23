using Inventory_Management_System.DesignRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class StocksOverviewTile : UserControl, IBorderStyle
    {

        private string? _tileHeader;
        private string? _tileDescription;
        private int _tileCount;

        private int _borderSize = 0;
        private int _borderRadius = 0;
        private Color _borderColor = Color.PaleVioletRed;

        public StocksOverviewTile()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        public StocksOverviewTile(string header, string description, int itemCount)
        {
            TileHeader = header;
            TileDescription = description;
            TileItemCount = itemCount;
        }

        public string? TileHeader
        {
            get { return _tileHeader; }
            set { 
                _tileHeader = value;    
                lbl_TileHeader.Text = value; 
            }
        }

        public string? TileDescription
        {
            get { return _tileDescription; }
            set { 
                _tileDescription = value;
                lbl_TileDescription.Text = value; 
            }
        }

        public Color TileImagePanelColor
        {
            get { return cPnl_TileImage.BackColor; }
            set
            {
                cPnl_TileImage.BackColor = value;
            }
        }

        public Image TileImage
        {
            get { return pbx_Icon.Image; }
            set
            {
                pbx_Icon.Image = value;
            }
        }

        [Browsable(false)]
        public int TileItemCount
        {
            get { return _tileCount; }
            set {
                _tileCount = value;
                lbl_TileItemCount.Text = value.ToString(); 
            }
        }

        


        [Category("Border Styles")]
        public int BorderSize
        {
            get { return _borderSize; }
            set
            {
                _borderSize = value;
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
            BorderRenderer.DrawBorder(this, pevent, this);
        }

    }
}
