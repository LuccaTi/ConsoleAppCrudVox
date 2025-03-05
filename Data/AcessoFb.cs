using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography;
using FirebirdSql.Data.FirebirdClient;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Models.Entities;
using System.Diagnostics.Metrics;

namespace ConsoleAppCrudVox.Data
{
    /// <summary>
    /// Usa padrão Singleton para obter uma instancia do Firebird
    /// </summary>
    internal class AcessoFb
    {
        private static readonly AcessoFb instanciaFireBird = new AcessoFb();
        private AcessoFb()
        {

        }

        public static AcessoFb GetInstancia()
        {
            return instanciaFireBird;
        }

        public FbConnection GetConexao()
        {
            string connection = ConfigurationManager.ConnectionStrings["FireBirdConnectionString"].ToString();
            return new FbConnection(connection);
        }
    }
}
