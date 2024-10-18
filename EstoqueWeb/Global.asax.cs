using Aplication.Interfaces;
using Aplication.OpenApp;
using Domain.Interface;
using Domain.Interface.Generica;
using Domain.Interface.InterfaceService;
using Domain.Interface.service;
using EstoqueWeb;
using EstoqueWeb.App_Start;
using Infra.Generic;
using Infra.Repository;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebEstoque.Interfaces;
using WebEstoque.Repository;

namespace WebEstoque
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            NinjectWebCommon.RegisterMvcDependencyResolver();
        }



    }
}
