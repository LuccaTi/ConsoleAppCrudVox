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
    internal static class GravacaoService
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
        public static int BuscarCodGravacao(int codLogin)
        {
            int codGravacao = 0;
            try
            {
                codGravacao = GravacaoRepository.BuscarCodGravacao(codLogin);
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
        public static DateTime BuscarDataFinal(int codGravacao)
        {
            DateTime dataFinal = new();
            try
            {
                dataFinal = GravacaoRepository.BuscarDataFinal(codGravacao);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
            return dataFinal;
        }
        public static bool VerificaSeGravacaoJaRegistrada(string nomeArquivo)
        {
            bool registrada = false;
            try
            {
                registrada = GravacaoRepository.VerificaSeGravacaoJaRegistrada(nomeArquivo);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
            }
            return registrada;
        }
        public static bool VerificaSeGravacaoJaAtualizada(DateTime dataFim)
        {
            bool atualizada = false;
            {
                try
                {
                    atualizada = GravacaoRepository.VerificaSeGravacaoJaAtualizada(dataFim);
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return atualizada;
        }
        public static bool VerificaSeGravacaoJaAtualizada(string txtIdChamada, int codDirecao)
        {
            bool atualizada = false;
            try
            {
                atualizada = GravacaoRepository.VerificaSeGravacaoJaAtualizada(txtIdChamada, codDirecao);
            }
            catch (FbException fbex)
            {
                Console.WriteLine("Error: " + fbex);
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
        public static void AtualizarGravacao(int codGravacao, string txtIdChamada, int codDirecao)
        {
            try
            {
                GravacaoRepository.AtualizarGravacao(codGravacao, txtIdChamada, codDirecao);
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
