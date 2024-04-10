namespace ItemPrinterDeGacha.Core;

public static class TimeUtil
{
    // number of seconds elapsed since 00:00:00 on January 1, 1970, Coordinated Universal Time.
    private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Convert current time to time_t (seconds) format.
    /// </summary>
    public static ulong GetTime(DateTime now) => (ulong)(now - Epoch).TotalSeconds;

    /// <summary>
    /// Convert time_t (seconds) to DateTime format.
    /// </summary>
    public static DateTime GetDateTime(ulong time) => Epoch.AddSeconds(time);

    public static bool TryGetValidSeed(ReadOnlySpan<char> text, out ulong seed)
    {
        if (!ulong.TryParse(text, out seed))
            return false;

        // Check that the datetime is valid
        var time = GetDateTime(seed);
        if (time.Year is < 2000 or > 2100)
            return false;

        return true;
    }
}
