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
    public class MovimentoEstoqueApp : IMovimentoApp
    {
        private readonly IEstoque _Iestoque;
        private readonly IMovimentoEstoqueService _imovimentoEstoqueService;
        private readonly IProdutoService Iproduto;
        private string NomeProduto ="";
        private int Quantidade;


        public MovimentoEstoqueApp( IMovimentoEstoqueService imovimentoEstoqueService, IEstoque Iestoque,IProdutoService _produto)
        {
            _Iestoque = Iestoque;
            _imovimentoEstoqueService = imovimentoEstoqueService;
            Iproduto = _produto;
        }

      
        public async Task<string> AddMovSaida(MovimentoEstoque mov)
        {
            var Produto = mov.ValidaPropiedadeInt(mov.Quantidade, "Nome do fornecedor");
            if (Produto)
            {
                Produto produto = new Produto();
                NomeProduto = mov.Produto.NomeProduto;
                mov.Produto = await Iproduto.SelectByGetNome(mov.Produto);
                Quantidade = mov.Produto.Estoque;
                if (mov.Produto!=null)
                return await _imovimentoEstoqueService.AddSaidaEstoque(mov,Quantidade);
            }
            throw new ArgumentException("Quantidade do produto menor que zero");
        }

        public async Task Deletar(MovimentoEstoque objeto)
        {
            await _imovimentoEstoqueService.Delete(objeto);
        }

        public async Task<List<MovimentoEstoque>> ListarEstoqueEntradasOuSaidas(string TipoMovimento)
        {
            return await _imovimentoEstoqueService.ListarEstoqueEntradasOuSaidas(TipoMovimento);
        }
        public async Task<List<MovimentoEstoque>> ListarProdutosEstoque()
        {
            return await _imovimentoEstoqueService.ListarProdutosEstoque();
        }

        public async Task<List<MovimentoEstoque>> ListarEstoqueTotalHistorico()
        {
            return await _imovimentoEstoqueService.ListarHistorico();
        }
        public async Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo, DateTime init, DateTime fim)
        {
            return await _imovimentoEstoqueService.ListarEstoque_DatasEntradas_saida(tipo, init, fim);
        }

        public async Task<MovimentoEstoque> Select(MovimentoEstoque objeto)
        {
            return await _imovimentoEstoqueService.SelectById(objeto);
        }

        public async Task Update(MovimentoEstoque objeto)
        {
           await _imovimentoEstoqueService.UpdateEstoqueValor(objeto);
        }

        public async Task<string> UpdateQtd(MovimentoEstoque objeto)
        {
            return await _imovimentoEstoqueService.UpdateQuantidade(objeto);
        }

        public async Task<MovimentoEstoque> SelectData(MovimentoEstoque objeto)
        {
           return await _imovimentoEstoqueService.SelectData(objeto);
        }

        public async Task<string> Add(MovimentoEstoque Estoque)
        {
            Produto produto = new Produto();
            NomeProduto = Estoque.Produto.NomeProduto;
            Estoque.Produto = await Iproduto.SelectByGetNome(Estoque.Produto);
            if (Estoque.Produto == null)
            {
                produto = await Iproduto.AddProduto(NomeProduto.ToString().ToLower());
                Estoque.Produto = new Produto
                {
                    CodProduto = produto.CodProduto,
                    NomeProduto = NomeProduto
                };
            }

            return await _imovimentoEstoqueService.AddEntradaEstoque(Estoque);
        }
    }
}
