using System.Text.Json.Serialization;

namespace DXKumaBot.Response.Lxns;

public sealed record LxnsCollectionRequiredSong
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("title")] public string? Title { get; set; }

    [JsonPropertyName("type")] public LxnsSongType Type { get; set; }

    [JsonPropertyName("completed")] public bool? Completed { get; set; }

    [JsonPropertyName("completed_difficulties")]
    public int[]? CompletedDifficulties { get; set; }
}