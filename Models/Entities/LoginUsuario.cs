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

        public LoginUsuario(int codRamal, int codLogin, DateTime datLogin)//LOGIN usuario sem nome
        {
            CodRamal = codRamal;
            CodLogin = codLogin;
            DatLogin = datLogin;
        }

        public LoginUsuario(int codRamal, int codLogin, DateTime datLogin, string codUsuario)//LOGIN usuario com nome
        {
            CodRamal = codRamal;
            CodLogin = codLogin;
            DatLogin = datLogin;
            CodUsuario = codUsuario;
        }

    }
}
