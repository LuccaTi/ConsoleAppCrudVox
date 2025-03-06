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
    internal static class UsuarioService
    {

        //SELECT
        public static bool VerificaSeUsuarioJaExiste(string codUsuario)
        {
            bool usuarioExiste = false;
            try
            {

                usuarioExiste = UsuarioRepository.VerificaSeUsuarioJaExiste(codUsuario);

            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return usuarioExiste;
        }
        public static string FornecerProxCodUsuario()
        {
            string codUsuario = "";

            try
            {
                codUsuario = UsuarioRepository.FornecerProxCodUsuario();
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return codUsuario;
        }
        public static string BuscarCodUsuario(string login)
        {
            string codUsuario = "";

            try
            {
                codUsuario = UsuarioRepository.BuscarCodUsuario(login);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return codUsuario;
        }

        //INSERT
        public static void RegistrarUsuario(RegistroUsuario usuario)
        {

            try
            {
                UsuarioRepository.RegistrarUsuario(usuario);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }


        //UPDATE

        //DELETE
        public static void ExcluirRegistro(int codUsuario)
        {

            try
            {
                UsuarioRepository.ExcluirRegistro(codUsuario);
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
                UsuarioRepository.ExcluirTodosDados();
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }
    }
}

