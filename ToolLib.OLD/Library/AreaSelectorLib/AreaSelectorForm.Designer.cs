namespace ToolLib.AreaSelectorLib
{
    partial class AreaSelectorForm
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
            this.panel_Area = new System.Windows.Forms.Panel();
            this.label_Tip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel_X
            // 
            this.panel_X.BackColor = System.Drawing.Color.Red;
            this.panel_X.Location = new System.Drawing.Point(209, 138);
            this.panel_X.Name = "panel_X";
            this.panel_X.Size = new System.Drawing.Size(100, 2);
            this.panel_X.TabIndex = 0;
            this.panel_X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnyDown);
            this.panel_X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AnyUp);
            // 
            // panel_Y
            // 
            this.panel_Y.BackColor = System.Drawing.Color.Red;
            this.panel_Y.Location = new System.Drawing.Point(319, 196);
            this.panel_Y.Name = "panel_Y";
            this.panel_Y.Size = new System.Drawing.Size(2, 100);
            this.panel_Y.TabIndex = 1;
            this.panel_Y.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnyDown);
            this.panel_Y.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AnyUp);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // panel_Area
            // 
            this.panel_Area.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Area.Location = new System.Drawing.Point(350, 181);
            this.panel_Area.Name = "panel_Area";
            this.panel_Area.Size = new System.Drawing.Size(200, 100);
            this.panel_Area.TabIndex = 2;
            this.panel_Area.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnyDown);
            this.panel_Area.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AnyUp);
            // 
            // label_Tip
            // 
            this.label_Tip.Location = new System.Drawing.Point(98, 70);
            this.label_Tip.Name = "label_Tip";
            this.label_Tip.Size = new System.Drawing.Size(452, 34);
            this.label_Tip.TabIndex = 3;
            this.label_Tip.Text = "坐标:  {x} , {y}\r\n请选择第一个坐标点";
            this.label_Tip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnyDown);
            this.label_Tip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AnyUp);
            // 
            // AreaSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 395);
            this.Controls.Add(this.label_Tip);
            this.Controls.Add(this.panel_Area);
            this.Controls.Add(this.panel_Y);
            this.Controls.Add(this.panel_X);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AreaSelectorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AreaSelectorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_X;
        private System.Windows.Forms.Panel panel_Y;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panel_Area;
        private System.Windows.Forms.Label label_Tip;
    }
}