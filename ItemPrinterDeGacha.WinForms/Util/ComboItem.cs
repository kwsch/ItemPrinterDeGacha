using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms;

public sealed record ComboItem(string Text, int Value) : IComparable<ComboItem>
{
    public static ComboItem[] GetList(ReadOnlySpan<ushort> items)
    {
        var list = new ComboItem[items.Length + 1];
        for (int i = 0; i < items.Length; i++)
            list[i] = new ComboItem(GameStrings.GetItemName(items[i]), items[i]);
        list[^1] = new ComboItem(GameStrings.GetItemName(0), 0);
        Array.Sort(list);
        return list;
    }

    public int CompareTo(ComboItem? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        int textComparison = string.CompareOrdinal(Text, other.Text);
        if (textComparison != 0) return textComparison;
        return Value.CompareTo(other.Value);
    }
}
