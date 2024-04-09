namespace ItemPrinterDeGacha
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            TB_Time = new TextBox();
            RB_TimeSpecific = new RadioButton();
            RB_TimeCurrent = new RadioButton();
            NUD_ItemID = new NumericUpDown();
            CB_Mode = new ComboBox();
            CB_TargetMode = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            L_Target = new Label();
            RTB_Result = new RichTextBox();
            B_Generate = new Button();
            label4 = new Label();
            CB_Seek = new ComboBox();
            NUD_Seconds = new NumericUpDown();
            label3 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_ItemID).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Seconds).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TB_Time);
            groupBox1.Controls.Add(RB_TimeSpecific);
            groupBox1.Controls.Add(RB_TimeCurrent);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(112, 84);
            groupBox1.TabIndex = 1;
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
            RB_TimeSpecific.CheckedChanged += ChangeTimeMode;
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
            RB_TimeCurrent.CheckedChanged += ChangeTimeMode;
            // 
            // NUD_ItemID
            // 
            NUD_ItemID.Location = new Point(526, 45);
            NUD_ItemID.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            NUD_ItemID.Name = "NUD_ItemID";
            NUD_ItemID.Size = new Size(80, 23);
            NUD_ItemID.TabIndex = 2;
            NUD_ItemID.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CB_Mode
            // 
            CB_Mode.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_Mode.FormattingEnabled = true;
            CB_Mode.Items.AddRange(new object[] { "Regular", "ItemBonus", "BallBonus" });
            CB_Mode.Location = new Point(198, 20);
            CB_Mode.Name = "CB_Mode";
            CB_Mode.Size = new Size(100, 23);
            CB_Mode.TabIndex = 0;
            CB_Mode.SelectedIndexChanged += CB_Mode_SelectedIndexChanged;
            // 
            // CB_TargetMode
            // 
            CB_TargetMode.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_TargetMode.FormattingEnabled = true;
            CB_TargetMode.Items.AddRange(new object[] { "ItemBonus", "BallBonus" });
            CB_TargetMode.Location = new Point(370, 20);
            CB_TargetMode.Name = "CB_TargetMode";
            CB_TargetMode.Size = new Size(80, 23);
            CB_TargetMode.TabIndex = 4;
            // 
            // label1
            // 
            label1.Location = new Point(132, 19);
            label1.Name = "label1";
            label1.Size = new Size(60, 24);
            label1.TabIndex = 5;
            label1.Text = "Mode";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new Point(460, 45);
            label2.Name = "label2";
            label2.Size = new Size(60, 24);
            label2.TabIndex = 6;
            label2.Text = "Item ID";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // L_Target
            // 
            L_Target.Location = new Point(304, 19);
            L_Target.Name = "L_Target";
            L_Target.Size = new Size(60, 24);
            L_Target.TabIndex = 7;
            L_Target.Text = "Target";
            L_Target.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RTB_Result
            // 
            RTB_Result.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RTB_Result.Location = new Point(12, 102);
            RTB_Result.Name = "RTB_Result";
            RTB_Result.ReadOnly = true;
            RTB_Result.Size = new Size(674, 291);
            RTB_Result.TabIndex = 8;
            RTB_Result.Text = "";
            // 
            // B_Generate
            // 
            B_Generate.BackColor = SystemColors.ActiveCaption;
            B_Generate.Location = new Point(252, 51);
            B_Generate.Name = "B_Generate";
            B_Generate.Size = new Size(132, 45);
            B_Generate.TabIndex = 9;
            B_Generate.Text = "Generate";
            B_Generate.UseVisualStyleBackColor = false;
            B_Generate.Click += B_Generate_Click;
            // 
            // label4
            // 
            label4.Location = new Point(460, 20);
            label4.Name = "label4";
            label4.Size = new Size(60, 24);
            label4.TabIndex = 11;
            label4.Text = "Seek";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CB_Seek
            // 
            CB_Seek.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_Seek.FormattingEnabled = true;
            CB_Seek.Items.AddRange(new object[] { "Single", "MostSingle", "MostSpecial" });
            CB_Seek.Location = new Point(526, 19);
            CB_Seek.Name = "CB_Seek";
            CB_Seek.Size = new Size(80, 23);
            CB_Seek.TabIndex = 10;
            CB_Seek.SelectedIndexChanged += CB_Mode_SelectedIndexChanged;
            // 
            // NUD_Seconds
            // 
            NUD_Seconds.Location = new Point(526, 73);
            NUD_Seconds.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            NUD_Seconds.Name = "NUD_Seconds";
            NUD_Seconds.Size = new Size(80, 23);
            NUD_Seconds.TabIndex = 12;
            NUD_Seconds.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Location = new Point(460, 73);
            label3.Name = "label3";
            label3.Size = new Size(60, 24);
            label3.TabIndex = 13;
            label3.Text = "Seconds";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 405);
            Controls.Add(label3);
            Controls.Add(NUD_Seconds);
            Controls.Add(label4);
            Controls.Add(CB_Seek);
            Controls.Add(B_Generate);
            Controls.Add(RTB_Result);
            Controls.Add(L_Target);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(CB_Mode);
            Controls.Add(CB_TargetMode);
            Controls.Add(NUD_ItemID);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ItemPrinterDeGacha";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_ItemID).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Seconds).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox1;
        private TextBox TB_Time;
        private RadioButton RB_TimeSpecific;
        private RadioButton RB_TimeCurrent;
        private NumericUpDown NUD_ItemID;
        private ComboBox CB_Mode;
        private ComboBox CB_TargetMode;
        private Label label1;
        private Label label2;
        private Label L_Target;
        private RichTextBox RTB_Result;
        private Button B_Generate;
        private Label label4;
        private ComboBox CB_Seek;
        private NumericUpDown NUD_Seconds;
        private Label label3;
    }
}
