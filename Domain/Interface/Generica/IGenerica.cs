using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Generica
{
    public interface IGenerica <t> where t : class
    {
       Task<int> ExecuteQuery(string query, MySqlParameter[] parameters);

        Task<T> Select<T> (string query, MySqlParameter[] parameters);
    }
}
