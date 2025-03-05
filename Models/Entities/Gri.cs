using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Models.Entities
{
    internal class Gri
    {

        public int Canal { get; set; }
        public DateTime DataInicio { get; set; }
        public int Codec { get; set; }
        public int Header { get; set; }
        public int Ramal { get; set; }
        public string Servidor { get; set; } = "";
        public string CaminhoArquivo { get; set; } = "";
        public string NomeArquivo { get; set; } = "";
        public string Usuario { get; set; } = "";

        public Gri()
        {

        }

        public Gri(int canal, DateTime dataInicio, int codec, int header, int ramal, string servidor, string caminhoArquivo, string nomeArquivo, string usuario)
        {
            Canal = canal;
            DataInicio = dataInicio;
            Codec = codec;
            Header = header;
            Ramal = ramal;
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
                $"\nHI:{DataInicio}" +
                $"\nCC:{Codec}" +
                $"\nHD:{Header}" +
                $"\nRM:{Ramal}";
        }
    }
}