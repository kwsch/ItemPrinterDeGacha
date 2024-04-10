namespace ItemPrinterDeGacha.WinForms;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        Hide();
        BringToFront();
        System.Media.SystemSounds.Asterisk.Play();
    }
}
