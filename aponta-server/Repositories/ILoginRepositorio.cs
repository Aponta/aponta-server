using apontaServer.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public interface ILoginRepositorio
    {
        Login Get(int id);

        Login Get(string usuario, string senha);

        Login Get(string usuario);

        Login Post(Login login);
    }
}
