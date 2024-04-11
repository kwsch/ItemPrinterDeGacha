namespace ItemPrinterDeGacha.WinForms;

public sealed partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        if (Program.Settings.DisableDpiScaling) // Set back to Font
            AutoScaleMode = AutoScaleMode.Font;
        this.TranslateInterface(Program.Settings.Language);
        Hide();
        BringToFront();
        System.Media.SystemSounds.Asterisk.Play();
        tabControl1.SelectedIndex = Program.Settings.CurrentTab;

#if DEBUG
        if (ModifierKeys == Keys.Shift)
            DevUtil.UpdateAll(); // Translations
#endif
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
