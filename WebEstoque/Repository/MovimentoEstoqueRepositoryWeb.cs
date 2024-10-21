using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aplication.Interfaces;
using Entities.Entities;
using WebEstoque.Interfaces;
using WebEstoque.Interfaces.Generica;

namespace WebEstoque.Repository
{
    public class MovimentoEstoqueRepositoryWeb : IEstoqueWeb , IGenerica<MovimentoEstoque>
    {
        private readonly IMovimentoApp _imov;

        public MovimentoEstoqueRepositoryWeb(IMovimentoApp imov)
        {
            _imov = imov;
        }

        public async Task<string> Add(MovimentoEstoque objeto)
        {
            return await _imov.Add(objeto);
        }

    
        public async Task AddSaidaEstoque(MovimentoEstoque objeto)
        {
            await _imov.AddMovSaida(objeto);
        }

        public Task BuscaId(int i)
        {
            throw new NotImplementedException();
        }

        public async Task Deletar(MovimentoEstoque objeto)
        {
            await _imov.Deletar(objeto);
        }

        public async Task<List<MovimentoEstoque>> ListaEntradaOuSaidas(string EntradaSaidaEstoque)
        {
            return await _imov.ListarEstoqueEntradasOuSaidas(EntradaSaidaEstoque);
        }
        public async Task<List<MovimentoEstoque>> ListarProdutosEstoque()
        {
            return await _imov.ListarProdutosEstoque();
        }
        public async Task<List<MovimentoEstoque>> ProcurarPeloNome(string nomeProduto)
        {
            return await _imov.ProcurarPeloNome(nomeProduto);
        }

        public async Task<List<MovimentoEstoque>> Listar()
        {
          return await _imov.ListarHistorico();
        }

        public async  Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo, DateTime init, DateTime fim)
        {
            return await _imov.ListarEstoque_DatasEntradas_saida(tipo,init,fim);
        }


        public async Task<MovimentoEstoque> Select(MovimentoEstoque objeto)
        {
            return await _imov.Select(objeto);
        }

        public async Task update(MovimentoEstoque objeto)
        {
            await _imov.Update(objeto);
        }

        public async Task UpdateQuantidade(MovimentoEstoque objeto)
        {
            await _imov.UpdateQtd(objeto);
        }

    }
}