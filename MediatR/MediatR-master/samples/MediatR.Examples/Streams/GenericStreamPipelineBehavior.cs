using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Examples;

public class GenericStreamPipelineBehavior<TRequest, TResponse> : IStreamPipelineBehavior<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly TextWriter _writer;

    public GenericStreamPipelineBehavior(TextWriter writer)
    {
        _writer = writer;
    }

    public async IAsyncEnumerable<TResponse> Handle(TRequest request, [EnumeratorCancellation]CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next)
    {
        await _writer.WriteLineAsync("-- Handling StreamRequest");
        await foreach (var response in next().WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return response;
        }
        await _writer.WriteLineAsync("-- Finished StreamRequest");
    }
}