using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.InterfaceService
{
    public interface IMovimentoEstoqueService
    {
        Task<string> AddEntradaEstoque(MovimentoEstoque Estoque);
        Task<string> AddSaidaEstoque(MovimentoEstoque Estoque,int Quantidade);
        Task<string> UpdateQuantidade(MovimentoEstoque Estoque);
        Task <string>UpdateEstoqueValor(MovimentoEstoque Estoque);
        Task<string> Delete(MovimentoEstoque Estoque);
        Task<MovimentoEstoque> SelectById(MovimentoEstoque Estoque);
        Task<List<MovimentoEstoque>> ListarEstoqueEntradasOuSaidas(string TipoMovimento);
        Task<List<MovimentoEstoque>> ListarHistorico();
        Task<MovimentoEstoque> SelectData(MovimentoEstoque Estoque);

        Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo, DateTime init, DateTime fim);
        Task<List<MovimentoEstoque>> ListarProdutosEstoque();
        Task<List<MovimentoEstoque>> ProcurarPeloNome(string nomeProduto);
    }
}
