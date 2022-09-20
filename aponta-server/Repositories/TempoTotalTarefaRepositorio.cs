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

        public TempoTotalTarefa Get(int idTarefa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TempoTotalTarefa>(@"SELECT TT.* FROM T_TEMPO_TOTAL_TAREFA TT 
                                            WHERE TT.ID_TAREFA = @idTarefa", new { idTarefa });

                    return resposta.FirstOrDefault();
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
                    cn.Execute(@"INSERT INTO T_TEMPO_TOTAL_TAREFA (TEMPO_TOTAL, ID_TAREFA) 
                                VALUES(@TEMPO_TOTAL, @ID_TAREFA)", new
                    {
                                tempoTotalTarefa.TEMPO_TOTAL,
                                tempoTotalTarefa.TAREFA.ID_TAREFA,
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
                    cn.Execute(@"UPDATE T_TEMPO_TOTAL_TAREFA 
                        SET TEMPO_TOTAL = TEMPO_TOTAL + @tempo WHERE ID = @id", new { id, tempo });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
