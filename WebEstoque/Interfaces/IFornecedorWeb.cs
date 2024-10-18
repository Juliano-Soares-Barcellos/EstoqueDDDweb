using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEstoque.Interfaces
{
    public interface IFornecedorWeb
    {
        Task<int> AddFornecedor(string nomeFornecedor);

        Task<Fornecedor> Select(string fornecedor);
    }
}
