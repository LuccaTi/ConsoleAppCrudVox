using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Models.Entities;
using ConsoleAppCrudVox.Repositories;

namespace ConsoleAppCrudVox.Services
{
    internal class GrfService
    {
        public GrfService()
        {

        }
        public static Grf CriarGrfDoArquivo(string caminhoArquivo)
        {
            Grf grfFile = new Grf();
            using (StreamReader sr = File.OpenText(caminhoArquivo))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine() ?? "";
                    if (line != "")
                    {
                        string info = line.Substring(0, 2);
                        switch (info)
                        {
                            case "SR":
                                grfFile.Servidor = line.Substring(3);
                                break;
                            case "CH":
                                grfFile.Canal = int.Parse(line.Substring(3));
                                break;
                            case "CA":
                                grfFile.CaminhoArquivo = line.Substring(3);
                                break;
                            case "NO":
                                grfFile.NomeArquivo = line.Substring(3);
                                break;
                            case "HF":
                                grfFile.DataFinal = DateTime.ParseExact(line.Substring(3), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                break;
                            case "US":
                                if (string.IsNullOrWhiteSpace(line.Substring(3)))
                                {
                                    grfFile.Usuario = line.Substring(3);
                                    break;
                                }
                                else
                                {
                                    string[] nomeUsuario = line.Substring(3).Split(".");
                                    StringBuilder sb = new StringBuilder();
                                    for (int i = 0; i < nomeUsuario.Length; i++)
                                    {
                                        sb.Append(nomeUsuario[i][0].ToString().ToUpper() +
                                            nomeUsuario[i].Substring(1) +
                                            " ");
                                    }
                                    grfFile.Usuario = sb.ToString().Trim();
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(grfFile.Usuario))
            {
                grfFile.Usuario = "MASTER";
            }
            return grfFile;
        }

        public static GrfDto CriarGrfDto(string caminhoDiretorio, string nomeArquivo, string caminhoArquivo, DateTime dataFinal)
        {
            GrfDto grfDto = new GrfDto();

            grfDto.CodGravacao = GravacaoRepository.BuscarCodGravacao(nomeArquivo);
            
            string caminhoArquivoWav = $"{caminhoDiretorio + @"\" + caminhoArquivo + @"\" + nomeArquivo}";
            FileInfo fi = new FileInfo(caminhoArquivoWav);
            grfDto.NumBytes = fi.Length;

            DateTime dataInicio = GravacaoRepository.BuscarDataInicio(grfDto.CodGravacao);
            grfDto.NumSegundos = dataFinal.Subtract(dataInicio).Seconds;

            return grfDto;
        }
    }
}
