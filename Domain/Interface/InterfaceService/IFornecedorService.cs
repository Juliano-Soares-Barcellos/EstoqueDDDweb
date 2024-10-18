using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.InterfaceService
{
    public interface IFornecedorService
    {
        Task<int> AddFornecedor(string nomeFornecedor);
        Task<Fornecedor> SelectByNome(string nomeFornecedor);
    }
}
