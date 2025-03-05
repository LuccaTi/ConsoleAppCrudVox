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
    internal class RamalService
    {
        //SELECT
        public static int BuscarCodRamal(int ramal)
        {
            int codRamal = 0;

            try
            {
                codRamal = RamalRepository.BuscarCodRamal(ramal);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return codRamal;
        }
        public static bool VerificaSeRamalAtivoOutroCanal(int numCanal, int ramal)
        {

            bool ramalAtivo = false;
            try
            {
                ramalAtivo = RamalRepository.VerificaSeRamalAtivoOutroCanal(numCanal, ramal);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return ramalAtivo;
        }
        public static int[] BuscarDadosParaDto(int ramal)
        {
            int[] dados = new int[4];

            try
            {
                dados = RamalRepository.BuscarDadosParaDto(ramal);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return dados;
        }
        public static bool VerificaSeRamalExiste(int ramal)
        {
            bool ramalExiste = false;
            try
            {
                ramalExiste = RamalRepository.VerificaSeRamalExiste(ramal);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return ramalExiste;
        }
        public static int FornecerProxCodRamal()
        {
            int codRamal = 0;

            try
            {
                codRamal = RamalRepository.FornecerProxCodRamal();
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

            return codRamal;
        }

        //INSERT
        public static void RegistrarRamal(Ramal ramal)
        {

            try
            {
                RamalRepository.RegistrarRamal(ramal);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }


        //UPDATE

        //DELETE
        public static void ExcluirRegitro(int codRamal)
        {

            try
            {
                RamalRepository.ExcluirRegitro(codRamal);
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
                RamalRepository.ExcluirTodosDados();
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }
    }
}

