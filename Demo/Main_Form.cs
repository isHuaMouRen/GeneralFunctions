using System;
using System.Windows.Forms;

using ToolLib.PosSelectorLib;

namespace Demo
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }

        private void button_PosSelector_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PosSelector.Show() + "");

        }
    }
}
