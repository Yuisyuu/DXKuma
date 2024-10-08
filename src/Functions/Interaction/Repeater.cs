using DXKumaBot.Bot.EventArg;
using DXKumaBot.Bot.Message;
using Lagrange.Core.Message.Entity;
using System.Collections.Concurrent;

namespace DXKumaBot.Functions.Interaction;

public sealed class Repeater
{
    private readonly ConcurrentDictionary<long, (string, int)> _lastMessages = new();

    public async Task EntryAsync(object? sender, MessageReceivedEventArgs args)
    {
        if (args.Message.SourceType is MessageSource.Qq)
        {
            return;
        }

        if (!args.Message.QqMessage!.All(x => x is TextEntity))
        {
            _lastMessages.Remove(args.Message.ChatId, out _);
            return;
        }

        if (!_lastMessages.TryGetValue(args.Message.ChatId, out (string Text, int Times) lastMessage) ||
            lastMessage.Text != args.Message.Text)
        {
            _lastMessages[args.Message.ChatId] = (args.Message.Text!, 1);
            return;
        }

        if (lastMessage.Times > 2)
        {
            return;
        }

        ++lastMessage.Times;
        if (lastMessage.Times is 3)
        {
            await args.Message.ReplyAsync(new(args.Message.Text), true);
        }

        _lastMessages[args.Message.ChatId] = lastMessage;
    }
}