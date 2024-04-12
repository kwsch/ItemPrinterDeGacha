using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls
{
    public partial class AdjacentViewer : UserControl
    {
        public AdjacentViewer()
        {
            InitializeComponent();
            NUD_Seed.Minimum = TimeUtil.MinSeed;
            NUD_Seed.Maximum = TimeUtil.MaxSeed;
            NUD_Seed.Text = ""; // Clear the entry so users can directly paste
            CB_Mode.Items.AddRange(Program.Localization.LocalizeEnum<PrintMode>());
            CB_Mode.SelectedIndex = 2; // Ball
            CB_Count.SelectedIndex = CB_Count.Items.Count - 1; // Default to 10
            NUD_Seed.ValueChanged += (_, _) => TryPrint();
            NUD_Seed.TextChanged += (_, _) => TryPrintText();
            CB_Mode.SelectedIndexChanged += (_, _) => TryPrint();
            CB_Count.SelectedIndexChanged += (_, _) => TryPrint();
        }

        private void B_MinusOne_Click(object sender, EventArgs e) => ChangeSeed(-1);
        private void B_AddOne_Click(object sender, EventArgs e) => ChangeSeed(+1);

        private void ChangeSeed(int bias)
        {
            ulong seed = (ulong)NUD_Seed.Value + (ulong)bias;
            if (!TimeUtil.IsValidSeed(seed))
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }
            NUD_Seed.Value = seed;
        }

        private void TryPrintText()
        {
            if (!TimeUtil.TryGetValidSeed(NUD_Seed.Text, out ulong seed))
            {
                if (NUD_Seed.Text.Length > 0)
                    System.Media.SystemSounds.Beep.Play();
                return;
            }
            Print(seed);
        }

        private void TryPrint()
        {
            ulong seed = (ulong)NUD_Seed.Value;
            if (!TimeUtil.IsValidSeed(seed))
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
