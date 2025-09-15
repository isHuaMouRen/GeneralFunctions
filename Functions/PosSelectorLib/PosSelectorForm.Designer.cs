namespace Debug_NewLibTest.Utils.PosSelectorLib
{
    partial class PosSelectorForm
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
            this.components = new System.ComponentModel.Container();
            this.panel_X = new System.Windows.Forms.Panel();
            this.panel_Y = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel_X
            // 
            this.panel_X.BackColor = System.Drawing.Color.Red;
            this.panel_X.Location = new System.Drawing.Point(308, 29);
            this.panel_X.Name = "panel_X";
            this.panel_X.Size = new System.Drawing.Size(2, 331);
            this.panel_X.TabIndex = 0;
            this.panel_X.Click += new System.EventHandler(this.AnyClick);
            // 
            // panel_Y
            // 
            this.panel_Y.BackColor = System.Drawing.Color.Red;
            this.panel_Y.Location = new System.Drawing.Point(326, 171);
            this.panel_Y.Name = "panel_Y";
            this.panel_Y.Size = new System.Drawing.Size(300, 2);
            this.panel_Y.TabIndex = 1;
            this.panel_Y.Click += new System.EventHandler(this.AnyClick);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(464, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "坐标: x, x";
            this.label1.Click += new System.EventHandler(this.AnyClick);
            // 
            // PosSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(749, 404);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel_Y);
            this.Controls.Add(this.panel_X);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PosSelectorForm";
            this.Opacity = 0.2D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PosSelectorForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PosSelectorForm_Load);
            this.Click += new System.EventHandler(this.AnyClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_X;
        private System.Windows.Forms.Panel panel_Y;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label1;
    }
}