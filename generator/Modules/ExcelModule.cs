using Autofac;
using generator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Modules
{
    class ExcelModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Excel>()
                            .As<IExcel>()
                           .SingleInstance();
        }
    }
}
