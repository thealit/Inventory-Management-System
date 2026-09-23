using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System.Components
{
    public partial class SearchBar : UserControl
    {
        public event EventHandler? InputChanged;

        public SearchBar()
        {
            InitializeComponent();

            textBox1.TextChanged += (s, e) => InputChanged?.Invoke(this, e);

            if (string.IsNullOrEmpty(textBox1.Text))
                textBox1.Leave += (s, e) => Pbx_IconSearch.Focus();

            Pbx_IconSearch.Focus();
        }


        public string Input
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string PlaceholderText
        {
            get { return textBox1.PlaceholderText; }
            set { textBox1.PlaceholderText = value; }
        }


        private Color _txtBxBgColor = Color.White;
        [Browsable(true)]
        public Color TextBoxBackColor
        {
            get { return _txtBxBgColor; }
            set
            {
                if (_txtBxBgColor == value) return;
                _txtBxBgColor = value;
                textBox1.BackColor = value;
                customPanel1.BackColor = value;
                Invalidate();
            }
        }

        private void Pbx_IconClear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                Pbx_IconSearch.Focus();
                textBox1.PlaceholderText = PlaceholderText;
                return;
            }
            textBox1.Clear();
            Pbx_IconSearch.Focus();
            InputChanged?.Invoke(this, EventArgs.Empty);
        }

        private void customPanel1_MouseLeave(object sender, EventArgs e)
        {
            Pbx_IconSearch.Focus();
        }
    }
}
