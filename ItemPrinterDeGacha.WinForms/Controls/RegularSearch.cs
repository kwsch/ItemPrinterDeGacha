using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class RegularSearch : UserControl
{
    private const PrintMode Mode = PrintMode.Regular;

    public RegularSearch()
    {
        InitializeComponent();
        CB_Seek.SelectedIndex = 0;
        CB_Count.SelectedIndex = CB_Count.Items.Count - 1; // Default to 10

        var items = ComboItem.GetList(ItemPrinter.Items);
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
            RTB_Result.Text = Program.Localization.ErrorMinMax;
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
                RTB_Result.Text += Environment.NewLine + string.Format(Program.Localization.F1_Count, c);
            }
            return;
        }

        var currentSeconds = TimeUtil.GetDateTime(ticks).Second;
        ticks -= (ulong)currentSeconds;
        if (search == SearchMode.MaxItem)
        {
            if (item == 0)
            {
                Populate(Program.Localization.ErrorNoItem);
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
            return;
        }
        Populate(Program.Localization.ErrorInvalidSearchCriteria);
    }

    private void Populate(ulong result, int count)
    {
        Span<Item> items = stackalloc Item[count];
        ItemPrinter.Print(result, items, Mode);
        Populate(result, items);
    }

    private void Populate(ulong result, Span<Item> items) => Populate(ItemUtil.GetTextResult(result, items));

    private void Populate(string result) => RTB_Result.Text = result;

    public enum SearchMode
    {
        MaxItem = 0,
        MaxValue = 1,
    }
}
