using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class ItemResultGridView : UserControl
{
    public ItemResultGridView() => InitializeComponent();

    public PrintMode Print(ulong ticks, PrintMode mode, int items)
    {
        if (items is (<1 ) or > 10)
            throw new ArgumentOutOfRangeException(nameof(items), "Items must be between 1 and 10.");

        Span<Item> itemSpan = stackalloc Item[items];
        var finalMode = ItemPrinter.Print(ticks, itemSpan, mode);

        Populate(itemSpan);
        return finalMode;
    }

    public void Populate(ReadOnlySpan<Item> itemSpan)
    {
        var rows = DGV_View.Rows;
        rows.Clear();
        foreach (var item in itemSpan)
        {
            var img = Properties.Resources.ResourceManager
                .GetObject($"aitem_{item.ItemId}");
            rows.Add(item.Count, img, GameStrings.GetItemName(item.ItemId));
        }
    }

    public void Clear() => DGV_View.Rows.Clear();
}
