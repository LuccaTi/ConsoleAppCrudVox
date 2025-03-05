using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Models.Entities;
using ConsoleAppCrudVox.Repositories;
using FirebirdSql.Data.FirebirdClient;

namespace ConsoleAppCrudVox.Services
{
    internal class GriService
    {
        public static Gri CriarGri(string caminhoArquivo)
        {
            Gri griFile = new Gri();
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
                                griFile.Servidor = line.Substring(3);
                                break;
                            case "CH":
                                griFile.Canal = int.Parse(line.Substring(3));
                                break;
                            case "CA":
                                griFile.CaminhoArquivo = line.Substring(3);
                                break;
                            case "NO":
                                griFile.NomeArquivo = line.Substring(3);
                                break;
                            case "HI":
                                griFile.DataInicio = DateTime.ParseExact(line.Substring(3), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                break;
                            case "US":
                                if (string.IsNullOrWhiteSpace(line.Substring(3)))
                                {
                                    griFile.Usuario = line.Substring(3);
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
                                    griFile.Usuario = sb.ToString().Trim();
                                    break;
                                }
                            case "CC":
                                griFile.Codec = int.Parse(line.Substring(3));
                                break;
                            case "HD":
                                griFile.Header = int.Parse(line.Substring(3));
                                break;
                            case "RM":
                                griFile.Ramal = int.Parse(line.Substring(3));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(griFile.Usuario))
            {
                griFile.Usuario = "MASTER";
            }
            return griFile;
        }

        public static GriDto CriarGriDto(int ramal)
        {
            int[] dados = RamalRepository.BuscarDadosParaDto(ramal);
            int codLogin = LoginRepository.BuscarCodLogin(dados[0]);

            GriDto griDto = new GriDto()
            {
                CodRamal = dados[0],
                NumCanal = dados[1],
                CodServidor = dados[2],
                CodOperacao = dados[3],
                CodLogin = codLogin
            };
            return griDto;
        }
    }
}
