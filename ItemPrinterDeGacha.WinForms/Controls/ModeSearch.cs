using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class ModeSearch : UserControl
{
    private const PrintMode Mode = PrintMode.Regular;

    public ModeSearch()
    {
        InitializeComponent();
        CB_Mode.Items.AddRange(Program.Localization.LocalizeEnum<PrintMode>()[1..]); // Skip Regular
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
        bool checkAdjacentSeeds = CHK_PM2.Checked; // +/- 2 seconds [-2, +2] need to have same mode
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

                if (checkAdjacentSeeds && !IsPassAdjacent(check, tmp, mode))
                    continue;

                SetResult(check, mode, tmp);
                return;
            }
            ticks += 60; // Next minute
        }
    }

    /// <summary>
    /// Displays the result of the search in the user interface.
    /// </summary>
    private void SetResult(ulong seed, PrintMode mode, Span<Item> items)
    {
        var dateTime = TimeUtil.GetDateTime(seed);
        var time = dateTime.ToString(Program.TimeFormat);
        var modeL10n = Program.Localization.LocalizeEnum(mode);
        RTB_Result.Text =
            string.Format(Program.Localization.F3_ModeAtTimeSeed, modeL10n, time, seed) + Environment.NewLine +
            ItemUtil.GetResultString(items);
        DGV_View.Populate(items);
        System.Media.SystemSounds.Beep.Play();
    }

    private static bool IsPassAdjacent(ulong check, Span<Item> tmp, PrintMode mode)
    {
        // Check for adjacent results to ensure the bonus mode is the same.
        // +/- 2 seconds should be enough to cover even the most inconsistent user.
        // They can then use the adjacent results to see how far off they were while still not wasting resources.
        for (int j = -2; j <= +2; j++)
        {
            var seed = check + unchecked((ulong)j);
            var adj = ItemPrinter.Print(seed, tmp, Mode);
            if (adj != mode)
                return false;
        }
        // Since `tmp` contains the result at +2sec, recalculate the result for +/- 0.
        ItemPrinter.Print(check, tmp, Mode);
        return true;
    }
}
