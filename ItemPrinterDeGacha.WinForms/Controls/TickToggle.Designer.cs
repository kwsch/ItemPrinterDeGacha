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
            GB_Time = new GroupBox();
            NUD_Time = new NumericUpDown();
            RB_TimeSpecific = new RadioButton();
            RB_TimeCurrent = new RadioButton();
            GB_Time.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_Time).BeginInit();
            SuspendLayout();
            // 
            // GB_Time
            // 
            GB_Time.Controls.Add(NUD_Time);
            GB_Time.Controls.Add(RB_TimeSpecific);
            GB_Time.Controls.Add(RB_TimeCurrent);
            GB_Time.Dock = DockStyle.Fill;
            GB_Time.Location = new Point(0, 0);
            GB_Time.Name = "GB_Time";
            GB_Time.Size = new Size(112, 83);
            GB_Time.TabIndex = 2;
            GB_Time.TabStop = false;
            GB_Time.Text = "Time";
            // 
            // NUD_Time
            // 
            NUD_Time.Enabled = false;
            NUD_Time.Location = new Point(6, 54);
            NUD_Time.Name = "NUD_Time";
            NUD_Time.Size = new Size(100, 23);
            NUD_Time.TabIndex = 2;
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
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(GB_Time);
            Name = "TickToggle";
            Size = new Size(112, 83);
            GB_Time.ResumeLayout(false);
            GB_Time.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_Time).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GB_Time;
        private RadioButton RB_TimeSpecific;
        private RadioButton RB_TimeCurrent;
        private NumericUpDown NUD_Time;
    }
}
