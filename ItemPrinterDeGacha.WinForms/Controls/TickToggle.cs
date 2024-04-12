using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class TickToggle : UserControl
{
    public TickToggle()
    {
        InitializeComponent();
        NUD_Time.Minimum = TimeUtil.MinSeed;
        NUD_Time.Maximum = TimeUtil.MaxSeed;
    }

    private void ChangeTime(object sender, EventArgs e) => NUD_Time.Enabled = !RB_TimeCurrent.Checked;

    public ulong Seed
    {
        get
        {
            if (RB_TimeSpecific.Checked && ulong.TryParse(NUD_Time.Text, out var seed))
                return seed;

            var time = TimeUtil.GetTime(DateTime.Now.AddSeconds(20));
            NUD_Time.Text = time.ToString();
            return time;
        }
    }

    public bool TryGetSeed(out ulong ticks)
    {
        if (RB_TimeSpecific.Checked)
            return TimeUtil.TryGetValidSeed(NUD_Time.Text, out ticks);

        ticks = Seed;
        return true;
    }

    public void UpdateIncrement(decimal min) => NUD_Time.Increment = Math.Max(1, min);
}
