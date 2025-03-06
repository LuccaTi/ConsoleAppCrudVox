using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Models.Entities;
using ConsoleAppCrudVox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Processors
{
    internal static class GriProcessor
    {

        public static void ProcessarGri(string file, string folder)
        {
            //Instancia o GRI
            Gri griFile = GriService.CriarGri(file);

            //Valida se o ramal já está ativo em outro canal
            if (RamalService.VerificaSeRamalAtivoOutroCanal(griFile.Canal, griFile.Ramal))
            {
                //Localiza e desativa os outros canais
                List<int> outrosCanais = RamalService.ListarOutrosCanaisDoRamal(griFile.Canal, griFile.Ramal);
                RamalService.DesativarCanais(outrosCanais);
            }
            else
            {
                //Cria o registro do ramal, vinculando com o novo canal.
                if (!RamalService.VerificaSeRamalExiste(griFile.Ramal))
                {
                    Ramal ramal = new Ramal()
                    {
                        CodServidor = ServidorService.BuscarCodServidor(griFile.Servidor),
                        CodRamal = RamalService.FornecerProxCodRamal(),
                        DataInicio = griFile.DataInicio,
                        NumCanal = griFile.Canal,
                        TxtNumRamal = griFile.Ramal
                    };
                    RamalService.RegistrarRamal(ramal);
                }
            }

            //Verifica se usuário está registrado
            string codUsuario = UsuarioService.BuscarCodUsuario(griFile.Usuario);
            if (!UsuarioService.VerificaSeUsuarioJaExiste(codUsuario))
            {
                RegistroUsuario usuario = new RegistroUsuario
                    (
                    UsuarioService.FornecerProxCodUsuario(), griFile.Usuario
                    );
                UsuarioService.RegistrarUsuario(usuario);
            }

            //Valida se tem outros usuários logados no ramal e faz o log out
            int codRamal = RamalService.BuscarCodRamal(griFile.Ramal);
            if (codUsuario != "" && codRamal != 0)
            {
                if (LoginService.VerificaSeRamalAtivoOutrosUsuarios(codRamal, codUsuario))
                {
                    List<string> usuarios = LoginService.ListarOutrosUsuariosLogados(codRamal, codUsuario);
                    LoginService.FazerLogOut(usuarios);
                }
            }

            //Login do usuário atual
            if (!LoginService.VerificaSeUsuarioJaLogado(codUsuario))
            {
                LoginUsuario usuario = new LoginUsuario()
                {
                    CodRamal = codRamal,
                    CodLogin = LoginService.FornecerProxCodLogin(),
                    DatLogin = griFile.DataInicio,
                    CodUsuario = codUsuario
                };
                LoginService.FazerLogin(usuario);
            }

            //Login do usuário quando campo usuário não está preenchido
            if (griFile.Usuario.Equals("MASTER") && !GravacaoService.VerificaSeGravacaoJaRegistrada(griFile.NomeArquivo))
            {
                LoginUsuario usuarioGenerico = new LoginUsuario()
                {
                    CodRamal = codRamal,
                    CodLogin = LoginService.FornecerProxCodLogin(),
                    DatLogin = griFile.DataInicio
                };
                LoginService.FazerLogin(usuarioGenerico);
            }

            //Recupera mais dados e insere novo registro na tabela gravação.
            if (!GravacaoService.VerificaSeGravacaoJaRegistrada(griFile.NomeArquivo))
            {
                GriDto griDto = GriService.CriarGriDto(griFile.Ramal);
                GravacaoService.RegistrarGravacao(griFile, griDto);
            }

            //Checa se os diretórios e arquivos .wav já existem, senão, cria eles.
            string[] griPath = griFile.CaminhoArquivo.Split(@"\");
            for (int i = 0; i < griPath.Count(); i++)
            {
                folder += @"\" + griPath[i];
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            string wavFilePath = $"{folder + @"\" + griFile.NomeArquivo}";
            if (!File.Exists(wavFilePath))
            {
                File.Create(wavFilePath);
            }
        }
    }
}
