namespace ItemPrinterDeGacha.WinForms.Controls
{
    partial class TickToggle
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            TB_Time = new TextBox();
            RB_TimeSpecific = new RadioButton();
            RB_TimeCurrent = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TB_Time);
            groupBox1.Controls.Add(RB_TimeSpecific);
            groupBox1.Controls.Add(RB_TimeCurrent);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(112, 83);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Time";
            // 
            // TB_Time
            // 
            TB_Time.Location = new Point(6, 54);
            TB_Time.Name = "TB_Time";
            TB_Time.ReadOnly = true;
            TB_Time.Size = new Size(100, 23);
            TB_Time.TabIndex = 2;
            // 
            // RB_TimeSpecific
            // 
            RB_TimeSpecific.AutoSize = true;
            RB_TimeSpecific.Location = new Point(6, 32);
            RB_TimeSpecific.Name = "RB_TimeSpecific";
            RB_TimeSpecific.Size = new Size(95, 19);
            RB_TimeSpecific.TabIndex = 1;
            RB_TimeSpecific.TabStop = true;
            RB_TimeSpecific.Text = "Specific Ticks";
            RB_TimeSpecific.UseVisualStyleBackColor = true;
            RB_TimeSpecific.CheckedChanged += ChangeTime;
            // 
            // RB_TimeCurrent
            // 
            RB_TimeCurrent.AutoSize = true;
            RB_TimeCurrent.Checked = true;
            RB_TimeCurrent.Location = new Point(6, 13);
            RB_TimeCurrent.Name = "RB_TimeCurrent";
            RB_TimeCurrent.Size = new Size(94, 19);
            RB_TimeCurrent.TabIndex = 0;
            RB_TimeCurrent.TabStop = true;
            RB_TimeCurrent.Text = "Current Time";
            RB_TimeCurrent.UseVisualStyleBackColor = true;
            RB_TimeCurrent.CheckedChanged += ChangeTime;
            // 
            // TickToggle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "TickToggle";
            Size = new Size(112, 83);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox TB_Time;
        private RadioButton RB_TimeSpecific;
        private RadioButton RB_TimeCurrent;
    }
}
