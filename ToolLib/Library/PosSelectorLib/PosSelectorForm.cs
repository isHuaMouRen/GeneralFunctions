using ToolLib.InputLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToolLib.PosSelectorLib
{
    public partial class PosSelectorForm : Form
    {
        public static Point Pos = new Point(0, 0);
        Point mousePos;

        public PosSelectorForm()
        {
            InitializeComponent();
        }

        private void PosSelectorForm_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.Location = new Point(0, 0);

            panel_X.Top = 0;
            panel_Y.Left = 0;

            panel_X.Height = this.Height;
            panel_Y.Width = this.Width;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            mousePos = InputManager.Mouse.GetMousePosition();

            panel_X.Left = mousePos.X;
            panel_Y.Top = mousePos.Y;

            label1.Left = mousePos.X + 20;
            label1.Top = mousePos.Y - 20;
            label1.Text = $"坐标: {mousePos.X}, {mousePos.Y}";
        }

        private void AnyClick(object sender, EventArgs e)
        {
            Pos = mousePos;
            this.Close();
        }
    }
}
