[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebEstoque.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebEstoque.App_Start.NinjectWebCommon), "Stop")]


namespace WebEstoque.App_Start
{
    using System;
    using System.Web;
    using Aplication.Interfaces;
    using Aplication.OpenApp;
    using Domain.Interface;
    using Domain.Interface.Generica;
    using Domain.Interface.InterfaceService;
    using Domain.Interface.service;
    using Domain.service;
    using Infra.Generic;
    using Infra.Repository;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.Mvc;
    using WebEstoque.Interfaces;
    using WebEstoque.Repository;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Inicia a aplicação
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Para a aplicação
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Cria o Kernel Ninject e registra os serviços
        /// </summary>
        /// <returns>O kernel</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                // Aqui fazemos o registro de dependências
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Registrar as dependências (binds)
        /// </summary>
        /// <param name="kernel">O kernel Ninject</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IEstoqueWeb>().To<MovimentoEstoqueRepositoryWeb>();
            kernel.Bind<IMovimentoApp>().To<MovimentoEstoqueApp>();
            kernel.Bind<IMovimentoEstoqueService>().To<MovimentoEstoqueService>();
            kernel.Bind<IEstoque>().To<MovimentoEstoqueRepository>();

            kernel.Bind<IProdutoService>().To<ProdutoService>();
            kernel.Bind<IProduto>().To<ProdutoRepository>();
            kernel.Bind<IProdutoWeb>().To<ProdutoWebRepository>();
            kernel.Bind<IProdutoApp>().To<ProdutoApp>();


            kernel.Bind(typeof(IGenerica<>)).To(typeof(RepositoryGeneric<>));

            kernel.Bind<IFornecedorApp>().To<FornecedorApp>();
            kernel.Bind<IFornecedorService>().To<FornecedorService>();
            kernel.Bind<IFornecedor>().To<FornecedorRepository>();
            kernel.Bind<IFornecedorWeb>().To<FornecedorWebRepository>();
        }
        public static void RegisterMvcDependencyResolver()
        {
            var kernel = CreateKernel();
            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}

