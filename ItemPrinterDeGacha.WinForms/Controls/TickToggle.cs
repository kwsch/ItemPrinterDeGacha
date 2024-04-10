using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class TickToggle : UserControl
{
    public TickToggle()
    {
        InitializeComponent();

        // Subscribe to the Resize event
        Resize += TickToggle_Resize;
    }

    private void TickToggle_Resize(object sender, EventArgs e)
    {
        AdjustFontSize();
    }

    private void AdjustFontSize()
    {
        // Calculate the font size based on the user control's size
        float fontSize = Math.Min(Width / 15f, Height / 5f);

        // Adjust the font size of the GroupBox, RadioButtons, and TextBox
        groupBox1.Font = new Font(groupBox1.Font.FontFamily, fontSize);
        RB_TimeCurrent.Font = new Font(RB_TimeCurrent.Font.FontFamily, fontSize);
        RB_TimeSpecific.Font = new Font(RB_TimeSpecific.Font.FontFamily, fontSize);
        TB_Time.Font = new Font(TB_Time.Font.FontFamily, fontSize);
    }
    private void ChangeTime(object sender, EventArgs e) => TB_Time.ReadOnly = RB_TimeCurrent.Checked;

    public ulong Seed
    {
        get
        {
            if (RB_TimeSpecific.Checked && ulong.TryParse(TB_Time.Text, out var seed))
                return seed;

            var time = TimeUtil.GetTime(DateTime.Now.AddSeconds(20));
            TB_Time.Text = time.ToString();
            return time;
        }
    }

    public bool TryGetSeed(out ulong ticks)
    {
        if (RB_TimeSpecific.Checked)
            return TimeUtil.TryGetValidSeed(TB_Time.Text, out ticks);

        ticks = Seed;
        return true;
    }
}
