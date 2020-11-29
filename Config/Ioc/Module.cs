using System.Linq;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Config.Ioc
{
    public class Module : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public Module(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
                 new Admins
                 {
                     AdminsList = _configuration.GetSection("Admins:AdminsList").Value.Split('|').ToList()
                 })
             .SingleInstance();

            builder.Register(c =>
                   new Database
                   {
                       Schema = _configuration.GetSection("Database:Schema").Value
                   })
               .SingleInstance();

            builder.Register(c =>
                (SqlConnectionString)
                _configuration.GetConnectionString("SqlConnectionString")).SingleInstance();

        }
    }
}
