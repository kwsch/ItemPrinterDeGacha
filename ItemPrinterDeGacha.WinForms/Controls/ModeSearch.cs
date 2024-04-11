using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class ModeSearch : UserControl
{
    private const PrintMode Mode = PrintMode.Regular;

    public ModeSearch()
    {
        InitializeComponent();
        CB_Mode.SelectedIndex = 1; // Default to BallBonus
        CB_Count.SelectedIndex = 0; // Default to 1

        var items = ComboItem.GetList(ItemPrinter.Items);
        CB_Item.InitializeBinding();
        CB_Item.DataSource = new BindingSource(items, null);
        CB_Item.SelectedValue = 0;
    }

    private void B_Search_Click(object sender, EventArgs e)
    {
        var min = (uint)NUD_Min.Value;
        var max = (uint)NUD_Max.Value;
        if (min > max)
        {
            RTB_Result.Text = Program.Localization.ErrorMinMax;
            return;
        }

        if (!tickToggle1.TryGetSeed(out var seed))
        {
            System.Media.SystemSounds.Beep.Play();
            return;
        }

        var item = WinFormsUtil.GetIndex(CB_Item);
        var mode = (PrintMode)(CB_Mode.SelectedIndex + 1);

        var ticks = seed;
        var currentSeconds = TimeUtil.GetDateTime(ticks).Second;
        ticks -= (ulong)currentSeconds;
        int jobs = int.Parse(CB_Count.Text);
        Span<Item> tmp = stackalloc Item[jobs];
        while (true)
        {
            for (uint i = min; i <= max; i++)
            {
                var check = ticks + i;
                var newMode = ItemPrinter.Print(check, tmp, Mode);
                if (newMode != mode)
                    continue;

                var first = tmp[0];
                if (item != 0 && first.ItemId != item)
                    continue;

                if (CHK_PM2.Checked && !IsPassAdjacent(check, tmp, mode))
                    continue;

                var dateTime = TimeUtil.GetDateTime(check);
                var time = dateTime.ToString(Program.TimeFormat);
                RTB_Result.Text =
                    string.Format(Program.Localization.F3_ModeAtTimeSeed, mode, time, check) + Environment.NewLine +
                    ItemUtil.GetResultString(tmp);
                return;
            }
            ticks += 60;
        }
    }

    private static bool IsPassAdjacent(ulong check, Span<Item> tmp, PrintMode mode)
    {
        for (int j = -2; j <= +2; j++)
        {
            var seed = check + unchecked((ulong)j);
            var adj = ItemPrinter.Print(seed, tmp, Mode);
            if (adj != mode)
                return false;
        }
        return true;
    }
}
