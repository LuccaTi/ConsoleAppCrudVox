using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Models.Entities
{
    internal class Tkt
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public int Ramal { get; set; }
        public string TxtIdChamada { get; set; } = "";
        public int CodDirecao { get; set; }
        public string Usuario { get; set; } = "";

        public Tkt()
        {

        }

        public Tkt(DateTime datInicio, DateTime datFinal, int ramal, string txtIdChamada, int codDirecao, string usuario)
        {
            DataInicio = datInicio;
            DataFinal = datFinal;
            Ramal = ramal;
            TxtIdChamada = txtIdChamada;
            CodDirecao = codDirecao;
            Usuario = usuario;
        }

        public override string ToString()
        {
            return $"HI:{DataInicio}" +
                $"\nHF:{DataFinal}" +
                $"\nNI:{Ramal}" +
                $"\nNU:{TxtIdChamada}" +
                $"\nDI:{CodDirecao}" +
                $"\nAG:{Usuario}";
        }
    }
}
