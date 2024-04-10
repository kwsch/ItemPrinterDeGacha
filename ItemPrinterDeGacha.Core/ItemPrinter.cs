using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace ItemPrinterDeGacha.Core;

public static class ItemPrinter
{
    private const uint ItemRandMax = 10001;
    private const uint BallRandMax = 10001;
    private static readonly LotteryItemValue[] ItemTable;
    private static readonly LotteryItemValue[] BallTable;
    private static readonly byte[] ItemJump;
    private static readonly byte[] BallJump;

    public static bool IsInTable(int item, PrintMode mode) => TableHasItem(mode, (ushort)item);

    static ItemPrinter()
    {
        var resource = Properties.Resources.item_table_array;
        var text = Encoding.UTF8.GetString(resource);
        var regular = JsonSerializer.Deserialize<LotteryRoot>(text)!.Table;
        var orderR = regular
            .Select(x => x.Param.Value)
            .OrderBy(z => z.EmergePercent);
        ItemTable = [.. orderR];
        ItemJump = GenerateJumpTable(ItemTable, ItemRandMax);

        resource = Properties.Resources.special_item_table_array;
        text = Encoding.UTF8.GetString(resource);
        var balls = JsonSerializer.Deserialize<BallRoot>(text)!;
        var orderB = balls.Table
            .SelectMany(x => x.Param.Table)
            .OrderBy(z => z.EmergePercent);
        BallTable = [.. orderB];
        BallJump = GenerateJumpTable(BallTable, BallRandMax);
    }

    private static bool TableHasItem(PrintMode mode, ushort itemId)
    {
        var table = mode == PrintMode.BallBonus ? BallTable : ItemTable;
        return table.Any(x => x.ItemId == itemId);
    }

    private static byte[] GenerateJumpTable(ReadOnlySpan<LotteryItemValue> table, [ConstantExpected] uint maxRand)
    {
        var result = new byte[maxRand];
        uint index = maxRand - 1u;
        for (int i = table.Length - 1; i >= 0; i--)
        {
            var max = index;
            var item = table[i];
            var weight = item.EmergePercent;
            while (weight-- > 0)
                result[index--] = (byte)i;
            var min = index + 1u;

            // Debug Util
            item.MaxRoll = max;
            item.MinRoll = i == 0 ? 0 : min;
        }
        return result;
    }

    public static PrintMode Print(ulong ticks, Span<Item> result, PrintMode printMode)
    {
        var rand = new Xoroshiro128Plus(ticks);
        return Print(ref rand, result, printMode);
    }

    public static PrintMode Print(ref Xoroshiro128Plus rand, Span<Item> result, PrintMode printMode)
    {
        // Generate the result

        var table = printMode == PrintMode.BallBonus ? BallTable : ItemTable;
        var randMax = printMode == PrintMode.BallBonus ? BallRandMax : ItemRandMax;
        var jump = printMode == PrintMode.BallBonus ? BallJump : ItemJump;

        var checkBonus = false;
        var returnMode = PrintMode.Regular;
        foreach (ref var item in result)
        {
            // Always check for next bonus mode, even if not possible.
            var roll = rand.NextInt(1000);
            if (roll < 20)
                    checkBonus = true;

            var itemRoll = rand.NextInt(randMax); // total weights
            var index = jump[itemRoll];
            var param = table[index];
            var (min, max) = (param.LotteryItemNumMin, param.LotteryItemNumMax);
            var count = min == max ? min : min + rand.NextInt(max - min + 1);
            if (printMode == PrintMode.ItemBonus)
                count *= 2;

            item = new(param.ItemId, (ushort)count);
        }

        if (checkBonus && printMode == PrintMode.Regular) // Don't bother calculating for already-bonus modes.
            returnMode = (PrintMode)(1 + rand.NextInt(2));
        _ = rand.NextInt(100); // don't care about color rand, but roll it anyway
        return returnMode;
    }

    public static ulong FindNextBonusMode(ulong startTicks, PrintMode targetMode, int itemId = 0)
    {
        if (targetMode is not (PrintMode.ItemBonus or PrintMode.BallBonus))
            throw new ArgumentException("Invalid target mode", nameof(targetMode));
        if (itemId != 0 && !TableHasItem(PrintMode.Regular, (ushort)itemId))
            throw new ArgumentException("Item ID not found in the table", nameof(itemId));

        Span<Item> result = stackalloc Item[1]; // best case scenario
        while (true)
        {
            var resultMode = Print(startTicks, result, PrintMode.Regular);
            if (resultMode == targetMode && (itemId == 0 || result[0].ItemId == itemId))
                return startTicks;
            startTicks++;
        }
    }

    public static ulong FindNextItem(ulong startTicks, ushort itemId, PrintMode mode)
    {
        // Sanity check that the item exists in the table.
        if (!TableHasItem(mode, itemId))
            throw new ArgumentException("Item ID not found in the table", nameof(itemId));

        ulong ticks = startTicks;
        Span<Item> result = stackalloc Item[1]; // best case scenario
        while (true)
        {
            _ = Print(ticks, result, mode);
            if (result[0].ItemId == itemId)
                return ticks;
            ticks++;
        }
    }

    public static ulong FindNextRegular(ulong startTicks, ushort itemId) => FindNextItem(startTicks, itemId, PrintMode.Regular);
    public static ulong FindNextBall(ulong startTicks, ushort itemId) => FindNextItem(startTicks, itemId, PrintMode.BallBonus);
    public static ulong FindNextItemBonus(ulong startTicks, ushort itemId) => FindNextItem(startTicks, itemId, PrintMode.ItemBonus);

    public static (ulong Ticks, int Count) MaxResults(ushort itemId, ulong start, ulong end, PrintMode mode)
    {
        if (!TableHasItem(mode, itemId))
            throw new ArgumentException("Item ID not found in the table", nameof(itemId));

        ulong result = 0;
        int count = -1;

        // Just run on a single thread for now.
        Span<Item> items = stackalloc Item[10];
        for (ulong i = start; i <= end; i++)
        {
            _ = Print(i, items, mode);
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
            _ = Print(i, items, mode);
            int c = 0;
            foreach (var item in items)
            {
                if (find.Contains(item.ItemId))
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

    public static (ulong Ticks, int Count) MaxResultsAnyBall(ulong start, ulong end, Span<Item> best)
    {
        ulong result = 0;
        int count = -1;

        // Just run on a single thread for now.
        Span<Item> items = stackalloc Item[10];
        for (ulong i = start; i <= end; i++)
        {
            _ = Print(i, items, PrintMode.BallBonus);
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

public enum PrintMode
{
    Regular = 0,
    ItemBonus = 1,
    BallBonus = 2,
}

public readonly record struct Item(ushort ItemId, ushort Count);
