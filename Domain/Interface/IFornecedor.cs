using Domain.Interface.Generica;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IFornecedor : IGenerica<Fornecedor>
    {
        Task<int> AddFornecedor(string fornecedor);
      
        Task<Fornecedor> SelectFornecedor(string fornecedor);
        Task DeleteFornecedor(Fornecedor fornecedor);
    }
}
