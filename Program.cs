using System.Data;
using System.Text;
using System.Configuration;
using System.Globalization;
using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Services;
using FirebirdSql.Data.FirebirdClient;
using ConsoleAppCrudVox.Models.Entities;
using ConsoleAppCrudVox.Repositories;
using ConsoleAppCrudVox.Processors;


namespace ConsoleAppCrudVox;
class Program
{
    public static void Main(string[] args)
    {
        string regDbPath = @"C:\Users\lucca\Área de Trabalho\TreinamentoVox\Grav\RegDB";
        List<string> files = new List<string>();

        try
        {
            files = Directory.GetFiles(regDbPath, "*.*").ToList();

            OrdenarListaDeArquivosPorExtensao(files);

            foreach (string file in files)
            {
                string gravFolder = @"C:\Users\lucca\Área de Trabalho\TreinamentoVox\Grav";

                //Processa o Gri
                if (file.EndsWith(".GRI"))
                {
                    GriProcessor.ProcessarGri(file, gravFolder);
                }

                //Processa o Grf
                if (file.EndsWith(".GRF"))
                {
                    GrfProcessor.ProcessarGrf(file, gravFolder);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex);
        }
    }
    public static void OrdenarListaDeArquivosPorExtensao(List<string> listaArquivos)
    {

        listaArquivos.Sort();
        listaArquivos.Reverse();
        listaArquivos.Sort((s1, s2) => s1.Substring(s1.LastIndexOf(".")).CompareTo(s2.Substring(s2.LastIndexOf("."))));
        listaArquivos.Reverse();//Ordernar por prioridade, a extensão do arquivo.

    }
}