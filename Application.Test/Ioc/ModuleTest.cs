using System;
using Application.Task.Commads;
using Config.Ioc;
using Test.Common;
using Test.Common.Builders;
using Xunit;

namespace Application.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Module(configBuilder.Build()), new Application.Ioc.Module());
        }

        [Fact]
        public void should_resolve_TaskWriteService()
        {
            _scopeResolver.IsInstancePerLifetimeScope<TaskWriteService>();
        }

        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}
