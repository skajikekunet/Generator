using Autofac;
using generator.Interfaces.Templates;
using generator.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Modules
{
    class TemplateModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileTemplate>()
                            .As<IFileTemplate> ()
                           .SingleInstance();

            builder.RegisterType<GetArrays>()
                            .As<IGetArrays>()
                           .SingleInstance();

            builder.RegisterType<Options>()
                           .As<IOption>()
                          .SingleInstance();

            builder.RegisterType<ShortProcess>()
                           .As<IShortProcess>()
                          .SingleInstance();

            /* builder.RegisterType<Logger<MainHostService>>()
                             .As<ILogger>()
                            .SingleInstance();*/

        }
    }
}

