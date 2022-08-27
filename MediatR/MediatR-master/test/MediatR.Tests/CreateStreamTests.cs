using System.Threading;

namespace MediatR.Tests;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Shouldly;
using StructureMap;
using Xunit;

public class CreateStreamTests
{

    public class Ping : IStreamRequest<Pong>
    {
        public string Message { get; set; }
    }

    public class Pong
    {
        public string Message { get; set; }
    }

    public class PingStreamHandler : IStreamRequestHandler<Ping, Pong>
    {
        public async IAsyncEnumerable<Pong> Handle(Ping request, [EnumeratorCancellation]CancellationToken cancellationToken)
        {
            yield return await Task.Run(() => new Pong { Message = request.Message + " Pang" });
        }
    }

    [Fact]
    public async Task Should_resolve_main_handler()
    {
        var container = new Container(cfg =>
        {
            cfg.Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(CreateStreamTests));
                scanner.IncludeNamespaceContainingType<Ping>();
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf(typeof(IStreamRequestHandler<,>));
            });
            cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => t => ctx.GetInstance(t));
            cfg.For<IMediator>().Use<Mediator>();
        });

        var mediator = container.GetInstance<IMediator>();

        var response = mediator.CreateStream(new Ping { Message = "Ping" });
        int i = 0;
        await foreach (Pong result in response)
        {
            if (i == 0)
            {
                result.Message.ShouldBe("Ping Pang");
            }

            i++;
        }

        i.ShouldBe(1);
    }

    [Fact]
    public async Task Should_resolve_main_handler_via_dynamic_dispatch()
    {
        var container = new Container(cfg =>
        {
            cfg.Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(CreateStreamTests));
                scanner.IncludeNamespaceContainingType<Ping>();
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf(typeof(IStreamRequestHandler<,>));
            });
            cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
            cfg.For<IMediator>().Use<Mediator>();
        });

        var mediator = container.GetInstance<IMediator>();

        object request = new Ping { Message = "Ping" };
        var response = mediator.CreateStream(request);
        int i = 0;
        await foreach (Pong result in response)
        {
            if (i == 0)
            {
                result.Message.ShouldBe("Ping Pang");
            }

            i++;
        }

        i.ShouldBe(1);
    }

    [Fact]
    public async Task Should_resolve_main_handler_by_specific_interface()
    {
        var container = new Container(cfg =>
        {
            cfg.Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(CreateStreamTests));
                scanner.IncludeNamespaceContainingType<Ping>();
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf(typeof(IStreamRequestHandler<,>));
            });
            cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => t => ctx.GetInstance(t));
            cfg.For<ISender>().Use<Mediator>();
        });

        var mediator = container.GetInstance<ISender>();
        var response = mediator.CreateStream(new Ping { Message = "Ping" });
        int i = 0;
        await foreach (Pong result in response)
        {
            if (i == 0)
            {
                result.Message.ShouldBe("Ping Pang");
            }

            i++;
        }

        i.ShouldBe(1);
    }
}