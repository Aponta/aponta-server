using apontaServer.Database;
using apontaServer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public class ApontamentoRepositorio : IApontamentoRepositorio
    {
        private IConexao Conexao;

        public ApontamentoRepositorio(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public void Delete(int id)
        {
            try
            {
                using(var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Execute(@"DELETE FROM T_APONTAMENTO WHERE ID = @id", new { id });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Apontamento Get(int IdUsuario, int IdTarefa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Apontamento>(@"SELECT AP.* FROM T_APONTAMENTO AP
                            INNER JOIN T_LOGIN LO ON AP.ID_USUARIO = LO.ID INNER JOIN T_TAREFA TA ON 
                            AP.ID_TAREFA = TA.ID WHERE AP.ID_USUARIO = @idUsuario AND AP.ID_TAREFA = @idTarefa", 
                            new { IdUsuario, IdTarefa });

                    return resposta.LastOrDefault<Apontamento>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Apontamento Get(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Apontamento>(@"SELECT AP.* FROM T_APONTAMENTO AP
                            INNER JOIN T_LOGIN LO ON AP.ID_USUARIO = LO.ID INNER JOIN T_TAREFA TA ON 
                            AP.ID_TAREFA = TA.ID WHERE AP.ID = @id", new { id });

                    return resposta.LastOrDefault<Apontamento>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Apontamento Get(Login usuario)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Apontamento>(@"SELECT AP.* FROM T_APONTAMENTO AP
                            INNER JOIN T_LOGIN LO ON AP.ID_USUARIO = LO.ID INNER JOIN T_TAREFA TA ON 
                            AP.ID_TAREFA = TA.ID WHERE AP.ID_USUARIO = @ID_USUARIO ORDER BY AP.ID DESC LIMIT 1",
                            new { usuario.ID_USUARIO });

                    return resposta.FirstOrDefault<Apontamento>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Apontamento> List(Login usuario)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Apontamento>(@"SELECT AP.* FROM T_APONTAMENTO AP
                            INNER JOIN T_LOGIN LO ON AP.ID_USUARIO = LO.ID INNER JOIN T_TAREFA TA ON 
                            AP.ID_TAREFA = TA.ID WHERE AP.ID_USUARIO = @ID_USUARIO",
                            new { usuario.ID_USUARIO });

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Apontamento> List(Tarefa tarefa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Apontamento>(@"SELECT AP.* FROM T_APONTAMENTO AP
                            INNER JOIN T_LOGIN LO ON AP.ID_TAREFA = LO.ID INNER JOIN T_TAREFA TA ON 
                            AP.ID_TAREFA = TA.ID WHERE AP.ID_TAREFA = @ID_TAREFA",
                            new { tarefa.ID_TAREFA });

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Apontamento> List(Login usuario, int quantidadeRegistros, int offset)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Apontamento>(@"SELECT * FROM T_APONTAMENTO WHERE ID_USUARIO = @ID_USUARIO 
                            AND DATA_HORA_FINAL IS NOT NULL ORDER BY ID DESC LIMIT @quantidadeRegistros OFFSET @offset",
                            new 
                            {
                                usuario.ID_USUARIO,
                                quantidadeRegistros,
                                offset
                            });
                    return resposta.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Apontamento Post(Apontamento apontamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_APONTAMENTO (DATA_HORA_INICIAL, DESCRICAO, ID_TAREFA, ID_USUARIO)
                                VALUES(@DATA_HORA_INICIAL, @DESCRICAO, @ID_TAREFA, @ID_USUARIO)",
                                new
                                {
                                    apontamento.DATA_HORA_INICIAL,
                                    apontamento.DESCRICAO,
                                    apontamento.TAREFA.ID_TAREFA,
                                    apontamento.USUARIO.ID_USUARIO
                                });

                    return List(apontamento.USUARIO).LastOrDefault<Apontamento>();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Put(int id, DateTime dataHoraFinal)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_APONTAMENTO SET DATA_HORA_FINAL = @dataHoraFinal WHERE id = @id", new { id, dataHoraFinal });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Put(int id, int idTarefa)
        {
            try
            {
                using(var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_APONTAMENTO SET ID_TAREFA = @idTarefa WHERE id = @id", new { id, idTarefa });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
