using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace ItemPrinterDeGacha.WinForms;

public sealed class Localization
{
    public static Localization Instance { get; private set; } = new();

    public string ErrorMinMax { get; init; } = "Min must be less than or equal to Max.";
    public string ErrorNoItem { get; init; } = "No item specified";
    public string ErrorInvalidSearchCriteria { get; init; } = "Invalid search criteria.";
    public string F1_Count { get; init; } = "Count: {0}";
    public string F1_Time { get; init; } = "Time: {0}";
    public string F1_Seed { get; init; } = "Seed: {0}";
    public string F2_CountItem { get; init; } = "x{0} {1}"; // x4 Master Ball
    public string F3_ModeAtTimeSeed { get; init; } = "{0} @ {1} -- {2}";

    public Dictionary<string, string> Others { get; init; } = [];

    public static void Initialize(string lang)
    {
        try { Instance = Load(lang); }
        catch { }
    }

    private static string GetResourceName(ReadOnlySpan<char> language) => $"message_{language}";

    public static Localization Load(ReadOnlySpan<char> language)
    {
        var name = GetResourceName(language);
        var text = ResourceUtil.GetString(name);
        if (text is not { Length: > 0 })
            return new Localization();
        return JsonSerializer.Deserialize<Localization>(text, Options)!;
    }

    private static readonly JsonSerializerOptions Options = new() {
        WriteIndented = true,
        AllowTrailingCommas = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    };

#if DEBUG

    public void Save(string path, ReadOnlySpan<char> language)
    {
        var fileName = GetResourceName(language) + ".txt";
        var text = JsonSerializer.Serialize(this, Options);
        File.WriteAllText(Path.Combine(path, fileName), text, Encoding.UTF8);
    }

#endif
    public string[] LocalizeEnum<T>() where T : struct, Enum
    {
        var type = typeof(T);
        var names = Enum.GetNames<T>();
        var result = new string[names.Length];
        for (int i = 0; i < result.Length; i++)
            result[i] = Localize($"{type.Name}.{names[i]}", names[i]);
        return result;
    }

    public string LocalizeEnum<T>(T value) where T : struct, Enum
    {
        var type = typeof(T);
        var name = Enum.GetName(value) ?? $"{value}";
        return Localize($"{type.Name}.{name}", name);
    }

    public void CacheEnum<T>() where T : struct, Enum
    {
        var type = typeof(T);
        foreach (var name in Enum.GetNames<T>())
            Localize($"{type.Name}.{name}", name);
    }

    private string Localize(string key, string fallback)
    {
        if (Others.TryGetValue(key, out var value))
            return value;
        Others.Add(key, fallback);
        return fallback;
    }
}
