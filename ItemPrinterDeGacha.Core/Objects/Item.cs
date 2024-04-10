namespace ItemPrinterDeGacha.Core;

/// <summary>
/// Simple struct to represent a printed item result.
/// </summary>
/// <param name="ItemId">Item index that was printed.</param>
/// <param name="Count">Count of items for this print result.</param>
public readonly record struct Item(ushort ItemId, ushort Count);
