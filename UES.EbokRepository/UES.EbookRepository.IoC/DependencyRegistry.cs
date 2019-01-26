using Autofac;
using System;
using UES.EbookRepository.BLL.Managers;
using UES.EbookRepository.DAL.Providers;

namespace UES.EbookRepository.IoC
{
    public static class DependencyRegistry
    {
        public static IContainer RegisterDependencies(ContainerBuilder builder)
        {
            // Providers
            builder.RegisterType<CategoryProvider>().AsImplementedInterfaces();
            builder.RegisterType<LanguageProvider>().AsImplementedInterfaces();
            builder.RegisterType<UserProvider>().AsImplementedInterfaces();
            builder.RegisterType<EbookProvider>().AsImplementedInterfaces();
            // Managers
            builder.RegisterType<IndexManager>().AsImplementedInterfaces();
            builder.RegisterType<CategoryManager>().AsImplementedInterfaces();
            builder.RegisterType<LanguageManager>().AsImplementedInterfaces();
            builder.RegisterType<UserManager>().AsImplementedInterfaces();
            builder.RegisterType<EbookManager>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
