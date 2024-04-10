using ItemPrinterDeGacha.Core;
using PKHeX.Core;
using System.Text;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class RegularSearch : UserControl
{
    private const PrintMode Mode = PrintMode.Regular;

    public RegularSearch()
    {
        InitializeComponent();
        CB_Seek.SelectedIndex = 0;

        var items = GameInfo.ItemDataSource
            .Where(z => ItemPrinter.IsInTable(z.Value, Mode))
            .ToArray();

        CB_Item.InitializeBinding();
        CB_Item.DataSource = new BindingSource(items, null);
        CB_Item.SelectedValue = 53; // PP Max
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
        var search = (SearchMode)CB_Seek.SelectedIndex;

        var ticks = seed;

        const int jobs = 10;
        Span<Item> tmp = stackalloc Item[jobs];

        var count = (uint)NUD_Seconds.Value;

        if (min == 0 && max == 59)
        {
            if (search == SearchMode.MaxItem)
            {
                (ulong t, int c) = ItemPrinter.MaxResultsAny(ticks, ticks + count, tmp, Mode, item);
                Populate(t, tmp);
                RTB_Result.Text += $"{Environment.NewLine}Count: {c}";
            }
            return;
        }

        var currentSeconds = TimeUtil.GetDateTime(ticks).Second;
        ticks -= (ulong)currentSeconds;
        if (search == SearchMode.MaxItem)
        {
            if (item == 0)
            {
                Populate("No item specified");
                return;
            }

            int c = -1;
            ulong result = 0;
            while (true)
            {
                for (uint i = min; i <= max; i++)
                {
                    var check = ticks + i;
                    _ = ItemPrinter.Print(check, tmp, Mode);
                    int qty = 0;
                    foreach (var it in tmp)
                    {
                        if (it.ItemId == item)
                            qty += it.Count;
                    }

                    if (qty <= c)
                        continue;
                    c = qty;
                    result = check;
                }

                ticks += 60;
                if (ticks - seed >= count)
                    break;
            }
            Populate(result, tmp.Length);
            return;
        }
        Populate("Invalid search.");
    }

    private void Populate(ulong result, int count)
    {
        Span<Item> items = stackalloc Item[count];
        ItemPrinter.Print(result, items, Mode);
        Populate(result, items);
    }

    private void Populate(ulong result, Span<Item> items)
    {
        var time = TimeUtil.GetDateTime(result);
        var text =
            $"Time: {time.ToString(Program.TimeFormat)}{Environment.NewLine}" +
            $"Seed: {result}{Environment.NewLine}" +
            GetResultString(items);
        Populate(text);
    }

    private static string GetResultString(Span<Item> items)
    {
        var lines = new StringBuilder(256);
        var names = GameInfo.Strings.Item;
        foreach (var item in items)
            lines.AppendLine($"x{item.Count} {names[item.ItemId]}");
        return lines.ToString();
    }

    private void Populate(string result) => RTB_Result.Text = result;

    public enum SearchMode
    {
        MaxItem = 0,
        MaxValue = 1,
    }
}
