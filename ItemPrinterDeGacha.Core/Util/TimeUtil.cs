namespace ItemPrinterDeGacha.Core;

/// <summary>
/// Converts a seed to a DateTime and vice versa.
/// </summary>
public static class TimeUtil
{
    /// <summary>
    /// Minimum seed value the Nintendo Switch can generate with.
    /// </summary>
    /// <remarks>Can't really hit this instantly, need a few seconds delay.</remarks>
    public const uint MinSeed = 946684800; // 2000-01-01 00:00:00

    /// <summary>
    /// Maximum seed value the Nintendo Switch can generate with.
    /// </summary>
    /// <remarks>
    /// Nobody is really going to set their console to 2099 and let it overflow to wait, so this is essentially an arbitrary cap.
    /// </remarks>
    public const uint MaxSeed = 4102444800 - 1; // 2100-01-01 00:00:00

    /// <summary>
    /// A seed is the number of seconds elapsed since 00:00:00 on January 1, 1970, console's displayed time.
    /// </summary>
    /// <remarks>
    /// The Nintendo Switch uses the local time since 1970, not UTC.
    /// </remarks>
    private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Convert current time to time_t (seconds) format.
    /// </summary>
    public static ulong GetTime(DateTime now) => (ulong)(now - Epoch).TotalSeconds;

    /// <summary>
    /// Convert time_t (seconds) to DateTime format.
    /// </summary>
    public static DateTime GetDateTime(ulong time) => Epoch.AddSeconds(time);

    /// <summary>
    /// Converts a base10 ulong-string to a seed, and indicates if the seed is within the valid range.
    /// </summary>
    public static bool TryGetValidSeed(ReadOnlySpan<char> text, out ulong seed)
    {
        if (!ulong.TryParse(text, out seed))
            return false;

        return IsValidSeed(seed);
    }

    /// <summary>
    /// Checks if the seed is within the valid range.
    /// </summary>
    public static bool IsValidSeed(ulong seed) => seed is >= MinSeed and <= MaxSeed;
}
