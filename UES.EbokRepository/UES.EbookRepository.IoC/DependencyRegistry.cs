using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using UES.EbookRepository.BLL.Managers;
using UES.EbookRepository.DAL.Providers;

namespace UES.EbookRepository.IoC
{
   public  class DependencyRegistry
    {
        public void RegisterDependencies(ContainerBuilder builder)
        {
            // Providers
            builder.RegisterType<CategoryProvider>().AsImplementedInterfaces();
            builder.RegisterType<UserProvider>().AsImplementedInterfaces();
            builder.RegisterType<LanguageProvider>().AsImplementedInterfaces();
            builder.RegisterType<EbookProvider>().AsImplementedInterfaces();
            // Managers
            builder.RegisterType<CategoryManager>().AsImplementedInterfaces();
            builder.RegisterType<UserManager>().AsImplementedInterfaces();
            builder.RegisterType<LanguageManager>().AsImplementedInterfaces();
            builder.RegisterType<EbookManager>().AsImplementedInterfaces();
        }
    }
}
