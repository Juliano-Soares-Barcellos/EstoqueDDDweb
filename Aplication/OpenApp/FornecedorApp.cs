using Aplication.Interfaces;
using Domain.Interface;
using Domain.Interface.InterfaceService;
using Domain.service;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OpenApp
{
    public class FornecedorApp : IFornecedorApp
    {
        private readonly IFornecedorService _fornecedor;
        public FornecedorApp(IFornecedorService fornService)
        {
            _fornecedor = fornService;
        }

        public async Task<int> AdcionarForncedor(string fornecedor)
        {
            return await _fornecedor.AddFornecedor(fornecedor);
        }


        public Task Deletar(Fornecedor objeto)
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> Select(Fornecedor objeto)
        {
           return _fornecedor.SelectByNome(objeto.NomeFornecedor);
        }

        public Task<Fornecedor> Select(string fornecedor)
        {
            return _fornecedor.SelectByNome(fornecedor);
        }

        public Task Update(Fornecedor objeto)
        {
            throw new NotImplementedException();
        }

       async Task<string> IAppGenerica<Fornecedor>.Add(Fornecedor objeto)
        {
            int cod = await _fornecedor.AddFornecedor(objeto.NomeFornecedor);
            return cod.ToString();
        }
    }
}
