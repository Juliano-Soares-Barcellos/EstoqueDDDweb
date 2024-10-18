using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebEstoque.Interfaces.Generica;

namespace WebEstoque.Interfaces
{
    public interface IEstoqueWeb : IGenerica <MovimentoEstoque>
    {
        Task<List<MovimentoEstoque>> ListaEntradaOuSaidas(string EntradaSaidaEstoque);
        Task UpdateQuantidade(MovimentoEstoque estoque);
        Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo, DateTime init, DateTime fim);

    }
}
