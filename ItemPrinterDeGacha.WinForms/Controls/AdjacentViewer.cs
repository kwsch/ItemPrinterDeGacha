using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls
{
    public partial class AdjacentViewer : UserControl
    {
        public AdjacentViewer()
        {
            InitializeComponent();
            CB_Mode.SelectedIndex = 2; // Ball
            CB_Mode.SelectedIndexChanged += (_, _) => TryPrint();
        }

        private void NUD_ItemCount_ValueChanged(object sender, EventArgs e) => TryPrint();
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
            var count = (int)NUD_ItemCount.Value;
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

        private void NUD_ItemCount_MouseClick(object sender, MouseEventArgs e)
        {
            // If rmb, toggle between 1 and 10.
            if (e.Button != MouseButtons.Right)
                return;
            NUD_ItemCount.Value = NUD_ItemCount.Value == 1 ? 10 : 1;
        }
    }
}
