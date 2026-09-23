using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Helpers
{
    public static class CenterControlOnLoad
    {
        private static Control? _currentVisibleControl;
        private static Form? _parentForm; // track form for click detection
        public static event EventHandler? ClickOutsideDetected;

        public static void ShowCentered(Control control)
        {
            if (control == null) return;

            var parent = control.Parent;
            if (parent == null) return;

            // Get top-level form
            Control topLevel = parent;
            while (topLevel.Parent != null)
                topLevel = topLevel.Parent;

            if (topLevel is not Form form) return;

            // Hide previous panel
            if (_currentVisibleControl != null && _currentVisibleControl != control)
            {
                _currentVisibleControl.Hide();
            }

            // Center
            void Center()
            {
                control.SuspendLayout();
                control.Location = new Point(
                    (parent.ClientSize.Width - control.Width) / 2,
                    (parent.ClientSize.Height - control.Height) / 2
                );
                control.ResumeLayout();
            }

            Center();

            // Resize handling
            form.Resize -= Form_Resize;
            form.Resize += Form_Resize;

            void Form_Resize(object? sender, EventArgs e)
            {
                Center();
            }

            // click outside detection
            if (_parentForm != null)
            {
                UnwireClick(_parentForm); // remove old handlers
            }

            _parentForm = form;
            WireClick(form);

            control.Show();
            control.BringToFront();

            _currentVisibleControl = control;
        }

        // detect outside click
        private static void OnFormMouseDown(object? sender, MouseEventArgs e)
        {
            if (_currentVisibleControl == null || _parentForm == null) return;

            Point mouse = _currentVisibleControl.PointToClient(Control.MousePosition);

            if (!_currentVisibleControl.ClientRectangle.Contains(mouse))
            {
                
                ClickOutsideDetected?.Invoke(_currentVisibleControl, EventArgs.Empty);
                HideCurrent();
            }
        }

        // attach to all controls recursively
        private static void WireClick(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.MouseDown += OnFormMouseDown;
                WireClick(ctrl);
            }

            parent.MouseDown += OnFormMouseDown;
        }

        // remove handlers
        private static void UnwireClick(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.MouseDown -= OnFormMouseDown;
                UnwireClick(ctrl);
            }

            parent.MouseDown -= OnFormMouseDown;
        }

        // hide method
        public static void HideCurrent()
        {
            if (_currentVisibleControl != null)
            {
                _currentVisibleControl.Hide();
                _currentVisibleControl = null;
            }

            if (_parentForm != null)
            {
                UnwireClick(_parentForm);
                _parentForm = null;
            }
        }
    }
}
