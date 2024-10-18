using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foms.Interfaces
{
    public interface IEstoque_FormRepository : IEstoqueFormRepositoryGenerica<MovimentoEstoque>
    {
        Task<DataTable> RetornarTabela();
        Task<string> AdcionarNovoMovimentoNoEstoque(MovimentoEstoque Estoque);
    }
}
