using ConsoleAppCrudVox.Data;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Repositories
{
    internal static class ServidorRepository
    {

        //SELECT
        public static int BuscarCodServidor(string nomeServidor)
        {
            int codServidor = 0;
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {

                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_SERVIDOR FROM SERVIDOR WHERE NOM_SERVIDOR = {nomeServidor};";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        codServidor = Convert.ToInt32(dr[0]);
                    }

                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error " + fbex);
                }
            }
            return codServidor;
        }

        //INSERT
        //UPDATE
        //DELETE
    }
}
