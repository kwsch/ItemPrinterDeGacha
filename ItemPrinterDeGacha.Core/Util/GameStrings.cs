namespace ItemPrinterDeGacha.Core;

/// <summary>
/// Utility logic to fetch item names from the embedded resources.
/// </summary>
public static class GameStrings
{
    private static string[] ItemNames = [];

    /// <summary>
    /// Initializes the item names from the embedded resources.
    /// </summary>
    public static void Initialize(string language = "en") => ItemNames = GetNames(language);

    /// <summary>
    /// Get the name of an item by its index.
    /// </summary>
    public static string GetItemName(int item)
    {
        if ((uint)item >= ItemNames.Length)
            return "???";
        return ItemNames[item];
    }

    private static string[] GetNames(string code)
    {
        if (Properties.Resources.ResourceManager.GetObject($"items_{code}") is not string resource)
            return [];
        var result = new string[2557]; // count of entries in text
        var span = resource.AsSpan();
        int ctr = 0;
        var enumerator = span.EnumerateLines();
        while (enumerator.MoveNext())
            result[ctr++] = enumerator.Current.ToString();
        return result;
    }
}
