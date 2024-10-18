using Domain.Interface;
using Entities.Entities;
using Infra.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class FornecedorRepository : RepositoryGeneric<Fornecedor>, IFornecedor
    {
        MySqlParameter[] parametro = null;
        public async Task<int> AddFornecedor(string nomefornecedor)
        {
            
            string query = "INSERT INTO fornecedor (NomeFornecedor) VALUES (@NomeFornecedor);SELECT LAST_INSERT_ID();";
            parametro = new MySqlParameter[]
            {
                new MySqlParameter ("@NomeFornecedor" ,nomefornecedor)
            };
            return await ExecuteQuery(query, parametro);
        }

        public async Task DeleteFornecedor(Fornecedor fornecedor)
        {
            string sql = "DELETE FROM fornecedor WHERE (CodFornecedor = @cod);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@cod" , fornecedor.CodFornecedor),
            };
            await ExecuteQuery(sql, parametro);
        }
    

        public async Task<Fornecedor> SelectFornecedor(string fornecedor)
        {
        string sql = "Select * FROM fornecedor WHERE (NomeFornecedor = @nomeFornecedor);";
        parametro = new MySqlParameter[]
        {
                    new MySqlParameter ("@nomeFornecedor" , fornecedor),
        };
        return await Select<Fornecedor>(sql, parametro);
    }
    }
}
