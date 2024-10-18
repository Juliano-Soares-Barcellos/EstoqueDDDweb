using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IFornecedorApp :IAppGenerica<Fornecedor>
    {
         Task<int> AdcionarForncedor(string fornecedor);
        Task<Fornecedor> Select(string fornecedor);
    }
}
 