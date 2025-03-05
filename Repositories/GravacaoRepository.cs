using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Models.Entities;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Repositories
{
    internal class GravacaoRepository
    {

        //SELECT
        public static int BuscarCodGravacao(string nomeArquivo)
        {
            int codGravacao = 0;
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COD_GRAVACAO FROM GRAVACAO WHERE NOM_ARQUIVO = '{nomeArquivo}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();


                    while (dr.Read())
                    {
                        codGravacao = Convert.ToInt32(dr[0]);
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return codGravacao;
        }
        public static DateTime BuscarDataInicio(int codGravacao)
        {
            DateTime dataInicio = new();
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT DAT_INI_GRAV FROM GRAVACAO WHERE COD_GRAVACAO = {codGravacao};";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        dataInicio = DateTime.Parse(dr[0].ToString() ?? "");
                    }
                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
            return dataInicio;
        }
        public static bool VerificaGravacaoJaRegistrada(string nomeArquivo)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {

                    conexaoFireBird.Open();
                    string mSQL = $"SELECT COUNT(*) FROM GRAVACAO WHERE NOM_ARQUIVO = '{nomeArquivo}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();
                    int count = 0;
                    while (dr.Read())
                    {
                        count = Convert.ToInt32(dr[0]);
                    }

                    if (count == 1)
                    {
                        return true;
                    }

                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }

                return false;
            }
        }

        //INSERT
        public static void RegistrarGravacao(Gri griFile, GriDto griDto)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"INSERT INTO GRAVACAO " +
                        $"(" +
                        $"COD_DIRECAO, COD_LOGIN, COD_SERVIDOR, COD_OPERACAO, DAT_INI_GRAV, " +
                        $"FLG_EXC_BLOQ, FLG_HEADER, FLG_MARCADA, NOM_ARQUIVO, NUM_BYTES, NUM_CODEC, " +
                        $"NUM_SEGUNDOS, NUM_STAT_DISCO, TXT_CAMINHO_ARQ" +
                        $") " +
                        $"" +
                        $"VALUES" +
                        $"(" +
                        $"{griDto.CodDirecao}, {griDto.CodLogin}, {griDto.CodServidor}, " +
                        $"{griDto.CodOperacao},'{griFile.DataInicio.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                        $"'{griDto.FlgExcBloq}', {griFile.Header}, '{griDto.FlgMarcada}', " +
                        $"'{griFile.NomeArquivo}', {griDto.NumBytes}, {griFile.Codec}, {griDto.NumSegundos}, " +
                        $"{griDto.NumStatDisco},'{griFile.CaminhoArquivo}'" +
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
        public static bool VerificaGravacaoJaAtualizada(DateTime dataFim)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"SELECT DAT_FIM_GRAV FROM GRAVACAO WHERE DAT_FIM_GRAV = '{dataFim.ToString("yyyy-MM-dd HH:mm:ss.fff")}';";

                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[0] != null)
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
        public static void AtualizarGravacao(Grf grfFile, GrfDto grfDto)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"UPDATE GRAVACAO SET DAT_FIM_GRAV = '{grfFile.DataFinal.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                        $"NUM_BYTES = {grfDto.NumBytes},NUM_SEGUNDOS = {grfDto.NumSegundos} WHERE COD_GRAVACAO = {grfDto.CodGravacao}";


                    FbCommand cmd = new FbCommand(mSQL, conexaoFireBird);
                    cmd.ExecuteNonQuery();

                }
                catch (FbException fbex)
                {
                    Console.WriteLine("Error: " + fbex);
                }
            }
        }
        
        //DELETE
        public static void ExcluirRegistro(int codGravacao)
        {
            using (FbConnection conexaoFireBird = AcessoFb.GetInstancia().GetConexao())
            {
                try
                {
                    conexaoFireBird.Open();
                    string mSQL = $"DELETE FROM GRAVACAO WHERE COD_GRAVACAO = {codGravacao}";

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
                    string mSQL = "DELETE FROM GRAVACAO";

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
