using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Models.Entities;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Repositories
{
    internal static class UsuarioRepository
    {

        //SELECT
        public static bool VerificaSeUsuarioJaExiste(string codUsuario)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {

                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_USUARIO FROM USUARIO WHERE COD_USUARIO = '{codUsuario}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr[0] != null && dr[0].Equals(codUsuario))
                        {
                            return true;
                        }
                    }

                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return false;
        }
        public static string FornecerProxCodUsuario()
        {
            int codUsuario = 0;
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COUNT(COD_USUARIO) FROM USUARIO;";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        codUsuario = Convert.ToInt32(dr[0]) + 1;
                    }

                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codUsuario.ToString();
        }
        public static string BuscarCodUsuario(string login)
        {
            string codUsuario = "";
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_USUARIO FROM USUARIO WHERE LOGIN = '{login.ToUpper()}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        codUsuario = dr[0].ToString() ?? "";
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codUsuario;
        }

        //INSERT
        public static void RegistrarUsuario(RegistroUsuario usuario)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"INSERT INTO USUARIO " +
                        $"(" +
                        $"COD_USUARIO, COD_CARGO, LANGUAGE, LOGIN, FLG_ATIVO, NOM_CURTO, NOM_USUARIO, SENHA" +
                        $")" +
                        $"VALUES" +
                        $"(" +
                        $"'{usuario.CodUsuario}', {usuario.CodCargo}, '{usuario.FlgAtivo}', '{usuario.Language}'," +
                        $"'{usuario.Login}', '{usuario.NomCurto}', '{usuario.NomUsuario}', '{usuario.Senha}'" +
                        $")" +
                        $"";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    cmd.ExecuteNonQuery();
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
        }
        
        //UPDATE
        
        //DELETE
        public static void ExcluirRegistro(int codUsuario)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"DELETE FROM USUARIO WHERE COD_USUARIO = {codUsuario}";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    cmd.ExecuteNonQuery();
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
        }
        public static void ExcluirTodosDados()
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = "DELETE FROM USUARIO";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    cmd.ExecuteNonQuery();
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
        }
    }

}

