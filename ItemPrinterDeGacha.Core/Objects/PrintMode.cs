namespace ItemPrinterDeGacha.Core;

/// <summary>
/// Item Printer print mode.
/// </summary>
public enum PrintMode
{
    /// <summary>
    /// Regular print mode. Can trigger <see cref="ItemBonus"/> or <see cref="BallBonus"/>.
    /// </summary>
    Regular = 0,

    /// <summary>
    /// Same as regular mode, but prints 2x the amount of items.
    /// </summary>
    ItemBonus = 1,

    /// <summary>
    /// Prints from a special table that only contains balls.
    /// </summary>
    BallBonus = 2,
}
