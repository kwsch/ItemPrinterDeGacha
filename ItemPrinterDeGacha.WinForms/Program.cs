using System.Text.Json;
using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms;

internal static class Program
{
    public static ProgramSettings Settings { get; private set; } = new();
    public static string TimeFormat => Settings.TimeFormat;
    public static Localization Localization => Localization.Instance;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        DetectSettings();
        Initialize();

        ApplicationConfiguration.Initialize();
        Application.Run(new Main());
    }

    private static void Initialize()
    {
        var lang = "en";
        if (Settings.Language.Length is 2 or 3)
            lang = Settings.Language;
        GameStrings.Initialize(lang);
        Localization.Initialize(lang);
    }

    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    private static void DetectSettings()
    {
        if (File.Exists("settings.json"))
        {
            var json = File.ReadAllText("settings.json");
            Settings = JsonSerializer.Deserialize<ProgramSettings>(json)!;
        }
        else
        {
            // Save the default settings
            SaveSettings();
        }
    }

    public static void SaveSettings()
    {
        var text = JsonSerializer.Serialize(Settings, Options);
        File.WriteAllText("settings.json", text);
    }
}
