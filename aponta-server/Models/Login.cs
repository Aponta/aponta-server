using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Models
{
    public class Login
    {
        private int _id;
        private string _usuario;
        private string _senha;

        public Login()
        {
        }

            public Login(int id, string usuario, string senha)
        {
            ID_USUARIO = id;
            USUARIO = usuario;
            SENHA = senha;
        }

        public int ID_USUARIO { get => _id; set => _id = value; }
        public string USUARIO { get => _usuario; set => _usuario = value; }
        public string SENHA { set => _senha = value; }

        public string retornarSenha()
        {
            return _senha;
        }
    }
}
