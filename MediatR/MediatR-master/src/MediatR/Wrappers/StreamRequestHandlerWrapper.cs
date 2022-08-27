namespace MediatR.Wrappers;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

internal abstract class StreamRequestHandlerBase : HandlerBase
{
    public abstract IAsyncEnumerable<object?> Handle(object request, CancellationToken cancellationToken, ServiceFactory serviceFactory);
}

internal abstract class StreamRequestHandlerWrapper<TResponse> : StreamRequestHandlerBase
{
    public abstract IAsyncEnumerable<TResponse> Handle(IStreamRequest<TResponse> request, CancellationToken cancellationToken,
        ServiceFactory serviceFactory);
}

internal class StreamRequestHandlerWrapperImpl<TRequest, TResponse> : StreamRequestHandlerWrapper<TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    public override async IAsyncEnumerable<object?> Handle(object request, [EnumeratorCancellation] CancellationToken cancellationToken, ServiceFactory serviceFactory)
    {
        await foreach (var item in Handle((IStreamRequest<TResponse>) request, cancellationToken, serviceFactory))
        {
            yield return item;
        }
    }

    public override async IAsyncEnumerable<TResponse> Handle(IStreamRequest<TResponse> request, [EnumeratorCancellation] CancellationToken cancellationToken, ServiceFactory serviceFactory)
    {
        IAsyncEnumerable<TResponse> Handler() => GetHandler<IStreamRequestHandler<TRequest, TResponse>>(serviceFactory).Handle((TRequest) request, cancellationToken);

        var items = serviceFactory
            .GetInstances<IStreamPipelineBehavior<TRequest, TResponse>>()
            .Reverse()
            .Aggregate(
                (StreamHandlerDelegate<TResponse>) Handler, 
                (next, pipeline) => () => pipeline.Handle(
                    (TRequest) request, 
                    cancellationToken, 
                    () => NextWrapper(next(), cancellationToken)
                )
            )();

        await foreach ( var item in items.WithCancellation(cancellationToken) )
        {
            yield return item;
        }
    }


    private static async IAsyncEnumerable<T> NextWrapper<T>(
        IAsyncEnumerable<T> items,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var cancellable = items
            .WithCancellation(cancellationToken)
            .ConfigureAwait(false);
        await foreach (var item in cancellable)
        {
            yield return item;
        }
    }

}