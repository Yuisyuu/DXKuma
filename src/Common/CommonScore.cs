using DXKumaBot.Response;
using DXKumaBot.Response.Lxns;

namespace DXKumaBot.Common;

public sealed record CommonScore
{
    public int Id { get; set; }

    public LxnsSongType Type { get; set; }

    public LevelIndex LevelIndex { get; set; }

    public float Achievements { get; set; }

    public LxnsComboType? Combo { get; set; }

    public LxnsSyncType? Sync { get; set; }

    public int DxScore { get; set; }

    public int MaxDxScore { get; set; }

    public float? Ds { get; set; }
}