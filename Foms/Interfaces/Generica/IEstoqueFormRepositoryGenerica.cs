using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foms.Interfaces
{
    public interface IEstoqueFormRepositoryGenerica <t> where t : class
    {
        Task<List<MovimentoEstoque>> obterTodos();
    }
}
