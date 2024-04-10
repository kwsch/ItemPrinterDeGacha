namespace ItemPrinterDeGacha.WinForms
{
    sealed partial class Main
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
            tabControl1 = new TabControl();
            Tab_Bonus = new TabPage();
            modeSearch1 = new Controls.ModeSearch();
            Tab_Regular = new TabPage();
            regularSearch1 = new Controls.RegularSearch();
            Tab_Ball = new TabPage();
            ballSearch1 = new Controls.BallSearch();
            Tab_Adjacent = new TabPage();
            adjacentViewer1 = new Controls.AdjacentViewer();
            tabControl1.SuspendLayout();
            Tab_Bonus.SuspendLayout();
            Tab_Regular.SuspendLayout();
            Tab_Ball.SuspendLayout();
            Tab_Adjacent.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Tab_Bonus);
            tabControl1.Controls.Add(Tab_Regular);
            tabControl1.Controls.Add(Tab_Ball);
            tabControl1.Controls.Add(Tab_Adjacent);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.ItemSize = new Size(128, 32);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(723, 427);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += ChangeSelectedTab;
            // 
            // Tab_Bonus
            // 
            Tab_Bonus.Controls.Add(modeSearch1);
            Tab_Bonus.Location = new Point(4, 36);
            Tab_Bonus.Name = "Tab_Bonus";
            Tab_Bonus.Size = new Size(715, 387);
            Tab_Bonus.TabIndex = 0;
            Tab_Bonus.Text = "Bonus";
            Tab_Bonus.UseVisualStyleBackColor = true;
            // 
            // modeSearch1
            // 
            modeSearch1.AutoScaleMode = AutoScaleMode.Inherit;
            modeSearch1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            modeSearch1.Dock = DockStyle.Fill;
            modeSearch1.Location = new Point(0, 0);
            modeSearch1.Name = "modeSearch1";
            modeSearch1.Size = new Size(715, 387);
            modeSearch1.TabIndex = 0;
            // 
            // Tab_Regular
            // 
            Tab_Regular.Controls.Add(regularSearch1);
            Tab_Regular.Location = new Point(4, 36);
            Tab_Regular.Name = "Tab_Regular";
            Tab_Regular.Size = new Size(715, 387);
            Tab_Regular.TabIndex = 2;
            Tab_Regular.Text = "Regular";
            Tab_Regular.UseVisualStyleBackColor = true;
            // 
            // regularSearch1
            // 
            regularSearch1.Dock = DockStyle.Fill;
            regularSearch1.Location = new Point(0, 0);
            regularSearch1.Name = "regularSearch1";
            regularSearch1.Size = new Size(715, 387);
            regularSearch1.TabIndex = 0;
            // 
            // Tab_Ball
            // 
            Tab_Ball.Controls.Add(ballSearch1);
            Tab_Ball.Location = new Point(4, 36);
            Tab_Ball.Name = "Tab_Ball";
            Tab_Ball.Size = new Size(715, 387);
            Tab_Ball.TabIndex = 3;
            Tab_Ball.Text = "Ball";
            Tab_Ball.UseVisualStyleBackColor = true;
            // 
            // ballSearch1
            // 
            ballSearch1.Dock = DockStyle.Fill;
            ballSearch1.Location = new Point(0, 0);
            ballSearch1.Name = "ballSearch1";
            ballSearch1.Size = new Size(715, 387);
            ballSearch1.TabIndex = 0;
            // 
            // Tab_Adjacent
            // 
            Tab_Adjacent.Controls.Add(adjacentViewer1);
            Tab_Adjacent.Location = new Point(4, 36);
            Tab_Adjacent.Name = "Tab_Adjacent";
            Tab_Adjacent.Padding = new Padding(3);
            Tab_Adjacent.Size = new Size(715, 387);
            Tab_Adjacent.TabIndex = 1;
            Tab_Adjacent.Text = "Adjacent";
            Tab_Adjacent.UseVisualStyleBackColor = true;
            // 
            // adjacentViewer1
            // 
            adjacentViewer1.Dock = DockStyle.Fill;
            adjacentViewer1.Location = new Point(3, 3);
            adjacentViewer1.Name = "adjacentViewer1";
            adjacentViewer1.Size = new Size(709, 381);
            adjacentViewer1.TabIndex = 0;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            ClientSize = new Size(723, 427);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ItemPrinterDeGacha";
            FormClosed += Main_FormClosed;
            tabControl1.ResumeLayout(false);
            Tab_Bonus.ResumeLayout(false);
            Tab_Regular.ResumeLayout(false);
            Tab_Ball.ResumeLayout(false);
            Tab_Adjacent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage Tab_Bonus;
        private TabPage Tab_Adjacent;
        private TabPage Tab_Regular;
        private TabPage Tab_Ball;
        private WinForms.Controls.AdjacentViewer adjacentViewer1;
        private WinForms.Controls.RegularSearch regularSearch1;
        private WinForms.Controls.BallSearch ballSearch1;
        private WinForms.Controls.ModeSearch modeSearch1;
    }
}
