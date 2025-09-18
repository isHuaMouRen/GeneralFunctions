namespace Demo
{
    partial class Main_Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_PosSelector = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_PosSelector
            // 
            this.button_PosSelector.Location = new System.Drawing.Point(12, 12);
            this.button_PosSelector.Name = "button_PosSelector";
            this.button_PosSelector.Size = new System.Drawing.Size(172, 35);
            this.button_PosSelector.TabIndex = 0;
            this.button_PosSelector.Text = "PosSelectorLib";
            this.button_PosSelector.UseVisualStyleBackColor = true;
            this.button_PosSelector.Click += new System.EventHandler(this.button_PosSelector_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 387);
            this.Controls.Add(this.button_PosSelector);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeneralFunctionsDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_PosSelector;
    }
}

