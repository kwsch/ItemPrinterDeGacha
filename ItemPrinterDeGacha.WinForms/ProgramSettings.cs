namespace ItemPrinterDeGacha.WinForms;

public sealed class ProgramSettings
{
    public string Language { get; init; } = "en";
    public string TimeFormat { get; init; } = "yyyy-MM-dd HH:mm:ss";
    public int CurrentTab { get; set; }
}
