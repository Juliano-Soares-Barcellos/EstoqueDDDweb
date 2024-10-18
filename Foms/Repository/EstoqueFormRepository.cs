using Aplication.Interfaces;
using Entities.Entities;
using Foms.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foms.Repository
{
    public class EstoqueFormRepository : IEstoque_FormRepository, IEstoqueFormRepositoryGenerica<MovimentoEstoque>
    {
        private readonly IMovimentoApp _imovimentoApp;
        public EstoqueFormRepository(IMovimentoApp imov)
        {
            _imovimentoApp = imov;
        }

        public Task<string> AdcionarNovoMovimentoNoEstoque(MovimentoEstoque Estoque)
        {
            return _imovimentoApp.Add(Estoque);
        }

        public async Task<List<MovimentoEstoque>> obterTodos()
        {
            return await _imovimentoApp.ListarEstoqueTotalHistorico();
        }

        public async Task<DataTable> RetornarTabela()
        {
               List<MovimentoEstoque> produtos = await obterTodos();

                var dataTable = new DataTable();
                DataRow row = null;

                dataTable.Columns.Add("Nome do Produto", typeof(string));
                dataTable.Columns.Add("Data da Modificacao", typeof(string));
                dataTable.Columns.Add("Quantidade", typeof(int));
                dataTable.Columns.Add("valor unitario", typeof(decimal));
                dataTable.Columns.Add("Fornecedor", typeof(string));
                dataTable.Columns.Add("Tipo de movimentacao do Estoque", typeof(string));

            if (produtos != null)
            {
                foreach (var item in produtos)
                {
                    row = dataTable.NewRow();
                    row["Nome do Produto"] = item.Produto.NomeProduto;
                    row["Data da Modificacao"] = item.DataMovimentacao.ToString();
                    row["Quantidade"] = item.Quantidade;
                    row["valor unitario"] = item.ValorUnitario==null?0: item.ValorUnitario;
                    row["Fornecedor"] = item.Fornecedor.NomeFornecedor;
                    row["Tipo de movimentacao do Estoque"] = item.tipomovimentacao.Descricao;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }
    }
}

