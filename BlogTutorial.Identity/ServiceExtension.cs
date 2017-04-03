using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogTutorial.Identity
{
    public static class ServiceExtension
    {
        public static void AddIdentityStore(this ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserStore>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationRoleStore>().InstancePerLifetimeScope();
        }
    }
}
