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
    /// Nobody is really going to set their console to 2060 and let it overflow to wait, so this is essentially an arbitrary cap.
    /// </remarks>
    public const uint MaxSeed = 2871763200 - 1; // 2061-01-01 00:00:00

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

    /// <summary>
    /// Clamps the start and end seed values to the valid range.
    /// </summary>
    public static void ClampStartEnd(ref ulong start, ref ulong end)
    {
        if (start > end) // Be nice and swap if max<min
            (start, end) = (end, start);
        if (start < MinSeed)
            start = MinSeed;
        if (end > MaxSeed)
            end = MaxSeed;
    }

    /// <summary>
    /// Clamps the start and additional-seconds to the valid range.
    /// </summary>
    /// <param name="seed">Seed to start from.</param>
    /// <param name="count">Additional seconds to add to the seed.</param>
    public static void ClampStartLength(ref ulong seed, ref uint count)
    {
        if (seed < MinSeed)
            seed = MinSeed;
        if (count == 0)
            return; // 0 is a valid count; no further changes needed.

        var max = seed + count;
        if (max > MaxSeed || max < seed) // shouldn't overflow but check anyway
            count = (uint)(MaxSeed - seed);
    }
}
