using Dapper;
using Domain.Interface;
using Entities.Entities;
using Infra.Configuracao;
using Infra.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class MovimentoEstoqueRepository : RepositoryGeneric<MovimentoEstoque>, IEstoque
    {
        private MySqlParameter[] parametro = null;
        private readonly DbConexao con;
        public MovimentoEstoqueRepository()
        {
            con = new DbConexao();
        }

        public async Task<List<MovimentoEstoque>> ListarProdutosEstoque()
        {
            try
            {
                using (var connection = con.Conexao())
                {
                    await connection.OpenAsync();

                    var query = @"
                SELECT 
                    movimentacaoestoque.CodMovimentacao, movimentacaoestoque.DataMovimentacao, movimentacaoestoque.ValorUnitario, movimentacaoestoque.Quantidade,
                    fornecedor.CodFornecedor, fornecedor.NomeFornecedor,
                    produto.CodProduto, produto.NomeProduto, produto.Estoque,
                    tipomovimentacao.CodMov, tipomovimentacao.Descricao
                FROM movimentacaoestoque
                INNER JOIN fornecedor ON movimentacaoestoque.FkFornecedor = fornecedor.CodFornecedor
                INNER JOIN produto ON movimentacaoestoque.FkProduto = produto.CodProduto
                INNER JOIN tipomovimentacao ON movimentacaoestoque.FkTipoMov = tipomovimentacao.CodMov
                WHERE movimentacaoestoque.CodMovimentacao IN (
                    SELECT MAX(m.CodMovimentacao)
                    FROM movimentacaoestoque m
                    WHERE m.FkTipoMov = 1
                    GROUP BY m.FkProduto
                )
                ORDER BY produto.NomeProduto;";

                    var resultados = await connection.QueryAsync<MovimentoEstoque, Fornecedor, Produto, tipomovimentacao, MovimentoEstoque>
                    (
                        query,
                        (movimentoEstoque, fornecedor, produto, TipoMovimentoEstoque) =>
                        {
                            movimentoEstoque.Fornecedor = fornecedor; // Mapeia o fornecedor
                    movimentoEstoque.Produto = produto; // Mapeia o produto
                    movimentoEstoque.tipomovimentacao = TipoMovimentoEstoque; // Mapeia o tipo de movimentação
                    return movimentoEstoque;
                        },
                        splitOn: "CodFornecedor,CodProduto,CodMov" // Define os pontos de separação
                    );

                    await connection.CloseAsync();
                    return resultados.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar produtos comprados: {ex.Message}");
                return new List<MovimentoEstoque>();
            }
            finally
            {
                await con.Conexao().CloseAsync();
            }
        }

        public async Task<List<MovimentoEstoque>> ListarProdutosEntradasOuSaidas(string TipoMovimento)
        {
            try
            {
                using (var connection = con.Conexao())
                {
                    await connection.OpenAsync();

                    var resultados = await connection.QueryAsync<MovimentoEstoque, Fornecedor, Produto, tipomovimentacao, MovimentoEstoque>
                    (
                        @"select * from movimentacaoestoque 
                  left join fornecedor on movimentacaoestoque.FkFornecedor = fornecedor.CodFornecedor 
                  inner join produto on movimentacaoestoque.FkProduto = produto.CodProduto 
                  inner join tipomovimentacao on movimentacaoestoque.FkTipoMov = tipomovimentacao.CodMov 
                  where tipomovimentacao.Descricao = @TipoMovi",
                         (movimentoEstoque, Fornecedor, produto, tipomovimentacao) =>
                         {
                             movimentoEstoque.FkFornecedor = Fornecedor.CodFornecedor;
                             movimentoEstoque.FkProduto = produto.CodProduto;
                             movimentoEstoque.Fk_tipomovimentacao = tipomovimentacao.CodMov;
                             movimentoEstoque.Fornecedor = Fornecedor;
                             movimentoEstoque.Produto = produto;
                             movimentoEstoque.tipomovimentacao = tipomovimentacao;
                             return movimentoEstoque;
                         },
                        splitOn: "FkFornecedor,FkProduto,FkTipoMov"
                    );
                    await connection.CloseAsync();
                    return resultados.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar produtos comprados: {ex.Message}");
                return new List<MovimentoEstoque>();
            }
            finally
            {
                await con.Conexao().CloseAsync();
            }
        }

        public async Task<List<MovimentoEstoque>> ListarProdutosTotalHistorico()
        {
            try
            {
                using (var connection = con.Conexao())
                {
                    await connection.OpenAsync();

                    var resultados = await connection.QueryAsync<MovimentoEstoque, Fornecedor, Produto, tipomovimentacao, MovimentoEstoque>
                    (
                            @"select * from movimentacaoestoque 
                      left join fornecedor on movimentacaoestoque.FkFornecedor = fornecedor.CodFornecedor 
                      inner join produto on movimentacaoestoque.FkProduto = produto.CodProduto 
                      inner join tipomovimentacao on movimentacaoestoque.FkTipoMov = tipomovimentacao.CodMov",

                        (movimentoEstoque, Fornecedor, produto, tipomovimentacao) =>
                        {
                            movimentoEstoque.FkFornecedor = Fornecedor.CodFornecedor;
                            movimentoEstoque.FkProduto = produto.CodProduto;
                            movimentoEstoque.Fk_tipomovimentacao = tipomovimentacao.CodMov;
                            movimentoEstoque.Fornecedor = Fornecedor;
                            movimentoEstoque.Produto = produto;
                            movimentoEstoque.tipomovimentacao = tipomovimentacao;
                            return movimentoEstoque;
                        },
                        splitOn: "CodFornecedor,CodProduto, CodMov"
                    );
                    connection.Close();
                    return resultados.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar produtos comprados: {ex.Message}");
                return new List<MovimentoEstoque>();
            }
            finally
            {
                await con.Conexao().CloseAsync();
            }
        }
        public async Task<List<MovimentoEstoque>> ListarEstoque_DatasEntradas_saida(int tipo, DateTime Datainit, DateTime fim)
        {
            try
            {
                using (var connection = con.Conexao())
                {
                    await connection.OpenAsync();

                    var resultados = await connection.QueryAsync<MovimentoEstoque, Fornecedor, Produto, tipomovimentacao, MovimentoEstoque>
                    (

                        @"select * from movimentacaoestoque 
                  left join fornecedor on movimentacaoestoque.FkFornecedor = fornecedor.CodFornecedor 
                  inner join produto on movimentacaoestoque.FkProduto = produto.CodProduto 
                  inner join tipomovimentacao on movimentacaoestoque.FkTipoMov = tipomovimentacao.CodMov 
                  where DataMovimentacao between @dataInit and @datafim and movimentacaoestoque.FkTipoMov=@tipoMovi",
                        (movimentoEstoque, fornecedor, produto, TipoMovimentoEstoque) =>
                        {
                            movimentoEstoque.FkFornecedor = fornecedor.CodFornecedor;
                            movimentoEstoque.FkProduto = produto.CodProduto;
                            movimentoEstoque.Fk_tipomovimentacao = TipoMovimentoEstoque.CodMov;
                            movimentoEstoque.Fornecedor = fornecedor;
                            movimentoEstoque.Produto = produto;
                            movimentoEstoque.tipomovimentacao = TipoMovimentoEstoque;
                            return movimentoEstoque;
                        },
                        new { TipoMovi = tipo, dataInit = Datainit, datafim = fim },
                          splitOn: "CodFornecedor,CodProduto, CodMov"
                    );
                    await connection.CloseAsync();
                    return resultados.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar produtos comprados: {ex.Message}");
                return new List<MovimentoEstoque>();
            }
            finally
            {
                await con.Conexao().CloseAsync();
            }
        }

        public async Task<string> AddMovimentoEstoque(MovimentoEstoque Estoque)
        {
            string sql = "INSERT INTO movimentacaoestoque (FkProduto, FkTipoMov,FkFornecedor,DataMovimentacao,Quantidade,ValorUnitario) VALUES (@fkProduto, @fkTipoMov, @fkFornecedor,@DataMovimento,@quantidade, @ValorUnitario);";
            parametro = new MySqlParameter[]
           {
                    new MySqlParameter ("@fkProduto" , Estoque.FkProduto),
                    new MySqlParameter ("@fkTipoMov" , Estoque.Fk_tipomovimentacao),
                    new MySqlParameter ("@fkFornecedor" , Estoque.FkFornecedor),
                    new MySqlParameter ("@DataMovimento" , Estoque.DataMovimentacao),
                    new MySqlParameter ("@quantidade" , Estoque.Quantidade),
                    new MySqlParameter ("@ValorUnitario" , Estoque.ValorUnitario ),
           };

            return await ExecuteQuery(sql, parametro) < 0 ? "Não inserido" : "Inserido com Sucesso";
        }

        public async Task<string> DeleteMovimentoEstoque(MovimentoEstoque Estoque)
        {
            string sql = "DELETE FROM movimentacaoestoque WHERE (CodMovimentacao = @cod);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@cod" , Estoque.CodMovimentacao),
            };
            return await ExecuteQuery(sql, parametro) < 0 ? "Erro ao Deletar" : "Sucesso ao Deletar";
        }

        public async Task<string> UpdateMovimentoEstoqueQuantidade(MovimentoEstoque Estoque)
        {
            string sql = "UPDATE movimentacaoestoque SET Quantidade = @quantidade WHERE(CodMovimentacao = @CodMov);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@quantidade " , Estoque.Quantidade),
                    new MySqlParameter ("@CodMov " , Estoque.CodMovimentacao)
            };
            return await ExecuteQuery(sql, parametro) < 0 ? "Erro ao atualizar" : "Atualizado com Sucesso";
        }

        public async Task<string> UpdateMovimentoEstoqueValor(MovimentoEstoque Estoque)
        {
            string sql = "UPDATE movimentacaoestoque SET ValorUnitario = @quantidade WHERE(CodMovimentacao = @CodMov);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@ValorUnitario" , Estoque.Quantidade),
                    new MySqlParameter ("@CodMov " , Estoque.CodMovimentacao)
            };
            return await ExecuteQuery(sql, parametro) < 0 ? "Erro ao atualizar" : "Atualizado com Sucesso";
        }

        public async Task<MovimentoEstoque> SelectData(MovimentoEstoque Estoque)
        {
            string sql = "select * from movimentacaoestoque where (DataMovimentacao = @data and FkProduto = @fkProduto );";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@data" , Estoque.DataMovimentacao),
                    new MySqlParameter ("@fkProduto " , Estoque.FkProduto)
            };
            return await Select<MovimentoEstoque>(sql, parametro);
        }
        public async Task<MovimentoEstoque> Select(MovimentoEstoque Estoque)
        {
            string sql = "select * from movimentacaoestoque where (CodMovimentacao = @CodMov);";
            parametro = new MySqlParameter[]
            {
                    new MySqlParameter ("@CodMov " , Estoque.CodMovimentacao)
            };
            return await Select<MovimentoEstoque>(sql, parametro);
        }
    }
}
