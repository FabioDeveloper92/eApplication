using Autofac;

namespace Infrastructure.Core.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionFactory>()
                   .As<IConnectionFactory>()
                   .InstancePerLifetimeScope();
        }
    }
}
