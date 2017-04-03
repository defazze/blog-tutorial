using Autofac;
using BlogTutorial.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTutorial2017
{
    public static class DependencyRegister
    {
        public static void AddIdentityStore(this ContainerBuilder builder)
        {
            builder.AddScoped<ApplicationUserStore>();
            builder.AddScoped<ApplicationRoleStore>();
        }

        private static void AddScoped<T>(this ContainerBuilder builder)
        {
            builder.RegisterType<T>().InstancePerLifetimeScope();
        }
    }
}
