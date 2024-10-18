using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aplication.Interfaces;
using Entities.Entities;
using WebEstoque.Interfaces;

namespace WebEstoque.Repository
{
    public class FornecedorWebRepository : IFornecedorWeb
    {
        private readonly IFornecedorApp _fornecedorApp;

        public FornecedorWebRepository(IFornecedorApp fornecedorApp)
        {
            _fornecedorApp = fornecedorApp;
        }

        public Task<int> AddFornecedor(string nomeFornecedor)
        {
           return _fornecedorApp.AdcionarForncedor(nomeFornecedor);
        }

        public Task<Fornecedor> Select(string fornecedor)
        {
            return _fornecedorApp.Select(fornecedor);
        }
    }
}