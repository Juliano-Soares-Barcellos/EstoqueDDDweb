using Domain.Interface.InterfaceService;
using Entities.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProduto _Produto;

        public ProdutoService(IProduto IProduto)
        {
            _Produto = IProduto;
        }

        public async Task<Produto> AddProduto(string produto)
        {
         
                Produto _produto = new Produto();

                _produto.CodProduto = await _Produto.AddProduto(produto);
                return _produto;

        }

        public async Task Delete(Produto produto)
        {
            var verificacao = produto.validaValorVazioOuNulo(produto.NomeProduto, "Nome do Produto", 1);

            if (verificacao)
                await _Produto.DeleteProduto(produto);
        }

        public async Task<List<Produto>> SelectAllProduto()
        {
          return await  _Produto.SelectAllProduct();
        }

        public async Task<Produto> SelectByGetId(Produto produto)
        {
            if(produto.ValidaPropiedadeInt(produto.CodProduto,"Codigo do produto"))
                return await _Produto.SelectProdutoId(produto);
            return null;
        }

        public async Task<Produto> SelectByGetNome(Produto produto)
        {
            if(produto.ValidaPropiedadeString(produto.NomeProduto,"Nome do produto"))
                return await _Produto.SelectProduto(produto);
            return null;
        }

        public async Task UpdateProduto(Produto produto)
        {
            var verificacao = produto.ValidaPropiedadeString(produto.NomeProduto, "Nome do Produto");

            if (verificacao)
                await _Produto.UpdateProduto(produto);
        }
    }
}
