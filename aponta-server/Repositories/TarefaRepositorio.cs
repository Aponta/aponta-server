using apontaServer.Database;
using apontaServer.Exceptions;
using apontaServer.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace apontaServer.Repositories
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        public IConexao Conexao;

        public TarefaRepositorio(IConexao Conexao)
        {
            this.Conexao = Conexao;
        }

        public Tarefa Get(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Tarefa>(@"SELECT *, ID AS ID_TAREFA FROM T_TAREFA WHERE ID = @id", new { id });

                    if (resposta.Count() == 0)
                    {
                        resposta = cn.Query<Tarefa>(@"SELECT *, ID AS ID_TAREFA FROM T_TAREFA WHERE ID_TAREFA_CHAMADO = @id", new { id });
                    }

                    return resposta.FirstOrDefault();
                }
            }
            catch (MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }

        public Tarefa GetTarefa(int idTarefa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Tarefa>(@"SELECT *, ID AS ID_TAREFA FROM T_TAREFA WHERE ID_TAREFA = @idTarefa", new { idTarefa });

                    return resposta.FirstOrDefault();
                }
            }
            catch (MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }

        public List<Tarefa> GetTarefa(int idUsuario, string termo)
        {
            try
            {
                using(var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Tarefa>(@"
                        SELECT *, ID AS ID_TAREFA FROM T_TAREFA WHERE ID_TAREFA_CHAMADO LIKE @termo AND ID_USUARIO = @idUsuario
                        UNION
                        SELECT *, ID AS ID_TAREFA FROM T_TAREFA WHERE CLIENTE_TAREFA LIKE @termo AND ID_USUARIO = @idUsuario", 
                        new { termo = termo + "%", idUsuario });

                    return resposta.ToList();
                }
            }
            catch(MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }

        public List<Tarefa> List(int idTarefa, int idUsuario)
        {
            try
            {
                using(var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Tarefa>(@"
                        SELECT *, ID AS ID_TAREFA FROM T_TAREFA 
                        WHERE ID_USUARIO = @idUsuario AND 
                        ID_TAREFA_CHAMADO LIKE @idTarefa", 
                        new { idTarefa = idTarefa + "%", idUsuario });

                    return resposta.ToList();
                }
            }
            catch(MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }

        public Tarefa Post(Tarefa tarefa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_TAREFA (ID_TAREFA_CHAMADO, CLIENTE_TAREFA, ID_USUARIO) 
                                VALUES (@ID_TAREFA_CHAMADO, @CLIENTE_TAREFA, @ID_USUARIO)", new
                    {
                        tarefa.ID_TAREFA_CHAMADO,
                        tarefa.CLIENTE_TAREFA,
                        tarefa.LOGIN.ID_USUARIO
                    });
                }

                return Get(tarefa.ID_TAREFA_CHAMADO);
            }
            catch (MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }
    }
}
