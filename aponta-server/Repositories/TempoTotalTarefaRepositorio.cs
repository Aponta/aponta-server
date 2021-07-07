using apontaServer.Repositories;
using apontaServer.Database;
using apontaServer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public class TempoTotalTarefaRepositorio : ITempoTotalTarefaRepositorio
    {
        private IConexao Conexao;

        public TempoTotalTarefaRepositorio(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public TempoTotalTarefa Get(int idUsuario, int idTarefa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TempoTotalTarefa>(@"SELECT TT.* FROM T_TEMPO_TOTAL_TAREFA TT INNER JOIN 
                                            T_LOGIN LO ON TT.ID_USUARIO = LO.ID INNER JOIN T_TAREFA TA ON TT.ID_TAREFA = TA.ID
                                            WHERE TT.ID_USUARIO = @idUsuario AND TT.ID_TAREFA = @idTarefa", new { idUsuario, idTarefa });

                    return resposta.FirstOrDefault<TempoTotalTarefa>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Post(TempoTotalTarefa tempoTotalTarefa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_TEMPO_TOTAL_TAREFA (TEMPO_TOTAL, ID_TAREFA, ID_USUARIO) 
                                VALUES(@TEMPO_TOTAL, @ID_TAREFA, @ID_USUARIO)", new
                    {
                                tempoTotalTarefa.TEMPO_TOTAL,
                                tempoTotalTarefa.TAREFA.ID_TAREFA,
                                tempoTotalTarefa.USUARIO.ID_USUARIO
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Put(int id, long tempo)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_TEMPO_TOTAL_TAREFA SET TEMPO_TOTAL = TEMPO_TOTAL + @tempo WHERE ID = @id", new { id, tempo });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
