namespace ToolLib.Library.ErrorReportBoxLib
{
    partial class ErrorReportBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.label_Text = new System.Windows.Forms.Label();
            this.textBox_Ex = new System.Windows.Forms.TextBox();
            this.button_ShowInfo = new System.Windows.Forms.Button();
            this.button_Continue = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Icon
            // 
            this.pictureBox_Icon.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Icon.TabIndex = 0;
            this.pictureBox_Icon.TabStop = false;
            // 
            // label_Text
            // 
            this.label_Text.Location = new System.Drawing.Point(50, 12);
            this.label_Text.Name = "label_Text";
            this.label_Text.Size = new System.Drawing.Size(472, 46);
            this.label_Text.TabIndex = 1;
            this.label_Text.Text = "text";
            // 
            // textBox_Ex
            // 
            this.textBox_Ex.Location = new System.Drawing.Point(12, 90);
            this.textBox_Ex.MaxLength = 2147483647;
            this.textBox_Ex.Multiline = true;
            this.textBox_Ex.Name = "textBox_Ex";
            this.textBox_Ex.ReadOnly = true;
            this.textBox_Ex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Ex.Size = new System.Drawing.Size(510, 179);
            this.textBox_Ex.TabIndex = 2;
            this.textBox_Ex.Visible = false;
            // 
            // button_ShowInfo
            // 
            this.button_ShowInfo.Location = new System.Drawing.Point(12, 61);
            this.button_ShowInfo.Name = "button_ShowInfo";
            this.button_ShowInfo.Size = new System.Drawing.Size(117, 23);
            this.button_ShowInfo.TabIndex = 3;
            this.button_ShowInfo.Text = "详细信息>>";
            this.button_ShowInfo.UseVisualStyleBackColor = true;
            this.button_ShowInfo.Click += new System.EventHandler(this.button_ShowInfo_Click);
            // 
            // button_Continue
            // 
            this.button_Continue.Location = new System.Drawing.Point(428, 61);
            this.button_Continue.Name = "button_Continue";
            this.button_Continue.Size = new System.Drawing.Size(94, 23);
            this.button_Continue.TabIndex = 4;
            this.button_Continue.Text = "继续";
            this.button_Continue.UseVisualStyleBackColor = true;
            this.button_Continue.Click += new System.EventHandler(this.button_Continue_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(328, 61);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(94, 23);
            this.button_Exit.TabIndex = 5;
            this.button_Exit.Text = "退出";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // ErrorReportBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 91);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Continue);
            this.Controls.Add(this.button_ShowInfo);
            this.Controls.Add(this.textBox_Ex);
            this.Controls.Add(this.label_Text);
            this.Controls.Add(this.pictureBox_Icon);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorReportBoxForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "title";
            this.Load += new System.EventHandler(this.ErrorReportBoxForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Icon;
        private System.Windows.Forms.Label label_Text;
        private System.Windows.Forms.TextBox textBox_Ex;
        private System.Windows.Forms.Button button_ShowInfo;
        private System.Windows.Forms.Button button_Continue;
        private System.Windows.Forms.Button button_Exit;
    }
}