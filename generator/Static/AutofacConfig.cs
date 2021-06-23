using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Static
{

    public static class AutofacConfig
    {
        public static IContainer ConfigureExcel(string path)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Excel>()
        .As<IExcel>()
        .WithParameters(new List<Parameter> { new NamedParameter("path", path) });
            return builder.Build();
         
        }


    }
}
