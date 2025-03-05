using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Models.Entities
{
    internal class Grf
    {

        public int Canal { get; set; }
        public DateTime DataFinal { get; set; }
        public string Servidor { get;  set; } = "";
        public string CaminhoArquivo { get;  set; } = "";
        public string NomeArquivo { get;  set; } = "";
        public string Usuario { get; set; } = "";

        public Grf()
        {

        }

        public Grf(int canal, DateTime dataFinal, string servidor, string caminhoArquivo, string nomeArquivo, string usuario)
        {
            Canal = canal;
            DataFinal = dataFinal;
            Servidor = servidor;
            CaminhoArquivo = caminhoArquivo;
            NomeArquivo = nomeArquivo;
            Usuario = usuario;
        }

        public override string ToString()
        {
            return $"SR:{Servidor}" +
                $"\nCH:{Canal}" +
                $"\nCA:{CaminhoArquivo}" +
                $"\nNO:{NomeArquivo}" +
                $"\nHF:{DataFinal}";
        }
    }
}
