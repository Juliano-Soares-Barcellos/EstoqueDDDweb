using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebEstoque.Interfaces
{
    public interface IProdutoWeb
    {
        Task<List<Produto>> SelectProduto();
        Task<Produto> SelectProdutoGetNome(string produto);
        Task<Produto> SelectProdutoGetId(int produto);
        Task UpdateNome(Produto produto);

    }
}