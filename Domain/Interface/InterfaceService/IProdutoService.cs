using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.InterfaceService
{
    public interface IProdutoService
    {
        Task <Produto>AddProduto(string produto);
        Task UpdateProduto(Produto produto);
        Task Delete(Produto produto);
        Task<Produto> SelectByGetNome(Produto produto);
        Task<Produto> SelectByGetId(Produto produto);
        Task<List<Produto>> SelectAllProduto();

    }
}
