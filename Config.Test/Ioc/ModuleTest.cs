using System;
using Config.Ioc;
using FluentAssertions;
using Test.Common;
using Test.Common.Builders;
using Xunit;

namespace Config.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Module(configBuilder.Build()));
        }


        [Fact]
        public void should_resolve_Admins()
        {
            _scopeResolver.IsSingleInstance<Admins>();
            _scopeResolver.Resolve<Admins>().AdminsList.Should().Contain("Fabio");
        }

        [Fact]
        public void should_resolve_Database()
        {
            _scopeResolver.IsSingleInstance<Database>();
            _scopeResolver.Resolve<Database>().Schema.Should().Be("dbo");
        }

        [Fact]
        public void should_resolve_SqlConnectionString()
        {
            _scopeResolver.IsSingleInstance<SqlConnectionString>();
            _scopeResolver.Resolve<SqlConnectionString>().Value.Should().Be("Server=tcp:localhost,1433;Initial Catalog=Fabio_Test;Persist Security Info=False;User ID=sa;Password=Fabio1234;MultipleActiveResultSets=False;Connection Timeout=30;");
        }

        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}
