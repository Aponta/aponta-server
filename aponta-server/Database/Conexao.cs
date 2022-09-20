using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Database
{
    public class Conexao : IConexao
    {
        private string connectionString;

        public Conexao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection AbrirConexao()
        {
            try
            {
                var cn = new MySqlConnection(connectionString);
                return cn;
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
