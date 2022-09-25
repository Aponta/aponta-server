using apontaServer.Enum;
using apontaServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace apontaServer.Services
{
    public class MetodosGenericosService
    {
        private IMetodosGenericosRepositorio repositorio;
   
        public MetodosGenericosService(IMetodosGenericosRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public string Dlookup(string campoBuscado, string tabela, string where)
        {
            try
            {
                return repositorio.Dlookup(campoBuscado, tabela, where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum metodosGenericos)
        {
            try
            {
                repositorio.StartTransactionCommitRollback(metodosGenericos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DateTime DataHoraBrasilia()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            else
                return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
        }

        public string FormataLista<T>(IEnumerable<T> lista)
        {
            string retorno = "";
            foreach(T item in lista)
            {
               retorno += $"{item.ToString()}, ";
            }

            return retorno.Remove(retorno.Length - 2);

        }
    }
}
