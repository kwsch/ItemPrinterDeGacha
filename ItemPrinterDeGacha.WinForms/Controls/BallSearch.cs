using ItemPrinterDeGacha.Core;
using System.Text;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class BallSearch : UserControl
{
    private const PrintMode Mode = PrintMode.BallBonus;

    public BallSearch()
    {
        InitializeComponent();
        CB_Seek.SelectedIndex = 0;
        CB_Count.SelectedIndex = CB_Count.Items.Count - 1; // Default to 10

        var items = ComboItem.GetList(ItemPrinter.Balls);

        CB_Item.InitializeBinding();
        CB_Item.DataSource = new BindingSource(items, null);
        CB_Item.SelectedValue = 1; // Master Ball
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

        int jobs = int.Parse(CB_Count.Text);
        Span<Item> tmp = stackalloc Item[jobs];

        var count = (uint)NUD_Seconds.Value;

        if (min == 0 && max == 59)
        {
            if (search == SearchMode.MaxItem)
            {
                (ulong t, int c) = SeedSearch.MaxResultsAny(ticks, ticks + count, tmp, Mode, item);
                Populate(t, tmp);
                RTB_Result.Text += $"{Environment.NewLine}Count: {c}";
            }
            else
            {
                (ulong t, int c) = SeedSearch.MaxResultsAnyBall(ticks, ticks + count, tmp);
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
            do
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
            }
            while (ticks - seed < count);
            Populate(result, tmp.Length);
        }
        else
        {
            int c = -1;
            ulong result = 0;
            do
            {
                for (uint i = min; i <= max; i++)
                {
                    var check = ticks + i;
                    _ = ItemPrinter.Print(check, tmp, Mode);
                    int qty = 0;
                    foreach (var it in tmp)
                    {
                        if (it.Count == 1)
                            qty++;
                    }

                    if (qty <= c)
                        continue;
                    c = qty;
                    result = check;
                }

                ticks += 60;
            }
            while (ticks - seed < count);
            Populate(result, tmp.Length);
        }
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
        foreach (var item in items)
            lines.AppendLine($"x{item.Count} {GameStrings.GetItemName(item.ItemId)}");
        return lines.ToString();
    }

    private void Populate(string result) => RTB_Result.Text = result;

    private void CB_Seek_SelectedIndexChanged(object sender, EventArgs e)
    {
        L_Item.Visible = CB_Item.Visible = CB_Seek.SelectedIndex == 0;
    }

    public enum SearchMode
    {
        MaxItem = 0,
        MaxValue = 1,
    }
}
