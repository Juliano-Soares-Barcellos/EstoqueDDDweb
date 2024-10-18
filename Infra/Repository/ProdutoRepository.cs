using Domain.Interface;
using Entities.Entities;
using Infra.Generic;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ProdutoRepository : RepositoryGeneric<Produto>, IProduto
    {
        private MySqlParameter[] parametro = null;


        public async Task<int> AddProduto(string Produto)
        {
            string query = "INSERT INTO produto (NomeProduto) VALUES (@NomeProduto);SELECT LAST_INSERT_ID();";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@NomeProduto" ,Produto)
            };

            return await ExecuteQuery(query, parametro);
        }

        public async Task DeleteProduto(Produto Produto)
        {
            string sql = "DELETE FROM produto WHERE (CodProduto = @cod);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@cod" , Produto.CodProduto),
            };
            await ExecuteQuery(sql, parametro);
        }

        public async Task UpdateProduto(Produto Produto)
        {
            string query = "UPDATE produto SET NomeProduto = @NomeProduto  where CodProduto = @CodigoProduto";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@CodigoProduto", Produto.CodProduto),
                    new MySqlParameter ("@NomeProduto", Produto.NomeProduto)
            };

            await ExecuteQuery(query, parametro);

        }
        public async Task<Produto> SelectProduto(Produto Produto)
        {
            string sql = "select * from produto where (nomeProduto=@produto);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@produto" , Produto.NomeProduto)
            };
            return await Select<Produto>(sql, parametro);
        }
        public async Task<Produto> SelectProdutoId(Produto Produto)
        {
            string sql = "select * from produto where (CodProduto=@produto);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@produto" , Produto.CodProduto)
            };
            return await Select<Produto>(sql, parametro);
        }

        public async Task<List<Produto>> SelectAllProduct()
        {
            string sql = "SELECT * FROM produto";

            return await Select<List<Produto>>(sql, null); 
        }

    }
}
