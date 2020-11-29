using System;
using Config.Ioc;
using Infrastructure.Write.Task;
using Test.Common;
using Test.Common.Builders;
using Xunit;

namespace Infrastructure.Write.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(
                new Module(configBuilder.Build()),
                new Write.Ioc.Module());
        }

        [Fact]
        public void should_resolve_ITaskWriteRepository()
        {
            _scopeResolver.IsSingleInstance<ITaskWriteRepository, TaskWriteRepository>();
        }

        [Fact]
        public void should_resolve_ITaskWriteMapper()
        {
            _scopeResolver.IsSingleInstance<ITaskWriteMapper, TaskWriteMapper>();
        }


        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}
