using Inventory_Management_System.Models;
using Microsoft.Identity.Client;

namespace Inventory_Management_System
{
    public partial class form_loginSignUp : Form
    {
        private SignInPanel _signInPanel;
        private SignUp _signUpPanel;

        public form_loginSignUp()
        {
            InitializeComponent();
            

            System.IO.MemoryStream boxIcon = new(Properties.Resources.icons8_box_100);
            this.Icon = new System.Drawing.Icon(boxIcon);

            this.DoubleBuffered = true;

            _signInPanel = new SignInPanel(this);
            _signUpPanel = new SignUp(this);

            ShowPanel(_signInPanel);
        }



        public void ShowSignIn() => ShowPanel(_signInPanel);
        public void ShowSignUp() {

            _signUpPanel.RewireInputEvents();
            ShowPanel(_signUpPanel);
        }


        public void ShowPanel(UserControl subPanel)
        {
            panel1.Controls.Clear();

            // get main panel dimensions
            int mainPanelWidth = panel1.Width;
            int mainPanelHeight = panel1.Height;

            // get subPanel dimensions
            int subPanelWidth = subPanel.Width;
            int subPanelHeight = subPanel.Height;

            // set padding to center the subPanel
            int paddingLeftRight = (mainPanelWidth - subPanelWidth) / 2;
            int paddingTopBottom = (mainPanelHeight - subPanelHeight) / 2;

            panel1.Padding = new Padding(paddingLeftRight, paddingTopBottom, paddingLeftRight, paddingTopBottom);

            subPanel.Dock = DockStyle.Fill;
            panel1.Controls.Add(subPanel);
        }

        public void ResetPanels()
        {
            // dispose old panels and recreate them clean
            _signInPanel?.Dispose();
            _signUpPanel?.Dispose();

            _signInPanel = new SignInPanel(this);
            _signUpPanel = new SignUp(this);
        }


       
    }
}
