using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IAppGenerica <t> where t : class
    {
        Task <string> Add(t objeto);
        Task Update(t objeto);
        Task Deletar(t objeto);
        Task <t>Select(t objeto);
    }
}
