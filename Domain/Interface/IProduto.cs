using Domain.Interface.Generica;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IProduto : IGenerica<Produto>
    {
        Task <int>AddProduto(string Produto);
        Task UpdateProduto(Produto Produto);
        Task DeleteProduto(Produto Produto);
        Task<Produto>SelectProduto(Produto Produto);
        Task<Produto> SelectProdutoId(Produto Produto);
        Task<List<Produto>>SelectAllProduct();
    }
}
