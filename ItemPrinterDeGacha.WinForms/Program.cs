using System.Text.Json;
using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms;

public sealed class ProgramSettings
{
    public string Language { get; init; } = "en";
    public string TimeFormat { get; init; } = "yyyy-MM-dd HH:mm:ss";
    public int CurrentTab { get; set; }
}

internal static class Program
{
    public static ProgramSettings Settings = new();
    public static string TimeFormat => Settings.TimeFormat;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        DetectSettings();
        InitializePKHeX();

        ApplicationConfiguration.Initialize();
        Application.Run(new Main());
    }

    private static void InitializePKHeX()
    {
        var lang = "en";
        if (Settings.Language.Length is 2 or 3)
            lang = Settings.Language;
        GameStrings.Initialize(lang);
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
