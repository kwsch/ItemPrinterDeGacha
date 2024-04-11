using System.Reflection;

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

        var buildTime = Program.GetLinkerTime(Assembly.GetExecutingAssembly());
        if (buildTime != default)
            Text = $"{Text} - {buildTime:yyyy-MM-dd HH:mm:ss}";
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
