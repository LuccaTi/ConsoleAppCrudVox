using ConsoleAppCrudVox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Services
{
    internal class TktService
    {
        public static Tkt CriarTkt(string caminhoArquivo)
        {
            Tkt tktFile = new Tkt();
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
                            case "HI":
                                tktFile.DataInicio = DateTime.ParseExact(line.Substring(3), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                break;
                            case "HF":
                                tktFile.DataFinal = DateTime.ParseExact(line.Substring(3), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                break;
                            case "NI":
                                tktFile.Ramal = int.Parse(line.Substring(3));
                                break;
                            case "NU":
                                tktFile.TxtIdChamada = line.Substring(3);
                                break;
                            case "DI":
                                tktFile.CodDirecao = int.Parse(line.Substring(3));
                                break;
                            case "AG":
                                if (string.IsNullOrWhiteSpace(line.Substring(3)))
                                {
                                    tktFile.Usuario = line.Substring(3);
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
                                    tktFile.Usuario = sb.ToString().Trim();
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(tktFile.Usuario))
            {
                tktFile.Usuario = "MASTER";
            }
            return tktFile;
        }
    }
}
