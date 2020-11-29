using Autofac;
using Infrastructure.Write.Task;

namespace Infrastructure.Write.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Core.Ioc.Module());

            builder.RegisterType<TaskWriteRepository>()
                   .As<ITaskWriteRepository>()
                   .SingleInstance();

            builder.RegisterType<TaskWriteMapper>()
                   .As<ITaskWriteMapper>()
                   .SingleInstance();
        }
    }
}
