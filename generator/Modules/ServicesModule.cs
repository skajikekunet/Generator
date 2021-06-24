using Autofac;
using generator.Classes;
using generator.Host;
using generator.Interfaces;
using Microsoft.Extensions.Logging;

namespace generator.Modules
{
    class ServicesModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
           /* builder.RegisterType<Excel>()
                            .As<IExcel>()
                           .SingleInstance();*/

            builder.RegisterType<Event>()
                            .As<IEvent>()
                           .SingleInstance();

            builder.RegisterType<Main>()
                            .As<IMain>()
                           .SingleInstance();

        }
    }
}
