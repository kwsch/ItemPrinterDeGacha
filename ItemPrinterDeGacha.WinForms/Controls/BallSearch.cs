using ItemPrinterDeGacha.Core;
using static ItemPrinterDeGacha.WinForms.SearchModeBall;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class BallSearch : UserControl
{
    private const PrintMode Mode = PrintMode.BallBonus;

    public BallSearch()
    {
        InitializeComponent();
        CB_Seek.Items.AddRange(Program.Localization.LocalizeEnum<SearchModeBall>());
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
            RTB_Result.Text = Program.Localization.ErrorMinMax;
            return;
        }

        if (!tickToggle1.TryGetSeed(out var seed))
        {
            System.Media.SystemSounds.Beep.Play();
            return;
        }

        var item = WinFormsUtil.GetIndex(CB_Item);
        var search = (SearchModeBall)CB_Seek.SelectedIndex;

        var ticks = seed;

        int jobs = int.Parse(CB_Count.Text);
        Span<Item> tmp = stackalloc Item[jobs];

        var count = (uint)NUD_Seconds.Value;

        if (min == 0 && max == 59)
        {
            if (search == MaxSpecificItem)
                SearchAnyTimeSpecificItem(ticks, count, tmp, item);
            else if (search == MaxValuables)
                SearchAnyTimeValuables(ticks, count, tmp);
            else
                Populate(Program.Localization.ErrorInvalidSearchCriteria);
        }
        else
        {
            var currentSeconds = TimeUtil.GetDateTime(ticks).Second;
            ticks -= (ulong)currentSeconds;
            if (search == MaxSpecificItem)
                SearchSpecificItem(item, min, max, ticks, tmp, seed, count);
            else if (search == MaxValuables)
                SearchValuables(min, max, ticks, tmp, seed, count);
            else
                Populate(Program.Localization.ErrorInvalidSearchCriteria);
        }
    }

    private void SearchAnyTimeSpecificItem(ulong ticks, uint count, Span<Item> tmp, int item)
    {
        (ulong t, int c) = SeedSearch.MaxResultsAny(ticks, ticks + count, tmp, Mode, item);
        Populate(t, tmp);
        RTB_Result.Text += Environment.NewLine +
                           string.Format(Program.Localization.F1_Count, c);
    }

    private void SearchAnyTimeValuables(ulong ticks, uint count, Span<Item> tmp)
    {
        (ulong t, int c) = SeedSearch.MaxResultsAnyBall(ticks, ticks + count, tmp);
        Populate(t, tmp);
        RTB_Result.Text += Environment.NewLine +
                           string.Format(Program.Localization.F1_Count, c);
    }

    private void SearchSpecificItem(int item, uint min, uint max, ulong ticks, Span<Item> tmp, ulong seed, uint count)
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
    }

    private void SearchValuables(uint min, uint max, ulong ticks, Span<Item> tmp, ulong seed, uint count)
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

    private void Populate(ulong result, int count)
    {
        Span<Item> items = stackalloc Item[count];
        ItemPrinter.Print(result, items, Mode);
        Populate(result, items);
    }

    private void Populate(ulong result, Span<Item> items)
    {
        DGV_View.Populate(items);
        Populate(ItemUtil.GetTextResult(result, items));
    }

    private void Populate(string result)
    {
        RTB_Result.Text = result;
        System.Media.SystemSounds.Beep.Play();
    }

    private void CB_Seek_SelectedIndexChanged(object sender, EventArgs e)
    {
        L_Item.Visible = CB_Item.Visible = CB_Seek.SelectedIndex == 0;
    }
}
