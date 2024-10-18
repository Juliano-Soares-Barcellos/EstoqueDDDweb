using Domain.Interface.Generica;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IEstoque : IGenerica<MovimentoEstoque>
    {
        Task<List<MovimentoEstoque>> ListarProdutosEntradasOuSaidas(string TipoMovimento);
        Task<List<MovimentoEstoque>> ListarProdutosTotalHistorico();
        Task <string> AddMovimentoEstoque(MovimentoEstoque Estoque);
        Task <string> UpdateMovimentoEstoqueQuantidade(MovimentoEstoque Estoque);
        Task <string> UpdateMovimentoEstoqueValor(MovimentoEstoque Estoque);
        Task<string> DeleteMovimentoEstoque(MovimentoEstoque Estoque);
        Task<MovimentoEstoque>SelectData(MovimentoEstoque Estoque);
        Task<MovimentoEstoque>Select(MovimentoEstoque Estoque);
        Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo,DateTime Datainit,DateTime fim);
        Task<List<MovimentoEstoque>> ListarProdutosEstoque();


        


    }
}
