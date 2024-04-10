namespace ItemPrinterDeGacha.Core;

// classes to model the json data.

public class LotteryRoot
{
    public required LotteryTable[] Table { get; set; }
}

public class LotteryTable
{
    public required LotteryItemParam Param { get; set; }
}

public class LotteryItemParam
{
    public required string FlagName { get; set; }
    public required LotteryItemValue Value { get; set; }
}

public class LotteryItemValue
{
    public ushort ItemId { get; set; }
    public int ProductionPriority { get; set; }
    public int EmergePercent { get; set; }
    public uint LotteryItemNumMin { get; set; }
    public uint LotteryItemNumMax { get; set; }

    // Manual Tags
    public uint MinRoll { get; set; }
    public uint MaxRoll { get; set; }

    public override string ToString() => $"[{EmergePercent}: {MinRoll}-{MaxRoll}] ({ProductionPriority}) {ItemId} {GameStrings.GetItemName(ItemId)}";
}

public class BallRoot
{
    public required BallTable[] Table { get; set; }
}

public class BallTable
{
    public required BallParam Param { get; set; }
}

public class BallParam
{
    public required LotteryItemValue[] Table { get; set; }
}
