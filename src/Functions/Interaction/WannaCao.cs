using DXKumaBot.Bot.EventArg;
using DXKumaBot.Bot.Message;
using DXKumaBot.Utils;
using System.Text.RegularExpressions;

namespace DXKumaBot.Functions.Interaction;

public sealed partial class WannaCao : RegexFunctionBase
{
    private readonly WeightsRandom _random = new([11, 11, 11, 11, 11, 11, 11, 11, 11, 1]);

    private readonly (string, int)[] _replies =
    [
        ("变态！！！", 0),
        ("走开！！！", 0),
        ("别靠近迪拉熊！！！", 0),
        ("迪拉熊不和你玩了！", 0),
        ("信不信迪拉熊吃你绝赞！", 0),
        ("信不信迪拉熊吃你星星！", 0),
        ("你不能这样对迪拉熊！", 0),
        ("迪拉熊不想理你了，哼！", 0),
        ("不把白潘AP了就别想！", 0),
        ("……你会对迪拉熊负责的，对吧？", 1)
    ];

    private protected override async Task MainAsync(object? sender, MessageReceivedEventArgs args)
    {
        int index = _random.Next();
        (string Text, int PhotoIndex) reply = _replies[index];
        string filePath = Path.Combine("Static", nameof(WannaCao), $"{reply.PhotoIndex}.png");
        MediaMessage message = new(MediaType.Photo, filePath);
        await args.Message.ReplyAsync(new(reply.Text, message), true);
    }

    [GeneratedRegex("^(香草|想草)(迪拉熊|dlx)$", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    private protected override partial Regex MessageRegex();
}