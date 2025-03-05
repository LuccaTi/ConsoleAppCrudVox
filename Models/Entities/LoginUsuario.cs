using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Models.Entities
{
    internal class LoginUsuario
    {
        public int CodRamal { get; set; }
        public int CodLogin { get; set; }//NOT NULL
        public DateTime DatLogin { get; set; }//NOT NULL
        public DateTime? DatLogout { get; set; }
        public string CodUsuario { get; set; } = "MASTER";
        public char FlgLogoutNorm { get; set; } = 'S';//NOT NULL

        public LoginUsuario()
        {

        }

        public LoginUsuario(int codRamal, int codLogin, DateTime datLogin, DateTime? datLogout)
        {
            CodRamal = codRamal;
            CodLogin = codLogin;
            DatLogin = datLogin;
            DatLogout = datLogout;
        }

        public LoginUsuario(int codRamal, int codLogin, DateTime datLogin, DateTime? datLogout, string codUsuario, char flgLogoutNorm) : this(codRamal, codLogin, datLogin, datLogout)
        {
            CodUsuario = codUsuario;
            FlgLogoutNorm = flgLogoutNorm;
        }
    }
}
