using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using static ItemPrinterDeGacha.Core.PrintMode;

namespace ItemPrinterDeGacha.Core;

/// <summary>
/// Item Printer class to handle the item printing logic.
/// </summary>
public static class ItemPrinter
{
    // Assumptions to make calculations straightforward instead of having branching logic:
    // Player has both bonus modes unlocked -- otherwise, the bonus mode check won't call rand().
    // Player has unlocked Stellar Tera Shards -- the rand max would be different (less).

    // Rough summary of the item printer logic:
    // Once the games load the table, they sort the rewards by ascending weight (lowest first).
    // Then, the game determines the "total weight" by summing all weights.
    // Stellar Tera Shards require a specific event flag (storyline progress) -- we assume it's unlocked.
    // For each reward printed, the game will give a 2% chance to unlock a bonus mode on the next print set.
    // When determining which item to award:
    // Roll a random number between 0 and total weight.
    // Find the item whose weight range contains the random number.
    // - Iterate from Index 0 in the reward table;
    // - Subtract the weight from the random number until it's 0 or negative.
    // - The item at the current index is the one to award.
    // To save time, we precompute a "jump table" to find the item index directly from the random number.

    // Max values for the random number generator.
    // Sum of all weights in the table + 1. Same for both modes, but listed separately for clarity.
    private const uint ItemRandMax = 10001; // Stellar Tera Shards unlocked
    private const uint BallRandMax = 10001;

    // Reward tables for both modes.
    // Ball Table json is a different format, but it's manually adjusted (min/max counts are always 1 or 5).
    // This allows for a single Print routine to handle both tables.
    private static readonly LotteryItemValue[] ItemTable;
    private static readonly LotteryItemValue[] BallTable;

    // Precomputed jump tables for the item and ball tables.
    // rand() % maxRand -> index in the lottery table, rather than seeking via weights each roll.
    private static readonly byte[] ItemJump;
    private static readonly byte[] BallJump;

    /// <summary>
    /// All items that can be printed (excluding Ball mode).
    /// </summary>
    public static ReadOnlySpan<ushort> Items =>
    [
        23, 24, 25, 26, 27, 28, 29, 38, 39, 40, 41, 45, 46, 47, 48, 49, 51, 52, 53, 80, 81, 82, 83, 84, 85, 86, 87, 88,
        89, 90, 91, 92, 94, 106, 107, 108, 109, 110, 221, 222, 223, 229, 230, 231, 232, 233, 234, 235, 237, 238, 239,
        240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 269, 272, 273, 277, 281, 321, 322, 323, 324,
        325, 326, 327, 537, 541, 571, 580, 581, 582, 583, 645, 650, 795, 796, 849, 1109, 1110, 1111, 1112, 1113, 1114,
        1115, 1120, 1124, 1125, 1126, 1127, 1128, 1253, 1254, 1606, 1842, 1843, 1862, 1863, 1864, 1865, 1866, 1867,
        1868, 1869, 1870, 1871, 1872, 1873, 1874, 1875, 1876, 1877, 1878, 1879, 1885, 1886, 2401, 2403, 2404, 2482, 2549,
    ];

    /// <summary>
    /// All balls that can be printed (only in Ball mode).
    /// </summary>
    public static ReadOnlySpan<ushort> Balls =>
    [
        1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,492,493,494,495,496,497,498,499,576,851,
    ];

    /// <summary>
    /// Initializes the static data for the item printer.
    /// </summary>
    static ItemPrinter()
    {
        // The games store the regular lottery table in a FlatBuffer format.
        // These resources have been converted to JSON for easier parsing.
        // Read the resource as byte[] from the dll and let the utf8 parser decode it from json.
        var resource = Properties.Resources.item_table_array;
        var text = new Utf8JsonReader(resource);
        var regular = JsonSerializer.Deserialize<LotteryRoot>(ref text)!.Table;
        var orderR = regular
            .Select(x => x.Param.Value)
            .OrderBy(z => z.EmergePercent);
        ItemTable = [.. orderR];
        ItemJump = GenerateJumpTable(ItemTable, ItemRandMax);

        resource = Properties.Resources.special_item_table_array;
        text = new Utf8JsonReader(resource);
        var balls = JsonSerializer.Deserialize<BallRoot>(ref text)!;
        var orderB = balls.Table
            .SelectMany(x => x.Param.Table)
            .OrderBy(z => z.EmergePercent);
        BallTable = [.. orderB];
        BallJump = GenerateJumpTable(BallTable, BallRandMax);
    }

