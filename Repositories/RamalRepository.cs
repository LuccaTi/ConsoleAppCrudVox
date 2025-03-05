using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Models.Entities;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Repositories
{
    internal class RamalRepository
    {

        //SELECT
        public static int BuscarCodRamal(int ramal)
        {
            int codRamal = 0;
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_RAMAL FROM RAMAL WHERE TXT_NUM_RAMAL = '{ramal.ToString()}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        codRamal = Convert.ToInt32(dr[0]);
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codRamal;
        }
        public static bool VerificaSeRamalAtivoOutroCanal(int numCanal, int ramal)
        {

            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT FLG_REG_ATIVO FROM RAMAL WHERE NUM_CANAL != {numCanal} AND  TXT_NUM_RAMAL = {ramal};";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[0] != null && dr[0].Equals('S'))
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
        public static int[] BuscarDadosParaDto(int ramal)
        {
            int[] dados = new int[4];
            using (FbConnection conexaoFirebird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFirebird.Open();
                    string mSQL = $"SELECT  COD_RAMAL, NUM_CANAL, COD_SERVIDOR, COD_OPERACAO FROM RAMAL WHERE TXT_NUM_RAMAL = {ramal} AND RAMAL.FLG_REG_ATIVO = 'S'";
                    FbCommand cmd = new FbCommand(mSQL, conexaoFirebird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        dados[0] = Convert.ToInt32(dr[0]);
                        dados[1] = Convert.ToInt32(dr[1]);
                        dados[2] = Convert.ToInt32(dr[2]);
                        dados[3] = Convert.ToInt32(dr[3]);
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return dados;
        }
        public static bool VerificaSeRamalExiste(int ramal)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT TXT_NUM_RAMAL FROM RAMAL WHERE TXT_NUM_RAMAL = {ramal};";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr[0] != null && Convert.ToInt32(dr[0]).Equals(ramal))
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
        public static int FornecerProxCodRamal()
        {
            int codRamal = 0;
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = "SELECT FIRST 1 COD_RAMAL FROM RAMAL ORDER BY COD_RAMAL DESC;";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        codRamal = Convert.ToInt32(dr[0]) + 1;
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codRamal;
        }

        //INSERT
        public static void RegistrarRamal(Ramal ramal)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"INSERT INTO RAMAL" +
                        $"(" +
                        $"COD_RAMAL, COD_SERVIDOR, COD_OPERACAO, DATE_START, DATE_END, " +
                        $"FLG_RAMAL_ATIVO, FLG_RAMALFIXO, FLG_REG_ATIVO, " +
                        $"FLG_OPERACAOFIXA, FLG_USUARIOFIXO, NUM_CANAL, TXT_NUM_RAMAL" +
                        $")" +
                        $"VALUES" +
                        $"(" +
                        $"{ramal.CodRamal}, {ramal.CodServidor}, {ramal.CodOperacao}, '{ramal.DataInicio.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                        $"{ramal.DataFim?.ToString("yyyy-MM-dd HH:mm:ss")}, '{ramal.FlgRamalAtivo}', '{ramal.FlgRamalFixo}'," +
                        $"'{ramal.FlgRegAtivo}', '{ramal.FlgOperacaoFixa}', '{ramal.FlgUsuarioFixo}', {ramal.NumCanal}," +
                        $"{ramal.TxtNumRamal}" +
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
        public static void ExcluirRegitro(int codRamal)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"DELETE FROM RAMAL WHERE COD_RAMAL = {codRamal}";

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
                    string mSQL = "DELETE FROM RAMAL";

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

