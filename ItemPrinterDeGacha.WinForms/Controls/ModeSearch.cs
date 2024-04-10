using ItemPrinterDeGacha.Core;
using PKHeX.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class ModeSearch : UserControl
{
    private const PrintMode Mode = PrintMode.Regular;

    public ModeSearch()
    {
        InitializeComponent();
        CB_Mode.SelectedIndex = 1; // Default to BallBonus

        var items = GameInfo.ItemDataSource
            .Where(z => ItemPrinter.IsInTable(z.Value, Mode) || z.Value == 0)
            .ToArray();

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
            RTB_Result.Text = "Min must be less than or equal to Max.";
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
        Span<Item> tmp = stackalloc Item[1];
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
                var result = $"{mode} @ {time} -- {check}";
                    result += $" -- with x{first.Count} {GameInfo.Strings.Item[first.ItemId]}!";
                RTB_Result.Text = result;
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
