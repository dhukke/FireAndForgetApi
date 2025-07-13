
using FireAndForgetApi.FireAndForgetBus;
using System.Threading.Channels;

namespace FireAndForgetApi.Events;

public sealed class InMemoryMessageQueue
{
    private readonly Channel<IFireAndForgetEvent> _channel = Channel.CreateUnbounded<IFireAndForgetEvent>();

    public ChannelWriter<IFireAndForgetEvent> Writer => _channel.Writer;

    public ChannelReader<IFireAndForgetEvent> Reader => _channel.Reader;
}