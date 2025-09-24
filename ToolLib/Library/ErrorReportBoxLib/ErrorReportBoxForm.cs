using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToolLib.Library.ErrorReportBoxLib
{
    public partial class ErrorReportBoxForm : Form
    {
        public string title;
        public string text;
        public bool showCloseButton;
        public Exception ex;

        public bool showInfo = false;



        public ErrorReportBoxForm()
        {
            InitializeComponent();

            
        }

        private void button_ShowInfo_Click(object sender, System.EventArgs e)
        {
            showInfo = !showInfo;

            if (showInfo)
            {
                button_ShowInfo.Text = "<<隐藏详细";
                this.Height = 320;
                textBox_Ex.Visible = true;
            }
            else
            {
                button_ShowInfo.Text = "详细信息>>";
                this.Height = 130;
                textBox_Ex.Visible = false;
            }
        }

        private void button_Continue_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_Exit_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void ErrorReportBoxForm_Load(object sender, System.EventArgs e)
        {
            pictureBox_Icon.Image = SystemIcons.Error.ToBitmap();

            this.Text = title;
            label_Text.Text = text;

            textBox_Ex.Text = $"{ex}";

            if (showCloseButton)
            {
                button_Exit.Visible = true;
                button_Exit.Enabled = true;
            }
            else
            {
                button_Exit.Visible = false;
                button_Exit.Enabled = false;
            }
        }
    }
}
