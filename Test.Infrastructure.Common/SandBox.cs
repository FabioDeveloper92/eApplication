using System;
using Autofac;
using Infrastructure.Core;
using MediatR;

namespace Test.Infrastructure.Common
{
    public class Sandbox : IDisposable
    {
        private readonly IContainer _container;

        public IMediator Mediator { get; }
        public IConnectionFactory ConnectionFactory { get; }

        public Sandbox(params Module[] modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
                builder.RegisterModule(module);

            _container = builder.Build();

            Mediator = _container.Resolve<IMediator>();
            ConnectionFactory = _container.Resolve<IConnectionFactory>();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}
