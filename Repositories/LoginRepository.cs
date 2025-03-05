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
    internal class LoginRepository
    {

        //SELECT
        public static bool VerificaSeRamalAtivoOutrosUsuarios(int codRamal, string codUsuarioGri)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COUNT(COD_RAMAL) FROM LOGIN WHERE COD_RAMAL = {codRamal} " +
                        $"AND COD_USUARIO != '{codUsuarioGri}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        if (Convert.ToInt32(dr[0]) >= 1)
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
        public static int BuscarCodLogin(int codRamal)
        {
            int codLogin = 0;
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_LOGIN FROM LOGIN WHERE COD_RAMAL ={codRamal};";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        codLogin = Convert.ToInt32(dr[0]);
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codLogin;
        }
        public static bool VerificaSeUsuarioJaLogado(string codUsuario)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_LOGIN FROM LOGIN WHERE COD_USUARIO = '{codUsuario}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr[0].ToString() != "" || dr[0] != null)
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
        public static List<string> ListarOutrosUsuariosLogados(int codRamal, string codUsuarioGri)
        {
            List<string> codigosUsuarios = new List<string>();
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_USUARIO FROM LOGIN WHERE COD_RAMAL = {codRamal} AND COD_USUARIO" +
                        $"!= '{codUsuarioGri}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        codigosUsuarios.Add(dr[0].ToString() ?? "");
                    }
                    return codigosUsuarios;
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codigosUsuarios;
        }
        public static int FornecerProxCodLogin()
        {
            int codLogin = 0;
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT FIRST 1 COD_LOGIN FROM LOGIN ORDER BY COD_LOGIN DESC;";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        codLogin = Convert.ToInt32(dr[0]) + 1;
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codLogin;
        }

        //INSERT
        public static void RegistrarLogin(LoginUsuario usuario)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"INSERT INTO LOGIN " +
                        $"(" +
                        $"COD_LOGIN, COD_RAMAL, COD_USUARIO, DAT_LOGIN, FLG_LOGOUT_NORM" +
                        $")" +
                        $"VALUES" +
                    $"(" +
                    $"{usuario.CodLogin}, {usuario.CodRamal}, '{usuario.CodUsuario}', '{usuario.DatLogin.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                    $"'{usuario.FlgLogoutNorm}'" +
                    $")";
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
        public static void FazerLogOut(List<string> codigoUsuarios)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    foreach (string codUsuario in codigoUsuarios)
                    {
                        if (!string.IsNullOrWhiteSpace(codUsuario))
                        {

                            string mSQL = $"UPDATE LOGIN SET DAT_LOGOUT = '{DateTime.Now}' " +
                                $"WHERE COD_USUARIO = '{codUsuario}'";
                            FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
        }

        //DELETE
        public static void ExcluirRegistro(int codLogin)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"DELETE FROM LOGIN WHERE COD_LOGIN = {codLogin}";

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
                    string mSQL = "DELETE FROM LOGIN";

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
