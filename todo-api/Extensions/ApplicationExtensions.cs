using Microsoft.AspNetCore.Authorization;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using todo_repository;
using todo_service;

namespace ASPNetCoreMastersTodoList.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static WebApplicationBuilder AddDefaultDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IItemService, ItemService>();

            return builder;
        }

        public static WebApplicationBuilder AddAutofacDependencies(this WebApplicationBuilder builder)
        {
            // Call UseServiceProviderFactory on the Host sub property 
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<ItemRepository>().As<IItemRepository>().InstancePerLifetimeScope();
                builder.RegisterType<ItemService>().As<IItemService>().InstancePerLifetimeScope();
            });

            return builder;
        }
    }
}
