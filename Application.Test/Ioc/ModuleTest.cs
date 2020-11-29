using System;
using Application.Task.Commads;
using Application.Task.Queries;
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

        [Fact]
        public void should_resolve_TaskReadService()
        {
            _scopeResolver.IsInstancePerLifetimeScope<TaskReadService>();
        }

        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}
