using Infra.Configuracao.StringConnection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Configuracao
{
    public class DbConexao
    {
       private readonly ConnectionStringDb stringConexao;
        public DbConexao ()
        {
            stringConexao = new ConnectionStringDb();
        }

        public MySqlConnection Conexao()

        {
            return new MySqlConnection(stringConexao.conexao);
        }

       
    }
}
