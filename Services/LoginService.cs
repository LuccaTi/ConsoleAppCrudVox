using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Models.Entities;
using ConsoleAppCrudVox.Repositories;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Services
{
    internal class LoginService
    {

        //SELECT
        public static bool VerificaSeRamalAtivoOutrosUsuarios(int codRamal, string codUsuarioGri)
        {
            bool ramalAtivo = false;
            try
            {
                ramalAtivo = LoginRepository.VerificaSeRamalAtivoOutrosUsuarios(codRamal, codUsuarioGri);

            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return ramalAtivo;
        }
        public static int BuscarCodLogin(int codRamal)
        {
            int codLogin = 0;

            try
            {
                codLogin = LoginRepository.BuscarCodLogin(codRamal);

            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return codLogin;
        }
        public static bool VerificaSeUsuarioJaLogado(string codUsuario)
        {
            bool usuarioLogado = false;
            try
            {
                usuarioLogado = LoginRepository.VerificaSeUsuarioJaLogado(codUsuario);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
            return usuarioLogado;
        }
        public static List<string> ListarOutrosUsuariosLogados(int codRamal, string codUsuarioGri)
        {
            List<string> codigosUsuarios = new List<string>();

            try
            {
                codigosUsuarios = LoginRepository.ListarOutrosUsuariosLogados(codRamal, codUsuarioGri);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return codigosUsuarios;
        }
        public static int FornecerProxCodLogin()
        {
            int codLogin = 0;

            try
            {
                codLogin = LoginRepository.FornecerProxCodLogin();
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return codLogin;
        }

        //INSERT
        public static void FazerLogin(LoginUsuario usuario)
        {

            try
            {
                LoginRepository.FazerLogin(usuario);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }

        //UPDATE
        public static void FazerLogOut(List<string> codigoUsuarios)
        {

            try
            {
                LoginRepository.FazerLogOut(codigoUsuarios);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }

        //DELETE
        public static void ExcluirRegistro(int codLogin)
        {

            try
            {
                LoginRepository.ExcluirRegistro(codLogin);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }
        public static void ExcluirTodosDados()
        {

            try
            {
                LoginRepository.ExcluirTodosDados();
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }
    }
}