    /// <summary>
    /// Generates a jump table for the lottery rand() results to a specific reward index.
    /// </summary>
    /// <param name="table">Reward table to generate the jump table for.</param>
    /// <param name="maxRand">Maximum rand() result value from the RNG.</param>
    private static byte[] GenerateJumpTable(ReadOnlySpan<LotteryItemValue> table, [ConstantExpected] uint maxRand)
    {
        // The jump table is a precomputed array to quickly find the item index from the rand() result.
        // The game uses <= instead of <, resulting in the [0]th item having n+1 weight.
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
            item.MinRoll = min;
        }
        table[0].MinRoll = 0; // Handle quirk where the first item has n+1 weight.
        return result;
    }

    /// <summary>
    /// Checks if the table contains the specified item.
    /// </summary>
    /// <param name="mode">Printing mode to check.</param>
    /// <param name="itemId">Item ID to check.</param>
    /// <returns>True if the item is in the table, false otherwise.</returns>
    public static bool TableHasItem(PrintMode mode, ushort itemId)
    {
        var table = mode == BallBonus ? Balls : Items;
        // Item IDs are sorted, so we can use a binary search for faster results.
        return table.BinarySearch(itemId) >= 0;
    }

    /// <summary>
    /// Prints a set of items using the specified seed.
    /// </summary>
    /// <remarks>
    /// Used for single-print jobs.
    /// If performing multiple print jobs in the same session, use the <see cref="Print(ref Xoroshiro128Plus, Span{Item}, PrintMode)"/> overload.
    /// </remarks>
    /// <returns>
    /// The next print mode to use.
    /// </returns>
    public static PrintMode Print(ulong seed, Span<Item> result, PrintMode printMode)
    {
        var rand = new Xoroshiro128Plus(seed);
        return Print(ref rand, result, printMode);
    }

    /// <summary>
    /// Determines the printing animation.
    /// </summary>
    /// <remarks>You shouldn't care about the result of this method.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong CalculateAnimation(ref Xoroshiro128Plus rand)
    {
        // Animation color is irrelevant for this simulation.
        // We still need to roll it if the rand is reused in subsequent print jobs.
        return rand.NextInt(100);
    }

    /// <summary>
    /// Prints a set of items using the specified random number generator instance.
    /// </summary>
    /// <remarks>
    /// Used for subsequent print jobs, or if chaining multiple print jobs in the same session.
    /// If performing a subsequent print job, don't forget to call <see cref="CalculateAnimation"/> before the next print job.
    /// </remarks>
    /// <returns>
    /// The next print mode to use.
    /// </returns>
    public static PrintMode Print(ref Xoroshiro128Plus rand, Span<Item> result, PrintMode printMode)
    {
        // Gather the conditions for this print job.
        var table = printMode == BallBonus ? BallTable : ItemTable;
        var randMax = printMode == BallBonus ? BallRandMax : ItemRandMax;
        var jump = printMode == BallBonus ? BallJump : ItemJump;

        // Print all items we've been requested to print.
        // If we're in ItemBonus mode, double the amount of items printed.
        var returnMode = Regular;
        foreach (ref var item in result)
        {
            // Always check for next bonus mode, even if not possible.
            var roll = rand.NextInt(1000);
            var setBonusMode = roll < 20;

            // Determine the item to print.
            var itemRoll = rand.NextInt(randMax); // total weights
            var index = jump[itemRoll];
            var param = table[index];

            // Determine quantity for this item.
            var (min, max) = (param.LotteryItemNumMin, param.LotteryItemNumMax);
            var count = min == max ? min : min + rand.NextInt(max - min + 1);
            if (printMode == ItemBonus)
                count *= 2;

            // Store in the result buffer.
            item = new(param.ItemId, (ushort)count);

            // If we're lucky enough to get a bonus mode, pick one.
            // Assume the player has both modes unlocked.
            // If a bonus mode was previously set, don't recalculate.
            if (printMode == Regular && setBonusMode && returnMode == Regular)
                returnMode = (PrintMode)(1 + rand.NextInt(2));
        }

        return returnMode;
    }
}
