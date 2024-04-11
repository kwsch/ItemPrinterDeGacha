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
        var lang = GameStrings.DefaultLanguage;
        if (Settings.Language.Length is 2 or 3)
            lang = Settings.Language;
        GameStrings.Initialize(lang);
        Localization.Initialize(lang);
    }

    private static string StartupPath => Path.GetDirectoryName(Environment.ProcessPath)!;
    private const string SettingsFile = "settings.json";
    private static string SettingsPath => Path.Combine(StartupPath, SettingsFile);
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    private static void DetectSettings()
    {
        var path = SettingsPath;
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
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
        var path = SettingsPath;
        var text = JsonSerializer.Serialize(Settings, Options);
        File.WriteAllText(path, text);
    }
}
