using System;
using System.Drawing;
using System.Windows.Forms;
using ToolLib.InputLib;

namespace ToolLib.Library.AreaSelectorLib
{
    public partial class AreaSelectorForm : Form
    {
        public int ClickNum = 0;

        public Rectangle rectangle;

        public AreaSelectorForm()
        {
            InitializeComponent();

            Size screenSize = Screen.PrimaryScreen.Bounds.Size;

            this.Location = new Point(0, 0);
            this.Size = screenSize;

            panel_X.Left = 0;
            panel_Y.Top = 0;

            panel_X.Width = screenSize.Width;
            panel_Y.Height = screenSize.Height;

            panel_Area.Location = new Point(0, 0);
            panel_Area.Size = new Size(0, 0);

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Point mousePos = InputManager.Mouse.GetMousePosition();

            panel_X.Top = mousePos.Y;
            panel_Y.Left = mousePos.X;

            if (ClickNum == 1)
            {
                panel_Area.Size = new Size(mousePos.X - panel_Area.Left, mousePos.Y - panel_Area.Top);

                label_Tip.Text = $"坐标: {mousePos.X} , {mousePos.Y}\n请选择终点";
            }
            else
            {
                label_Tip.Text = $"坐标: {mousePos.X} , {mousePos.Y}\n请选择起点";
            }
        }

        private void AnyClick(object sender,EventArgs e)
        {
            ClickNum = ClickNum + 1;
            if (ClickNum == 1)
            {
                panel_Area.Location = InputManager.Mouse.GetMousePosition();
            }
            else if (ClickNum == 2)
            {
                rectangle = new Rectangle(panel_Area.Location, panel_Area.Size);
                this.Close();
            }
        }
    }
}
