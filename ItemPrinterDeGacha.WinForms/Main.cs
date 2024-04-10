using System.Text.Json;

namespace ItemPrinterDeGacha.WinForms;

public sealed partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        Hide();
        BringToFront();
        System.Media.SystemSounds.Asterisk.Play();
        tabControl1.SelectedIndex = Program.Settings.CurrentTab;
    }

    private void ChangeSelectedTab(object sender, EventArgs e)
    {
        Program.Settings.CurrentTab = tabControl1.SelectedIndex;
    }

    private void Main_FormClosed(object sender, FormClosedEventArgs e)
    {
        // Save settings
        Program.SaveSettings();
    }
}
