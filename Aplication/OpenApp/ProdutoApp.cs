using Aplication.Interfaces;
using Domain.Interface;
using Domain.Interface.InterfaceService;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.OpenApp
{
    public class ProdutoApp : IProdutoApp
    {
        private Produto produtoEntity = new Produto();

        private readonly IProdutoService _Produto;

        public ProdutoApp(IProdutoService produto)
        {
            _Produto = produto;
        }
        public Task<List<Produto>> SelectAllProduct()
        {
            return _Produto.SelectAllProduto();
        }

        public async Task<Produto> SelectByGetNome(string produto)
        {
            produtoEntity =
                new Produto
                {
                    NomeProduto = produto
                };
            return await _Produto.SelectByGetNome(produtoEntity);
        }

        public async Task<Produto> SelectByGetId(int produto)
        {
            produtoEntity =
                new Produto
                {
                    CodProduto = produto
                };
            return await _Produto.SelectByGetId(produtoEntity);
        }

        public async Task UpdateNome(Produto prod)
        {
             await _Produto.UpdateProduto(prod);
        }
    }
}
