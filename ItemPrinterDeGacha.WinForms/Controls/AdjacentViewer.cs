using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls
{
    public partial class AdjacentViewer : UserControl
    {
        public AdjacentViewer()
        {
            InitializeComponent();
            CB_Mode.SelectedIndex = 2; // Ball
            CB_Count.SelectedIndex = CB_Count.Items.Count - 1;
            CB_Mode.SelectedIndexChanged += (_, _) => TryPrint();
            CB_Count.SelectedIndexChanged += (_, _) => TryPrint();
            Resize += AdjacentViewer_Resize;
        }

        private void AdjacentViewer_Resize(object sender, EventArgs e)
        {
            AdjustDataGridViewSizes();
        }

        private void AdjustDataGridViewSizes()
        {
            int padding = 10;
            int totalWidth = Width - (padding * 4);
            int columnWidth = totalWidth / 3;
            int height = Height - 120;

            ADJ_N1.Size = new Size(columnWidth, height);
            ADJ_0.Size = new Size(columnWidth, height);
            ADJ_P1.Size = new Size(columnWidth, height);

            ADJ_N1.Location = new Point(padding, ADJ_N1.Location.Y);
            ADJ_0.Location = new Point(padding * 2 + columnWidth, ADJ_0.Location.Y);
            ADJ_P1.Location = new Point(padding * 3 + columnWidth * 2, ADJ_P1.Location.Y);
        }

        private void MTB_Seed_TextChanged(object sender, EventArgs e) => TryPrint();
        private void B_MinusOne_Click(object sender, EventArgs e) => ChangeSeed(-1);
        private void B_AddOne_Click(object sender, EventArgs e) => ChangeSeed(+1);

        private void ChangeSeed(int bias)
        {
            if (!TimeUtil.TryGetValidSeed(MTB_Seed.Text, out ulong seed))
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }
            MTB_Seed.Text = (seed + (ulong)bias).ToString();
        }

        private void TryPrint()
        {
            if (!TimeUtil.TryGetValidSeed(MTB_Seed.Text, out ulong seed))
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }
            Print(seed);
        }

        private void Print(ulong seed)
        {
            var count = int.Parse(CB_Count.Text);
            var mode = (PrintMode)CB_Mode.SelectedIndex;
            Populate(ADJ_0, L_0, seed, count, mode);
            Populate(ADJ_N1, L_N1, seed - 1, count, mode);
            Populate(ADJ_P1, L_P1, seed + 1, count, mode);
        }

        private static void Populate(ItemResultGridView adj, Control lbl, ulong seed, int count, PrintMode mode)
        {
            var finalMode = adj.Print(seed, mode, count);
            var time = TimeUtil.GetDateTime(seed);
            lbl.Text = time.ToString(Program.TimeFormat);
            if (mode == PrintMode.Regular && finalMode != PrintMode.Regular)
                lbl.ForeColor = finalMode == PrintMode.BallBonus ? Color.Blue : Color.Red;
            else
                lbl.ResetForeColor();
        }
    }
}
