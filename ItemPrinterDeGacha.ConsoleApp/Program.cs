using ItemPrinterDeGacha.Core;

int mode;
while (true)
{
    Console.WriteLine("Enter mode: 0 - Regular, 1 - ItemBonus, 2 - BallBonus");
    if (int.TryParse(Console.ReadLine(), out mode) && mode is >= 0 and <= 2)
        break;
    Console.WriteLine("Invalid mode, try again.");
}

if (mode == 0)
{
    Console.WriteLine("Enter desired mode: 1 - ItemBonus, 2 - BallBonus");
    if (int.TryParse(Console.ReadLine(), out int targetMode) && targetMode is >= 1 and <= 2)
    {
        ulong ticks = PromptTime();
        var result = ItemPrinter.FindNextBonusMode(ticks, (PrintMode)targetMode);
        Console.WriteLine($"Next {(PrintMode)targetMode} mode: {TimeUtil.GetDateTime(result)}");
    }
    return;
}

Console.WriteLine("Enter item ID:");
if (ushort.TryParse(Console.ReadLine(), out ushort itemId))
{
    ulong ticks = PromptTime();
    var result = mode == 1 ? ItemPrinter.FindNextItemBonus(ticks, itemId) : ItemPrinter.FindNextBall(ticks, itemId);
    Console.WriteLine($"Next {(PrintMode)mode} mode for item {itemId}: {TimeUtil.GetDateTime(result)}");
}

return;

static ulong PromptTime()
{
    const int bufferSeconds = 20; // give ample time for the user to react and press the button
    Console.WriteLine("Use current time? (Y/N)");
    if (string.Equals(Console.ReadLine(), "Y", StringComparison.OrdinalIgnoreCase))
        return TimeUtil.GetTime(DateTime.Now.AddSeconds(bufferSeconds)); // not UTC

    Console.WriteLine("Enter time (seconds since epoch):");
    if (ulong.TryParse(Console.ReadLine(), out ulong time))
        return time;

    throw new ArgumentException("Invalid time format");
}
