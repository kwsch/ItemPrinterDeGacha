namespace ItemPrinterDeGacha.WinForms.Controls
{
    partial class AdjacentViewer
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
            ADJ_N1 = new ItemResultGridView();
            ADJ_0 = new ItemResultGridView();
            ADJ_P1 = new ItemResultGridView();
            L_N1 = new Label();
            L_0 = new Label();
            L_P1 = new Label();
            B_MinusOne = new Button();
            B_AddOne = new Button();
            CB_Mode = new ComboBox();
            L_Mode = new Label();
            L_Seed = new Label();
            MTB_Seed = new MaskedTextBox();
            CB_Count = new ComboBox();
            SuspendLayout();
            // 
            // ADJ_N1
            // 
            ADJ_N1.AutoSize = true;
            ADJ_N1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ADJ_N1.Location = new Point(1, 83);
            ADJ_N1.Name = "ADJ_N1";
            ADJ_N1.Size = new Size(235, 250);
            ADJ_N1.TabIndex = 6;
            // 
            // ADJ_0
            //
            ADJ_0.AutoSize = true;
            ADJ_0.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ADJ_0.Location = new Point(237, 83);
            ADJ_0.Name = "ADJ_0";
            ADJ_0.Size = new Size(235, 250);
            ADJ_0.TabIndex = 7;
            // 
            // ADJ_P1
            //
            ADJ_0.AutoSize = true;
            ADJ_P1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ADJ_P1.Location = new Point(473, 83);
            ADJ_P1.Name = "ADJ_P1";
            ADJ_P1.Size = new Size(235, 250);
            ADJ_P1.TabIndex = 8;
            // 
            // L_N1
            // 
            L_N1.BorderStyle = BorderStyle.FixedSingle;
            L_N1.Location = new Point(1, 58);
            L_N1.Name = "L_N1";
            L_N1.Size = new Size(235, 24);
            L_N1.TabIndex = 9;
            L_N1.Text = "-1";
            L_N1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // L_0
            // 
            L_0.BorderStyle = BorderStyle.FixedSingle;
            L_0.Location = new Point(237, 58);
            L_0.Name = "L_0";
            L_0.Size = new Size(235, 24);
            L_0.TabIndex = 10;
            L_0.Text = "0";
            L_0.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // L_P1
            // 
            L_P1.BorderStyle = BorderStyle.FixedSingle;
            L_P1.Location = new Point(473, 58);
            L_P1.Name = "L_P1";
            L_P1.Size = new Size(235, 24);
            L_P1.TabIndex = 11;
            L_P1.Text = "+1";
            L_P1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // B_MinusOne
            // 
            B_MinusOne.Location = new Point(161, 32);
            B_MinusOne.Name = "B_MinusOne";
            B_MinusOne.Size = new Size(75, 23);
            B_MinusOne.TabIndex = 4;
            B_MinusOne.Text = "-1";
            B_MinusOne.UseVisualStyleBackColor = true;
            B_MinusOne.Click += B_MinusOne_Click;
            // 
            // B_AddOne
            // 
            B_AddOne.Location = new Point(473, 32);
            B_AddOne.Name = "B_AddOne";
            B_AddOne.Size = new Size(75, 23);
            B_AddOne.TabIndex = 5;
            B_AddOne.Text = "+1";
            B_AddOne.UseVisualStyleBackColor = true;
            B_AddOne.Click += B_AddOne_Click;
            // 
            // CB_Mode
            // 
            CB_Mode.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_Mode.FormattingEnabled = true;
            CB_Mode.Items.AddRange(new object[] { "Regular", "ItemBonus", "BallBonus" });
            CB_Mode.Location = new Point(335, 6);
            CB_Mode.Name = "CB_Mode";
            CB_Mode.Size = new Size(92, 23);
            CB_Mode.TabIndex = 1;
            // 
            // L_Mode
            // 
            L_Mode.Location = new Point(237, 4);
            L_Mode.Name = "L_Mode";
            L_Mode.Size = new Size(92, 24);
            L_Mode.TabIndex = 10;
            L_Mode.Text = "Mode:";
            L_Mode.TextAlign = ContentAlignment.MiddleRight;
            // 
            // L_Seed
            // 
            L_Seed.Location = new Point(237, 31);
            L_Seed.Name = "L_Seed";
            L_Seed.Size = new Size(92, 24);
            L_Seed.TabIndex = 11;
            L_Seed.Text = "Seed:";
            L_Seed.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MTB_Seed
            // 
            MTB_Seed.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MTB_Seed.Location = new Point(335, 33);
            MTB_Seed.Mask = "0000000000";
            MTB_Seed.Name = "MTB_Seed";
            MTB_Seed.Size = new Size(78, 21);
            MTB_Seed.TabIndex = 3;
            MTB_Seed.TextChanged += MTB_Seed_TextChanged;
            // 
            // CB_Count
            // 
            CB_Count.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_Count.FormattingEnabled = true;
            CB_Count.Items.AddRange(new object[] { "1", "5", "10" });
            CB_Count.Location = new Point(433, 6);
            CB_Count.Name = "CB_Count";
            CB_Count.Size = new Size(39, 23);
            CB_Count.TabIndex = 12;
            // 
            // AdjacentViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CB_Count);
            Controls.Add(MTB_Seed);
            Controls.Add(L_Seed);
            Controls.Add(L_Mode);
            Controls.Add(CB_Mode);
            Controls.Add(B_AddOne);
            Controls.Add(B_MinusOne);
            Controls.Add(L_P1);
            Controls.Add(L_0);
            Controls.Add(L_N1);
            Controls.Add(ADJ_P1);
            Controls.Add(ADJ_0);
            Controls.Add(ADJ_N1);
            Name = "AdjacentViewer";
            Size = new Size(709, 334);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ItemResultGridView ADJ_N1;
        private ItemResultGridView ADJ_0;
        private ItemResultGridView ADJ_P1;
        private Label L_N1;
        private Label L_0;
        private Label L_P1;
        private Button B_MinusOne;
        private Button B_AddOne;
        private ComboBox CB_Mode;
        private Label L_Mode;
        private Label L_Seed;
        private MaskedTextBox MTB_Seed;
        private ComboBox CB_Count;
    }
}
