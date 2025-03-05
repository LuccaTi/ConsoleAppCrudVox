using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppCrudVox.Models.Entities;

namespace ConsoleAppCrudVox.Dtos
{
    internal class GriDto
    {
        public int CodRamal { get; set; }
        public int NumCanal { get; set; }
        public int CodServidor { get; set; }
        public int CodOperacao { get; set; }
        public int CodLogin { get; set; }
        public int CodDirecao { get; set; } = 0;
        public char FlgExcBloq { get; set; } = 'N';
        public char FlgMarcada { get; set; } = 'N';
        public int NumBytes { get; set; } = 0;
        public int NumSegundos { get; set; } = 0;
        public int NumStatDisco { get; set; } = 1;

        public GriDto()
        {

        }
        public GriDto(int codRamal, int numCanal, int codServidor, int codOperacao, int codLogin)
        {
            CodRamal = codRamal;
            NumCanal = numCanal;
            CodServidor = codServidor;
            CodOperacao = codOperacao;
            CodLogin = codLogin;
        }
    }
}
