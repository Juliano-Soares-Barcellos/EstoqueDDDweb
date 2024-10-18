using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProdutoApp
    {
        Task<List<Produto>> SelectAllProduct();
        Task<Produto> SelectByGetNome(string produto);
        Task<Produto> SelectByGetId(int produto);
        Task UpdateNome(Produto produto);
    }
}
