using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Models.Entities
{
    internal class RegistroUsuario
    {
        public int CodUsuario { get; set; }//NOT NULL
        public int CodCargo { get; set; } = 2;
        public char FlgAtivo { get; set; } = 'S';//NOT NULL
        public string Language { get; set; } = "PT-Br";
        public string Login { get; set; } = "";
        public string NomCurto { get; set; } = "";//NOT NULL
        public string NomUsuario { get; set; } = "";
        public string Senha { get; private set; } = "";


        public RegistroUsuario()
        {

        }

        public RegistroUsuario(int codUsuario, string nomeUsuario)
        {
            CodUsuario = codUsuario;
            Login = nomeUsuario.ToUpper();
            NomCurto = nomeUsuario.Substring(0, 8).ToUpper();
            NomUsuario = nomeUsuario.ToUpper();
        }

        public void GerarSenha(string senha)
        {
            Senha = senha;
        }
    }
}
