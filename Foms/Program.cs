using Aplication.Interfaces;
using Aplication.OpenApp;
using Domain.Interface;
using Domain.Interface.Generica;
using Domain.Interface.InterfaceService;
using Domain.Interface.service;
using Foms;
using Foms.Forms;
using Foms.Interfaces;
using Foms.Repository;
using Infra.Generic;
using Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace YourNamespace
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);  
            ServiceProvider = services.BuildServiceProvider();

            var mainForm = ServiceProvider.GetRequiredService<EstoqueForm>();
            Application.Run(mainForm);
        }


        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<IEstoque_FormRepository, EstoqueFormRepository>();
            services.AddScoped<IMovimentoApp, MovimentoEstoqueApp>();
            services.AddScoped<IMovimentoEstoqueService, MovimentoEstoqueService>();
            services.AddScoped<IEstoque,MovimentoEstoqueRepository>();
            services.AddScoped(typeof(IGenerica<>), typeof(RepositoryGeneric<>));


            // Registrar o formulário
            services.AddScoped<EstoqueForm>();
        }
    }
}
