using apontaServer.Database;
using apontaServer.Enum;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public class MetodosGenericosRepositorio : IMetodosGenericosRepositorio
    {
        private IConexao Conexao;

        public MetodosGenericosRepositorio(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public string Dlookup(string campoBuscado, string tabela, string where)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    IEnumerable<string> resposta;

                    if (where == "")
                    {
                        resposta = cn.Query<string>("SELECT " + campoBuscado + " FROM " + tabela + " ORDER BY " + campoBuscado + " DESC LIMIT 1");
                    }
                    else
                    {
                        resposta = cn.Query<string>("SELECT " + campoBuscado + " FROM " + tabela + " WHERE " + where + " LIMIT 1");
                    }

                    return resposta.FirstOrDefault<string>();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StartTransactionCommitRollback(MetodosGenericosEnum metodosGenericos)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    switch (metodosGenericos)
                    {
                        case MetodosGenericosEnum.START:
                            cn.Execute("START TRANSACTION");
                            break;
                        case MetodosGenericosEnum.COMMIT:
                            cn.Execute("COMMIT");
                            break;
                        case MetodosGenericosEnum.ROLLBACK:
                            cn.Execute("ROLLBACK");
                            break;
                        default:
                            cn.Execute("ROLLBACK");
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
