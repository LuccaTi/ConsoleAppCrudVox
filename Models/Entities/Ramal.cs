using ConsoleAppCrudVox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Models.Entities
{
    internal class Ramal
    {
        public int CodServidor { get; set; }//NOT NULL
        public int NumCanal { get; set; }//NOT NULL
        public int TxtNumRamal { get; set; }//NOT NULL
        public int CodRamal { get; set; }//NOT NULL
        public DateTime DataInicio { get; set; }
        public int CodOperacao { get; set; } = 2;
        public DateTime? DataFim { get; set; } = null;
        public char FlgRamalAtivo { get; set; } = 'S';//NOT NULL
        public char FlgRamalFixo { get; set; } = 'S';
        public char FlgRegAtivo { get; set; } = 'S';//NOT NULL
        public char FlgOperacaoFixa { get; set; } = 'S';
        public char FlgUsuarioFixo { get; set; } = 'S';

        public Ramal()
        {

        }

        public Ramal(int codServidor, int numCanal, int txtNumRamal, int codRamal, DateTime dataInicio)
        {
            CodServidor = codServidor;
            NumCanal = numCanal;
            TxtNumRamal = txtNumRamal;
            CodRamal = codRamal;
            DataInicio = dataInicio;
        }

        public Ramal(int codServidor, int numCanal, int txtNumRamal, int codRamal, DateTime dataInicio, int codOperacao, DateTime? dataFim, char flgRamalAtivo, char flgRamalFixo, char flgRegAtivo, char flgOperacaoFixa, char flgUsuarioFixo) : this(codServidor, numCanal, txtNumRamal, codRamal, dataInicio)
        {
            CodOperacao = codOperacao;
            DataFim = dataFim;
            FlgRamalAtivo = flgRamalAtivo;
            FlgRamalFixo = flgRamalFixo;
            FlgRegAtivo = flgRegAtivo;
            FlgOperacaoFixa = flgOperacaoFixa;
            FlgUsuarioFixo = flgUsuarioFixo;
        }
    }
}
