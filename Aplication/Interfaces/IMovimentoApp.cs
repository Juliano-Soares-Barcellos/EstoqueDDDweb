using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IMovimentoApp : IAppGenerica<MovimentoEstoque>
    {
        Task <string> AddMovSaida(MovimentoEstoque mov);
        Task<List<MovimentoEstoque>> ListarEstoqueEntradasOuSaidas(string TipoMovimento);
        Task<List<MovimentoEstoque>> ListarHistorico();
        Task <string>UpdateQtd(MovimentoEstoque objeto);
        Task <MovimentoEstoque> SelectData(MovimentoEstoque objeto);
        Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo, DateTime init, DateTime fim);
        Task<List<MovimentoEstoque>> ListarProdutosEstoque();
        Task<List<MovimentoEstoque>> ProcurarPeloNome(string nomeProduto);


    }
}
