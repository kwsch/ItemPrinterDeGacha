using ItemPrinterDeGacha.Core;
using System.ComponentModel.DataAnnotations;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class RegularSearch : UserControl
{
    private const PrintMode Mode = PrintMode.Regular;

    public RegularSearch()
    {
        InitializeComponent();
        CB_Seek.Items.AddRange(Program.Localization.LocalizeEnum<SearchModeRegular>());
        CB_Seek.SelectedIndex = 0;
        CB_Count.SelectedIndex = CB_Count.Items.Count - 1; // Default to 10

        var items = ComboItem.GetList(ItemPrinter.Items);
        CB_Item.InitializeBinding();
        CB_Item.DataSource = new BindingSource(items, null);
        CB_Item.SelectedValue = 53; // PP Max

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
        var search = (SearchModeRegular)CB_Seek.SelectedIndex;
        var count = (uint)NUD_Seconds.Value;

        var ticks = seed;
        TimeUtil.ClampStartLength(ref seed, ref count);

        int jobs = int.Parse(CB_Count.Text);
        Span<Item> tmp = stackalloc Item[jobs];

        if (min == 0 && max == 59)
        {
            if (search == SearchModeRegular.MaxSpecificItem)
            {
                (ulong t, int c) = SeedSearch.MaxResultsAny(ticks, ticks + count, tmp, Mode, item);
                Populate(t, tmp);
                // Append the total sum of the specific item found.
                RTB_Result.Text += Environment.NewLine + string.Format(Program.Localization.F1_Count, c);
            }
            return;
        }

        var currentSeconds = TimeUtil.GetDateTime(ticks).Second;
        ticks -= (ulong)currentSeconds;
        if (search == SearchModeRegular.MaxSpecificItem)
        {
            if (item == 0)
            {
                PopulateError(Program.Localization.ErrorNoItem);
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

                ticks += 60; // Next minute
            }
            while (ticks - seed < count);
            Populate(result, tmp.Length);
            // Append the total sum of the specific item found.
            RTB_Result.Text += Environment.NewLine + string.Format(Program.Localization.F1_Count, c);
            return;
        }
        PopulateError(Program.Localization.ErrorInvalidSearchCriteria);
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

    private void NUD_Seconds_ValueChanged(object sender, EventArgs e) => UpdateIncrement();
    private void UpdateIncrement() => tickToggle1.UpdateIncrement(NUD_Seconds.Value);
}
