using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Dtos;
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
    internal class GravacaoService
    {
        //SELECT
        public static int BuscarCodGravacao(string nomeArquivo)
        {
            int codGravacao = 0;
            try
            {
                codGravacao = GravacaoRepository.BuscarCodGravacao(nomeArquivo);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
            return codGravacao;
        }
        public static DateTime BuscarDataInicio(int codGravacao)
        {
            DateTime dataInicio = new();

            try
            {
                dataInicio = GravacaoRepository.BuscarDataInicio(codGravacao);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
            return dataInicio;
        }
        public static bool VerificaGravacaoJaRegistrada(string nomeArquivo)
        {
            bool registrada = false;
            try
            {
                registrada = GravacaoRepository.VerificaGravacaoJaRegistrada(nomeArquivo);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
            return registrada;
        }
        public static bool VerificaGravacaoJaAtualizada(DateTime dataFim)
        {
            bool atualizada = false;
            {
                try
                {
                    atualizada = GravacaoRepository.VerificaGravacaoJaAtualizada(dataFim);
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return atualizada;
        }

        //INSERT
        public static void RegistrarGravacao(Gri griFile, GriDto griDto)
        {

            try
            {
                GravacaoRepository.RegistrarGravacao(griFile, griDto);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
        }

        //UPDATE

        public static void AtualizarGravacao(Grf grfFile, GrfDto grfDto)
        {

            try
            {
                GravacaoRepository.AtualizarGravacao(grfFile, grfDto);

            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

        }

        //DELETE
        public static void ExcluirRegistro(int codGravacao)
        {

            try
            {
                GravacaoRepository.ExcluirRegistro(codGravacao);
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
                GravacaoRepository.ExcluirTodosDados();
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }

        }
    }
}
