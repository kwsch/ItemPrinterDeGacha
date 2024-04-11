namespace ItemPrinterDeGacha.WinForms;

internal static class ResourceUtil
{
    public static ReadOnlySpan<string> GetStringList(string s)
    {
        if (Properties.Resources.ResourceManager.GetObject(s) is not string resource)
            return [];

        var list = new List<string>();
        var span = resource.AsSpan();
        var enumerator = span.EnumerateLines();
        while (enumerator.MoveNext())
            list.Add(enumerator.Current.ToString());
        return list.ToArray();
    }

    public static string GetString(string name)
    {
        return Properties.Resources.ResourceManager.GetObject(name) as string ?? "";
    }
}
