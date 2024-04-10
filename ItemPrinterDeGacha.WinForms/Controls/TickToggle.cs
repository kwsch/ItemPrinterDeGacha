using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class TickToggle : UserControl
{
    public TickToggle() => InitializeComponent();
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
