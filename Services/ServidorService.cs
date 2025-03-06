using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Repositories;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Services
{
    internal static class ServidorService
    {
        //SELECT
        public static int BuscarCodServidor(string nomeServidor)
        {
            int codServidor = 0;

            try
            {

                codServidor = ServidorRepository.BuscarCodServidor(nomeServidor);

            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error " + fbex);
            }

            return codServidor;
        }

        //INSERT
        //UPDATE
        //DELETE
    }
}
