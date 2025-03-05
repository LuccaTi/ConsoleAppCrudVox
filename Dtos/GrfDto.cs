using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Dtos
{
    internal class GrfDto
    {
        public int CodGravacao { get; set; }
        public long NumBytes { get; set; }
        public int NumSegundos { get; set; }

        public GrfDto()
        {

        }

        public GrfDto(int codGravacao, long numBytes, int numSegundos)
        {
            CodGravacao = codGravacao;
            NumBytes = numBytes;
            NumSegundos = numSegundos;
        }
    }
}
