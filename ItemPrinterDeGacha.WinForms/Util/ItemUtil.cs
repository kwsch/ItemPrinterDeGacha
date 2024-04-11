using System.Text;
using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public static class ItemUtil
{
    public static string GetTextResult(ulong result, Span<Item> items)
    {
        var time = TimeUtil.GetDateTime(result);
        var timeText = time.ToString(Program.TimeFormat);
        return
            string.Format(Program.Localization.F1_Time, timeText) + Environment.NewLine +
            string.Format(Program.Localization.F1_Seed, result) + Environment.NewLine +
            GetResultString(items);
    }

    public static string GetResultString(Span<Item> items)
    {
        var lines = new StringBuilder(256);
        foreach (var item in items)
        {
            var itemName = GameStrings.GetItemName(item.ItemId);
            var text = string.Format(Program.Localization.F2_CountItem, item.Count, itemName);
            lines.AppendLine(text);
        }
        return lines.ToString();
    }
}
