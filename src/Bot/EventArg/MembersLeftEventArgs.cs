using DXKumaBot.Bot.Message;
using Lagrange.Core.Event.EventArg;

namespace DXKumaBot.Bot.EventArg;

public class MembersLeftEventArgs : EventArgs
{
    private readonly IBot _bot;

    public MembersLeftEventArgs(IBot bot, GroupMemberDecreaseEvent message)
    {
        _bot = bot;
        QqMessage = message;
        SourceType = MessageSource.Qq;
    }

    public MembersLeftEventArgs(IBot bot, TgMessage message)
    {
        _bot = bot;
        TgMessage = message;
        SourceType = MessageSource.Telegram;
    }

    public MessageSource SourceType { get; }
    public GroupMemberDecreaseEvent? QqMessage { get; }
    public TgMessage? TgMessage { get; }

    public string UserId => SourceType switch
    {
#pragma warning disable VSTHRD002
#pragma warning disable VSTHRD104
        MessageSource.Qq => ((QqBot)_bot).GetUserInfoAsync(QqMessage!.MemberUin).Result!.Qid ??
                            QqMessage.MemberUin.ToString(),
#pragma warning restore VSTHRD104
#pragma warning restore VSTHRD002
        MessageSource.Telegram => TgMessage!.LeftChatMember!.Username!,
        _ => throw new ArgumentOutOfRangeException(nameof(SourceType), SourceType, null)
    };

    public string UserName => SourceType switch
    {
#pragma warning disable VSTHRD002
#pragma warning disable VSTHRD104
        MessageSource.Qq => ((QqBot)_bot).GetUserInfoAsync(QqMessage!.MemberUin).Result!.Nickname,
#pragma warning restore VSTHRD104
#pragma warning restore VSTHRD002
        MessageSource.Telegram => $"{TgMessage!.LeftChatMember!.FirstName}{TgMessage!.LeftChatMember!.LastName}",
        _ => throw new ArgumentOutOfRangeException(nameof(SourceType), SourceType, null)
    };

    public async Task ReplyAsync(MessagePair messages)
    {
        await _bot.SendMessageAsync(messages, SourceType switch
        {
            MessageSource.Qq => QqMessage!.GroupUin,
            MessageSource.Telegram => TgMessage!.Chat.Id,
            _ => throw new ArgumentOutOfRangeException(nameof(SourceType), SourceType, null)
        }, SourceType is MessageSource.Telegram ? TgMessage : default);
    }
}