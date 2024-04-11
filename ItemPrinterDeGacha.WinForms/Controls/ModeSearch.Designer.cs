namespace ItemPrinterDeGacha.WinForms.Controls
{
    partial class ModeSearch
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
            tickToggle1 = new TickToggle();
            L_Seek = new Label();
            CB_Mode = new ComboBox();
            L_Item = new Label();
            CB_Item = new ComboBox();
            B_Search = new Button();
            RTB_Result = new RichTextBox();
            NUD_Min = new NumericUpDown();
            NUD_Max = new NumericUpDown();
            L_Min = new Label();
            L_Max = new Label();
            CHK_PM2 = new CheckBox();
            CB_Count = new ComboBox();
            L_Jobs = new Label();
            DGV_View = new ItemResultGridView();
            ((System.ComponentModel.ISupportInitialize)NUD_Min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Max).BeginInit();
            SuspendLayout();
            // 
            // tickToggle1
            // 
            tickToggle1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tickToggle1.Location = new Point(4, 0);
            tickToggle1.Name = "tickToggle1";
            tickToggle1.Size = new Size(132, 82);
            tickToggle1.TabIndex = 6;
            // 
            // L_Seek
            // 
            L_Seek.Location = new Point(141, 4);
            L_Seek.Name = "L_Seek";
            L_Seek.Size = new Size(71, 24);
            L_Seek.TabIndex = 16;
            L_Seek.Text = "Seek:";
            L_Seek.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CB_Mode
            // 
            CB_Mode.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_Mode.DropDownWidth = 180;
            CB_Mode.FormattingEnabled = true;
            CB_Mode.Location = new Point(214, 6);
            CB_Mode.Name = "CB_Mode";
            CB_Mode.Size = new Size(130, 23);
            CB_Mode.TabIndex = 1;
            // 
            // L_Item
            // 
            L_Item.Location = new Point(141, 29);
            L_Item.Name = "L_Item";
            L_Item.Size = new Size(71, 24);
            L_Item.TabIndex = 22;
            L_Item.Text = "Item:";
            L_Item.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CB_Item
            // 
            CB_Item.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CB_Item.AutoCompleteSource = AutoCompleteSource.ListItems;
            CB_Item.DropDownWidth = 180;
            CB_Item.FormattingEnabled = true;
            CB_Item.Location = new Point(214, 31);
            CB_Item.Name = "CB_Item";
            CB_Item.Size = new Size(130, 23);
            CB_Item.TabIndex = 2;
            // 
            // B_Search
            // 
            B_Search.Location = new Point(540, 6);
            B_Search.Name = "B_Search";
            B_Search.Size = new Size(132, 48);
            B_Search.TabIndex = 0;
            B_Search.Text = "Search";
            B_Search.UseVisualStyleBackColor = false;
            B_Search.Click += B_Search_Click;
            // 
            // RTB_Result
            // 
            RTB_Result.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RTB_Result.Location = new Point(240, 86);
            RTB_Result.Name = "RTB_Result";
            RTB_Result.ReadOnly = true;
            RTB_Result.Size = new Size(440, 161);
            RTB_Result.TabIndex = 24;
            RTB_Result.Text = "";
            // 
            // NUD_Min
            // 
            NUD_Min.Location = new Point(470, 6);
            NUD_Min.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            NUD_Min.Name = "NUD_Min";
            NUD_Min.Size = new Size(44, 23);
            NUD_Min.TabIndex = 3;
            NUD_Min.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // NUD_Max
            // 
            NUD_Max.Location = new Point(470, 31);
            NUD_Max.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            NUD_Max.Name = "NUD_Max";
            NUD_Max.Size = new Size(44, 23);
            NUD_Max.TabIndex = 4;
            NUD_Max.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // L_Min
            // 
            L_Min.Location = new Point(348, 4);
            L_Min.Name = "L_Min";
            L_Min.Size = new Size(116, 24);
            L_Min.TabIndex = 27;
            L_Min.Text = "Min Seconds:";
            L_Min.TextAlign = ContentAlignment.MiddleRight;
            // 
            // L_Max
            // 
            L_Max.Location = new Point(348, 29);
            L_Max.Name = "L_Max";
            L_Max.Size = new Size(116, 24);
            L_Max.TabIndex = 28;
            L_Max.Text = "Max Seconds:";
            L_Max.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CHK_PM2
            // 
            CHK_PM2.CheckAlign = ContentAlignment.MiddleRight;
            CHK_PM2.Checked = true;
            CHK_PM2.CheckState = CheckState.Checked;
            CHK_PM2.Location = new Point(470, 59);
            CHK_PM2.Name = "CHK_PM2";
            CHK_PM2.Size = new Size(202, 19);
            CHK_PM2.TabIndex = 5;
            CHK_PM2.Text = "Successful +/- 2";
            CHK_PM2.TextAlign = ContentAlignment.MiddleRight;
            CHK_PM2.UseVisualStyleBackColor = true;
            // 
            // CB_Count
            // 
            CB_Count.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_Count.FormattingEnabled = true;
            CB_Count.Items.AddRange(new object[] { "1", "5", "10" });
            CB_Count.Location = new Point(214, 56);
            CB_Count.Name = "CB_Count";
            CB_Count.Size = new Size(39, 23);
            CB_Count.TabIndex = 39;
            // 
            // L_Jobs
            // 
            L_Jobs.Location = new Point(141, 55);
            L_Jobs.Name = "L_Jobs";
            L_Jobs.Size = new Size(71, 24);
            L_Jobs.TabIndex = 38;
            L_Jobs.Text = "Jobs:";
            L_Jobs.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DGV_View
            // 
            DGV_View.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            DGV_View.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DGV_View.Location = new Point(4, 86);
            DGV_View.Name = "DGV_View";
            DGV_View.Size = new Size(235, 160);
            DGV_View.TabIndex = 40;
            // 
            // ModeSearch
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(DGV_View);
            Controls.Add(CB_Count);
            Controls.Add(L_Jobs);
            Controls.Add(CHK_PM2);
            Controls.Add(L_Max);
            Controls.Add(L_Min);
            Controls.Add(NUD_Max);
            Controls.Add(NUD_Min);
            Controls.Add(RTB_Result);
            Controls.Add(B_Search);
            Controls.Add(L_Item);
            Controls.Add(CB_Item);
            Controls.Add(tickToggle1);
            Controls.Add(L_Seek);
            Controls.Add(CB_Mode);
            Margin = new Padding(0);
            Name = "ModeSearch";
            Size = new Size(680, 250);
            ((System.ComponentModel.ISupportInitialize)NUD_Min).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Max).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TickToggle tickToggle1;
        private Label L_Seek;
        private ComboBox CB_Mode;
        private Label L_Item;
        private ComboBox CB_Item;
        private Button B_Search;
        private RichTextBox RTB_Result;
        private NumericUpDown NUD_Min;
        private NumericUpDown NUD_Max;
        private Label L_Min;
        private Label L_Max;
        private CheckBox CHK_PM2;
        private ComboBox CB_Count;
        private Label L_Jobs;
        private ItemResultGridView DGV_View;
    }
}
