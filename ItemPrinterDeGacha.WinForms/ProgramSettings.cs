namespace ItemPrinterDeGacha.WinForms;

public sealed class ProgramSettings
{
    public string Language { get; init; } = "en";
    public string TimeFormat { get; init; } = "yyyy-MM-dd HH:mm:ss";
    public bool DisableDpiScaling { get; init; } // Only set to true if you want to use Font scaling
    public int CurrentTab { get; set; }
}
