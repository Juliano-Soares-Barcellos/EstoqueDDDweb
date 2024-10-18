using Domain.Interface;
using Domain.Interface.InterfaceService;
using Entities.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.service
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedor _Fornecedor;

        private Fornecedor FornecedorEntity = new Fornecedor();

        public FornecedorService(IFornecedor Fornecedor)
        {
            _Fornecedor = Fornecedor;
        }

        public async Task<int> AddFornecedor(string fornecedor)
        {
            return FornecedorEntity.CodFornecedor = await _Fornecedor.AddFornecedor(fornecedor);
        }

        public async Task<Fornecedor> SelectByNome(string nomeFornecedor)
        {
           return await _Fornecedor.SelectFornecedor(nomeFornecedor);
        }
    }
}
