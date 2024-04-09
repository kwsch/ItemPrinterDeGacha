using System.Text;
using ItemPrinterDeGacha.Core;

namespace ItemPrinterDeGacha;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        CB_TargetMode.SelectedIndex = 1; // Ball
        CB_Mode.SelectedIndex = 2; // Ball
        CB_Seek.SelectedIndex = 0; // Seek Single Item
    }

    private void ChangeTimeMode(object? sender, EventArgs e)
    {
        bool current = RB_TimeCurrent.Checked;
        TB_Time.ReadOnly = current;
    }

    private void CB_Mode_SelectedIndexChanged(object? sender, EventArgs e)
    {
        bool regular = CB_Mode.SelectedIndex == 0;
        NUD_ItemID.Enabled = CB_Seek.SelectedIndex < 2;
        CB_Seek.Enabled = !regular;

        CB_TargetMode.Enabled = regular;
    }

    private uint SecondsToSearch => (uint)NUD_Seconds.Value;

    public static bool IsGoodSeconds(int seconds) => seconds is >= 8 and <= 13;

    private void B_Generate_Click(object? sender, EventArgs e)
    {
        try
        {
            RTB_Result.Text = Generate();
        }
        catch (Exception ex)
        {
            RTB_Result.Text = ex.Message;
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string Generate()
    {
        ulong ticks;
        if (RB_TimeCurrent.Checked)
        {
            const int bias = 20; // give ample time for the user to react and press the button
            var date = DateTime.Now.AddSeconds(bias);
            ticks = TimeUtil.GetTime(date);
            TB_Time.Text = ticks.ToString();
        }
        else
        {
            if (!ulong.TryParse(TB_Time.Text, out ticks))
                return "Invalid time format";
        }

        if (!ushort.TryParse(NUD_ItemID.Text, out ushort itemId))
            throw new Exception("Invalid item ID");

        var mode = (PrintMode)CB_Mode.SelectedIndex;
        if (mode == PrintMode.Regular)
        {
            if (CB_Seek.SelectedIndex == 1) // Maximize specific item
            {
                var (result, count) = Generator.MaxResults(itemId, ticks, ticks + SecondsToSearch, mode);
                return
                    $"Next target for {ItemNames.Names[itemId]} ({itemId}): {TimeUtil.GetDateTime(result)}{Environment.NewLine}" +
                    $"Delay: {result - ticks} seconds ({(float)(result - ticks) / 60:F2}){Environment.NewLine}" +
                    $"Time: {result}{Environment.NewLine}" +
                    $"Count: {count}";
            }
            else
            {
                var targetMode = (PrintMode)(CB_TargetMode.SelectedIndex + 1);
                ulong result;
                DateTime time;
                {
                    var start = ticks;
                    while (true)
                    {
                        result = Generator.FindNextBonusMode(start, targetMode, itemId);
                        time = TimeUtil.GetDateTime(result);
                        if (IsGoodSeconds(time.Second)) // quick enough after a time change but not too quick.
                            break;
                        start = result + 1;
                    }
                }
                return
                    $"Next {targetMode} mode: {time}{(itemId != 0 ? $" w/ {ItemNames.Names[itemId]}" : "")}{Environment.NewLine}" +
                    $"Delay: {result - ticks} seconds ({(float)(result - ticks) / 60:F2}){Environment.NewLine}" +
                    $"Time: {result}";
            }
        }

        if (CB_Seek.SelectedIndex == 0) // Specific item (use 1)
        {
            var result = Generator.FindNextItem(ticks, itemId, mode);
            return
                $"Next target for {ItemNames.Names[itemId]} ({itemId}): {TimeUtil.GetDateTime(result)}{Environment.NewLine}" +
                $"Delay: {result - ticks} seconds ({(float)(result - ticks) / 60:F2}){Environment.NewLine}" +
                $"Time: {result}";
        }

        if (CB_Seek.SelectedIndex == 1) // Maximize specific item (use 10)
        {
            var (result, count) = Generator.MaxResults(itemId, ticks, ticks + SecondsToSearch, mode);
            return
                $"Next target for {ItemNames.Names[itemId]} ({itemId}): {TimeUtil.GetDateTime(result)}{Environment.NewLine}" +
                $"Delay: {result - ticks} seconds ({(float)(result - ticks) / 60:F2}){Environment.NewLine}" +
                $"Time: {result}{Environment.NewLine}" +
                $"Count: {count}";
        }

        if (CB_Seek.SelectedIndex == 2)
        {
            if (mode == PrintMode.BallBonus) // Maximize all valuables
            {
                Span<Item> items = stackalloc Item[10];
                var (result, count) = Generator.MaxResultsAnyBall(ticks, ticks + SecondsToSearch, items);
                return GetSetList(items, result, ticks, count, ItemNames.Names);
            }
            else
            {
                // TODO: MANY ITEMS?
                Span<Item> items = stackalloc Item[10];
                var (result, count) = Generator.MaxResultsAny(ticks, ticks + SecondsToSearch, items, itemId);
                return GetSetList(items, result, ticks, count, ItemNames.Names);
            }
        }

        throw new Exception("Invalid mode");
    }

    private static string GetSetList(Span<Item> items, ulong result, ulong ticks, int count, ReadOnlySpan<string> itemNames)
    {
        var lines = new StringBuilder(256);
        foreach (var item in items)
            lines.AppendLine($"x{item.Count} {itemNames[item.ItemId]} ({item.ItemId:0000})");
        return
            $"Best result: {TimeUtil.GetDateTime(result)} (0x{result:x8}){Environment.NewLine}" +
            $"Time: {result}{Environment.NewLine}" +
            $"Delay: {result - ticks} seconds ({(float)(result - ticks) / 60:F2}){Environment.NewLine}" +
            $"Count: {count}{Environment.NewLine}" +
            lines;
    }
}
