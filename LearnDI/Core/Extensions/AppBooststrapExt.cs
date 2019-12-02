using Castle.Windsor.MsDependencyInjection;
using Core.DependencyInjection;
using Core.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class AppBooststrapExt
    {
        public static IServiceProvider Bootstrap(this IServiceCollection services, DIManager dIManager = null)
        {
            if (dIManager != null)
            {
                return WindsorRegistrationHelper.CreateServiceProvider(dIManager.IocContainer, services);
            }
            return WindsorRegistrationHelper.CreateServiceProvider(DIManager.Instance.IocContainer, services);
        }

        public static void ConfigApp(this IApplicationBuilder app)
        {
            //DIManager.Instance.RegisterDI(typeof(AppBootstrapExt).Assembly);
            DIManager.Instance.Register<ModuleManager>();
            DIManager.Instance.Resolve<ModuleManager>().Bootstrap();
        }
    }
}
