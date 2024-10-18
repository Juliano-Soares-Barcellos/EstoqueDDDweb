using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEstoque.Interfaces.Generica
{
    public interface IGenerica <t> where t : class
    {
        Task Add(t objeto);
        Task<List<t>> Listar();
        Task Deletar(t objeto);
        Task update(t objeto);
        Task BuscaId(int i);
        Task<t> Select(t objeto);



    }
}
