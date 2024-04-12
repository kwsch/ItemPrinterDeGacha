using System.ComponentModel.DataAnnotations;
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

        UpdateIncrement();
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
        var count = (uint)NUD_Seconds.Value;

        var ticks = seed;
        TimeUtil.ClampStartLength(ref seed, ref count);

        int jobs = int.Parse(CB_Count.Text);
        Span<Item> tmp = stackalloc Item[jobs];

        if (min == 0 && max == 59)
        {
            if (search == MaxSpecificItem)
                SearchAnyTimeSpecificItem(ticks, count, tmp, item);
            else if (search == MaxValuables)
                SearchAnyTimeValuables(ticks, count, tmp);
            else
                PopulateError(Program.Localization.ErrorInvalidSearchCriteria);
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
                PopulateError(Program.Localization.ErrorInvalidSearchCriteria);
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
            PopulateError(Program.Localization.ErrorNoItem);
            return;
        }

        // Print the items for each second in the range, and count the number of the specific item.
        // If the count is higher than the previous highest count, update the result.
        // If the count is the same, keep the previous result.
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

            ticks += 60; // Next minute
        }
        while (ticks - seed < count);
        Populate(result, tmp.Length);
        // Append the total sum of the specific item found.
        RTB_Result.Text += Environment.NewLine + string.Format(Program.Localization.F1_Count, c);
    }

    private void SearchValuables(uint min, uint max, ulong ticks, Span<Item> tmp, ulong seed, uint count)
    {
        // Print the items for each second in the range, and count the number of Special Balls.
        // If the count is higher than the previous highest count, update the result.
        // If the count is the same, keep the previous result.
        // Special Balls will always have a count of 1, so just use that as our "is valuable" check.
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

            ticks += 60; // Next minute
        }
        while (ticks - seed < count);
        Populate(result, tmp.Length);
        // Append the total sum of the valuable items found.
        RTB_Result.Text += Environment.NewLine + string.Format(Program.Localization.F1_Count, c);
    }

    /// <summary>
    /// Displays the result of the search in the user interface.
    /// </summary>
    /// <param name="result">Seed found.</param>
    /// <param name="count">Items printed with that seed.</param>
    private void Populate(ulong result, [Range(1, 10)] int count)
    {
        Span<Item> items = stackalloc Item[count];
        ItemPrinter.Print(result, items, Mode);
        Populate(result, items);
    }

    /// <summary>
    /// Displays the result of the search in the user interface.
    /// </summary>
    private void Populate(ulong result, [Length(1, 10)] Span<Item> items)
    {
        DGV_View.Populate(items);
        RTB_Result.Text = ItemUtil.GetTextResult(result, items);
        System.Media.SystemSounds.Beep.Play();
    }

    private void PopulateError(string error)
    {
        RTB_Result.Text = error;
        System.Media.SystemSounds.Beep.Play();
        DGV_View.Clear();
    }

    private void CB_Seek_SelectedIndexChanged(object sender, EventArgs e)
    {
        L_Item.Visible = CB_Item.Visible = CB_Seek.SelectedIndex == 0;
    }

    private void NUD_Seconds_ValueChanged(object sender, EventArgs e) => UpdateIncrement();
    private void UpdateIncrement() => tickToggle1.UpdateIncrement(NUD_Seconds.Value);
}
