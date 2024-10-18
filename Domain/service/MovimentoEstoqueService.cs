using Domain.Interface.InterfaceService;
using Domain.Kernel;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface.service
{
    public class MovimentoEstoqueService : KernelDomain,IMovimentoEstoqueService
    {
        private readonly IEstoque movimentoEstoque;
        private readonly IProdutoService Iproduto;


        public MovimentoEstoqueService(IEstoque ImovimentoEstoque, IProdutoService iproduto)
        {
            movimentoEstoque = ImovimentoEstoque;
            Iproduto = iproduto;
        }


        public async Task<string> AddEntradaEstoque(MovimentoEstoque Estoque)
        {
            if (Estoque.Quantidade > 0)
            {
                Estoque.FkProduto = Estoque.Produto.CodProduto;
                Estoque.ValorUnitario = validaPonto(Estoque.ValorUnitario.ToString());
                return await movimentoEstoque.AddMovimentoEstoque(Estoque);
            }
            throw new ArgumentException("Estoque menor que zero");

        }
        public async Task<string> AddSaidaEstoque(MovimentoEstoque Estoque, int Quantidade)
        {
            int quantidadeEmEstoque = Quantidade - Estoque.Quantidade;

            if (Estoque.Produto.Estoque > 0 && quantidadeEmEstoque >= 0)
            {
                Estoque.FkProduto = Estoque.Produto.CodProduto;
                return await movimentoEstoque.AddMovimentoEstoque(Estoque);

            }
            throw new ArgumentException($"você esta tentando tirar uma quantidade maior do que você tem em estoque no momento " +
                $"A quantidade atual do produto é de {Quantidade} e você esta tentando tirar {Estoque.Quantidade}");
        }

        public async Task<string> UpdateQuantidade(MovimentoEstoque Estoque)
        {
            Estoque = await SelectById(Estoque);
            var QtdProduto = Estoque.ValidaPropiedadeInt(Estoque.Quantidade, "Quantidade do produto");

            if (QtdProduto && Estoque != null)
            {
                return await movimentoEstoque.UpdateMovimentoEstoqueQuantidade(Estoque);
            }

            return QtdProduto.ToString();
        }

        public async Task<string> UpdateEstoqueValor(MovimentoEstoque Estoque)
        {
            Estoque = await SelectById(Estoque);
            var valor = Estoque.ValidaPropiedadeDecimal(Estoque.ValorUnitario, "Valor unitario");
            if (valor && Estoque != null)
            {
                return await movimentoEstoque.UpdateMovimentoEstoqueValor(Estoque);
            }

            return valor.ToString();
        }

        public async Task<string> Delete(MovimentoEstoque Estoque)
        {
            Estoque = await SelectById(Estoque);
            var codigoMov = Estoque.ValidaPropiedadeInt(Estoque.CodMovimentacao, "Codigo do movimento");
            if (codigoMov && Estoque != null)
            {
                return await movimentoEstoque.DeleteMovimentoEstoque(Estoque);
            }

            return codigoMov.ToString();
        }

        public async Task<MovimentoEstoque> SelectById(MovimentoEstoque Estoque)
        {
            var codigoMov = Estoque.ValidaPropiedadeInt(Estoque.CodMovimentacao, "Codigo do movimento");
            if (codigoMov && Estoque != null)
            {
                return await movimentoEstoque.Select(Estoque);
            }

            return null; ;
        }

        public async Task<List<MovimentoEstoque>> ListarEstoqueEntradasOuSaidas(string TipoMovimento)
        {
            return await movimentoEstoque.ListarProdutosEntradasOuSaidas(TipoMovimento);
        }

        public async Task<List<MovimentoEstoque>> ListarProdutosEstoque()
        {
            return await movimentoEstoque.ListarProdutosEstoque();
        }

        public async Task<List<MovimentoEstoque>> ListarHistorico()
        {
            return await movimentoEstoque.ListarProdutosTotalHistorico();
        }

        public async Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo, DateTime init, DateTime fim)
        {
            return await movimentoEstoque.ListarEstoque_DatasEntradas_saida(tipo, init, fim);
        }

        public async Task<MovimentoEstoque> SelectData(MovimentoEstoque Estoque)
        {
            return await movimentoEstoque.SelectData(Estoque);
        }

       
    }
}
