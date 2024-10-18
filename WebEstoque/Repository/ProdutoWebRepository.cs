using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aplication.Interfaces;
using Entities.Entities;
using WebEstoque.Interfaces;

namespace WebEstoque.Repository
{
    public class ProdutoWebRepository : IProdutoWeb
    {
        private readonly IProdutoApp produto;
        public ProdutoWebRepository(IProdutoApp produtos)
        {
            produto = produtos;
        }
        public Task<List<Produto>> SelectProduto()
        {
            return  produto.SelectAllProduct();
        }

        public async Task<Produto> SelectProdutoGetId(int produtos)
        {
            return await produto.SelectByGetId(produtos);
        }

        public async Task<Produto> SelectProdutoGetNome(string produtos)
        {
           return await produto.SelectByGetNome(produtos);
        }

        public async Task UpdateNome(Produto prod)
        {
            await produto.UpdateNome(prod);
        }
    }
}