using static ItemPrinterDeGacha.Core.PrintMode;

namespace ItemPrinterDeGacha.Core;

/// <summary>
/// Search routines for finding seeds that match certain criteria.
/// </summary>
public static class SeedSearch
{
    /// <summary>
    /// Searches for the next activation of a bonus mode.
    /// </summary>
    /// <param name="startTicks">Starting seed to search from.</param>
    /// <param name="targetMode">Desired bonus mode to find.</param>
    /// <param name="itemId">Optional item ID to search for.</param>
    /// <returns>The seed that activates the bonus mode.</returns>
    /// <remarks>
    /// Iterates upwards from the starting seed until the desired bonus mode is found.
    /// </remarks>
    public static ulong FindNextBonusMode(ulong startTicks, PrintMode targetMode, int itemId = 0)
    {
        if (targetMode is not (ItemBonus or BallBonus))
            throw new ArgumentException("Invalid target mode", nameof(targetMode));
        if (itemId != 0 && !ItemPrinter.TableHasItem(Regular, (ushort)itemId))
            throw new ArgumentException("Item ID not found in the table", nameof(itemId));

        Span<Item> result = stackalloc Item[1]; // best case scenario
        while (true)
        {
            var resultMode = ItemPrinter.Print(startTicks, result, Regular);
            if (resultMode == targetMode && (itemId == 0 || result[0].ItemId == itemId))
                return startTicks;
            startTicks++;
        }
    }

    public static ulong FindNextItem(ulong startTicks, ushort itemId, PrintMode mode)
    {
        // Sanity check that the item exists in the table.
        if (!ItemPrinter.TableHasItem(mode, itemId))
            throw new ArgumentException("Item ID not found in the table", nameof(itemId));

        ulong ticks = startTicks;
        Span<Item> result = stackalloc Item[1]; // best case scenario
        while (true)
        {
            _ = ItemPrinter.Print(ticks, result, mode);
            if (result[0].ItemId == itemId)
                return ticks;
            ticks++;
        }
    }

    public static ulong FindNextRegular(ulong startTicks, ushort itemId) => FindNextItem(startTicks, itemId, Regular);
    public static ulong FindNextBall(ulong startTicks, ushort itemId) => FindNextItem(startTicks, itemId, BallBonus);
    public static ulong FindNextItemBonus(ulong startTicks, ushort itemId) => FindNextItem(startTicks, itemId, ItemBonus);

    public static (ulong Ticks, int Count) MaxResults(ushort itemId, ulong start, ulong end, PrintMode mode)
    {
        if (!ItemPrinter.TableHasItem(mode, itemId))
            throw new ArgumentException("Item ID not found in the table", nameof(itemId));

        ulong result = 0;
        int count = -1;

        // Just run on a single thread for now.
        Span<Item> items = stackalloc Item[10];
        for (ulong i = start; i <= end; i++)
        {
            _ = ItemPrinter.Print(i, items, mode);
            int c = 0;
            foreach (var item in items)
            {
                if (item.ItemId == itemId)
                    c += item.Count;
            }

            if (c <= count)
                continue;

            count = c;
            result = i;
        }
        return (result, count);
    }

    public static (ulong Ticks, int Count) MaxResultsAny(ulong start, ulong end, Span<Item> best, PrintMode mode, params int[] find)
    {
        ulong result = 0;
        int count = -1;

        // Just run on a single thread for now.
        Span<Item> items = stackalloc Item[best.Length];
        for (ulong i = start; i <= end; i++)
        {
            _ = ItemPrinter.Print(i, items, mode);
            int c = 0;
            foreach (var item in items)
            {
                if (find.Contains(item.ItemId))
                    c += item.Count;
            }

            if (c <= count)
                continue;

            count = c;
            result = i;
            items.CopyTo(best);
        }
        return (result, count);
    }

    public static (ulong Ticks, int Count) MaxResultsAnyBall(ulong start, ulong end, Span<Item> best)
    {
        ulong result = 0;
        int count = -1;

        // Just run on a single thread for now.
        Span<Item> items = stackalloc Item[10];
        for (ulong i = start; i <= end; i++)
        {
            _ = ItemPrinter.Print(i, items, BallBonus);
            int c = 0;
            foreach (var item in items)
            {
                if (item.Count == 1)
                    c++;
            }

            if (c <= count)
                continue;

            count = c;
            result = i;
            items.CopyTo(best);
        }
        return (result, count);
    }
}
