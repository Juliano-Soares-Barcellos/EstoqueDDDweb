using Dapper;
using Domain.Interface.Generica;
using Entities.Entities;
using Infra.Configuracao;
using Microsoft.Win32.SafeHandles;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infra.Generic
{
    public class RepositoryGeneric<t> : IGenerica<t>, IDisposable where t : class
    {
        private readonly DbConexao conexao;

        public RepositoryGeneric()
        {
            conexao = new DbConexao();
        }

        public async Task<int> ExecuteQuery(string query, MySqlParameter[] parameters)
        {
            var conexao = new DbConexao().Conexao();
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    await conexao.OpenAsync();
                    comando.Parameters.AddRange(parameters);
                    int result =Convert.ToInt32(await comando.ExecuteScalarAsync());
                    await conexao.CloseAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public async Task<T> Select<T>(string query, MySqlParameter[] parameters)
        {
            try
            {
                using (var conexao = new DbConexao().Conexao())
                {
                    await conexao.OpenAsync();

                    var dynamicParameters = new DynamicParameters();
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            dynamicParameters.Add(param.ParameterName, param.Value);
                        }

                        // Se o tipo T for uma lista de Produto, use QueryAsync<Produto>
                        if (typeof(T) == typeof(List<Produto>))
                        {
                            var resultado = await conexao.QueryAsync<Produto>(query, dynamicParameters);
                            return (T)(object)resultado.ToList(); // Conversão explícita
                        }

                        // Caso contrário, apenas execute a query genérica
                        var resultadoGenerico = await conexao.QueryAsync<T>(query, dynamicParameters);
                        return resultadoGenerico.FirstOrDefault();
                    }
                    else
                    {
                        // Sem parâmetros
                        if (typeof(T) == typeof(List<Produto>))
                        {
                            var resultadoSemParametros = await conexao.QueryAsync<Produto>(query);
                            return (T)(object)resultadoSemParametros.ToList();
                        }

                        var resultadoGenericoSemParametros = await conexao.QueryAsync<T>(query);
                        return resultadoGenericoSemParametros.FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
                return default(T); // Retorna o valor padrão do tipo T
            }
        }


        private bool _disposedValue;
    private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _safeHandle?.Dispose();
                _safeHandle = null;
            }

            _disposedValue = true;
        }
    }


}
}
