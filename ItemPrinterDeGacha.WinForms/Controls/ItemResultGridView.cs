using ItemPrinterDeGacha.Core;
using PKHeX.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class ItemResultGridView : UserControl
{
    public ItemResultGridView()
    {
        InitializeComponent();
        Resize += ItemResultGridView_Resize;
    }

    private void ItemResultGridView_Resize(object sender, EventArgs e)
    {
        AdjustColumnSizes();
        AdjustColumnHeaderFontSize();
    }

    private void AdjustColumnSizes()
    {
        int totalWidth = Width - SystemInformation.VerticalScrollBarWidth;
        int countColumnWidth = (int)(totalWidth * 0.35);
        int imgColumnWidth = (int)(totalWidth * 0.2);
        int nameColumnWidth = totalWidth - countColumnWidth - imgColumnWidth;

        DGV_View.Columns["Count"].Width = countColumnWidth;
        DGV_View.Columns["IMG"].Width = imgColumnWidth;
        DGV_View.Columns["ItemName"].Width = nameColumnWidth;
    }

    private void AdjustColumnHeaderFontSize()
    {
        float fontSize = Math.Min(Width / 30f, Height / 15f);
        foreach (DataGridViewColumn column in DGV_View.Columns)
        {
            column.HeaderCell.Style.Font = new Font(DGV_View.Font.FontFamily, fontSize);
        }
    }

    public PrintMode Print(ulong ticks, PrintMode mode, int items)
    {
        if (items is (<1 ) or > 10)
            throw new ArgumentOutOfRangeException(nameof(items), "Items must be between 1 and 10.");

        Span<Item> itemSpan = stackalloc Item[items];
        var finalMode = ItemPrinter.Print(ticks, itemSpan, mode);

        Populate(itemSpan);
        return finalMode;
    }

    private void Populate(ReadOnlySpan<Item> itemSpan)
    {
        var names = GameInfo.Strings.Item;
        var rows = DGV_View.Rows;
        rows.Clear();
        foreach (var item in itemSpan)
        {
            var img = PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager
                .GetObject($"aitem_{item.ItemId}");
            rows.Add(item.Count, img, names[item.ItemId]);
        }
    }
}
