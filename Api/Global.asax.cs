using Aplication.Interfaces;
using Aplication.OpenApp;
using Domain.Interface;
using Domain.Interface.Generica;
using Domain.Interface.InterfaceService;
using Domain.Interface.service;
using Infra.Generic;
using Microsoft.Extensions.DependencyInjection;
using Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;



namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            DependencyResolver.SetResolver(new MicrosoftDependencyResolver(serviceProvider));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMovimentoApp, MovimentoEstoqueApp>();
            services.AddTransient<IMovimentoEstoqueService, MovimentoEstoqueService>();
            services.AddTransient<IEstoque, MovimentoEstoqueRepository>();
            services.AddTransient(typeof(IGenerica<>), typeof(RepositoryGeneric<>));
        }
    }
}
