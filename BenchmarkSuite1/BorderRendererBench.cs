using System;
using System.Drawing;
using System.Windows.Forms;
using BenchmarkDotNet.Attributes;
using Inventory_Management_System.Components;
using Inventory_Management_System.DesignRenderers;
using Microsoft.VSDiagnostics;

namespace Inventory_Management_System.Benchmarks
{
    [CPUUsageDiagnoser]
    public class BorderRendererBench
    {
        private CustomPanel _panel = null !;
        private PaintEventArgs _pevent = null !;
        [GlobalSetup]
        public void Setup()
        {
            _panel = new CustomPanel();
            _panel.Width = 300;
            _panel.Height = 200;
            _panel.BorderSize = 4;
            _panel.BorderRadius = 12;
            _panel.BackColor = Color.White;
            var bmp = new Bitmap(_panel.Width, _panel.Height);
            var g = Graphics.FromImage(bmp);
            _pevent = new PaintEventArgs(g, new Rectangle(0, 0, bmp.Width, bmp.Height));
        }

        [Benchmark]
        public void DrawBorderBench()
        {
            BorderRenderer.DrawBorder(_panel, _pevent, _panel);
        }
    }
}