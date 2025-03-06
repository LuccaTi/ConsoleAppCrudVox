using System.Data;
using System.Text;
using System.Configuration;
using System.Globalization;
using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Services;
using FirebirdSql.Data.FirebirdClient;
using ConsoleAppCrudVox.Models.Entities;
using ConsoleAppCrudVox.Repositories;


namespace ConsoleAppCrudVox;
class Program
{
    public static void Main(string[] args)
    {
        string regDbPath = @"C:\Users\lucca\Área de Trabalho\TreinamentoVox\Grav\RegDB";
        List<string> files = new List<string>();

        try
        {
            files = Directory.GetFiles(regDbPath, "*.*").ToList();

            OrdenarListaDeArquivosPorExtensao(files);

            //Jogar tudo num metodo - cada iteração chama uma classe de processamento 
            foreach (string file in files)
            {
                string gravPath = @"C:\Users\lucca\Área de Trabalho\TreinamentoVox\Grav";

                //Processa o Gri
                if (file.EndsWith(".GRI"))
                {

                    //Cria o Gri
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

                    //Recupera (SELECT) os dados necessários e insere (INSERT) na tabela gravação.
                    if (!GravacaoService.VerificaSeGravacaoJaRegistrada(griFile.NomeArquivo))
                    {
                        GriDto griDto = GriService.CriarGriDto(griFile.Ramal);
                        GravacaoService.RegistrarGravacao(griFile, griDto);
                    }

                    //Checa se os diretórios e arquivos .wav já existem, senão, cria eles.
                    string[] griPath = griFile.CaminhoArquivo.Split(@"\");
                    for (int i = 0; i < griPath.Count(); i++)
                    {
                        gravPath += @"\" + griPath[i];
                        if (!Directory.Exists(gravPath))
                        {
                            Directory.CreateDirectory(gravPath);
                        }
                    }
                    string wavFilePath = $"{gravPath + @"\" + griFile.NomeArquivo}";
                    if (!File.Exists(wavFilePath))
                    {
                        File.Create(wavFilePath);
                    }

                }


                //Processa o Grf
                if (file.EndsWith(".GRF"))
                {
                    //Cria o Grf
                    Grf grfFile = GrfService.CriarGrfDoArquivo(file);

                    //Verifica se há um registro para ser atualizado
                    if (GravacaoService.VerificaSeGravacaoJaRegistrada(grfFile.NomeArquivo))
                    {
                        //Faz a atualização dos registros
                        if (!GravacaoService.VerificaSeGravacaoJaAtualizada(grfFile.DataFinal))
                        {


                            GrfDto grfDto = GrfService.CriarGrfDto
                                (
                                gravPath, grfFile.NomeArquivo, grfFile.CaminhoArquivo, grfFile.DataFinal
                                );
                            GravacaoService.AtualizarGravacao(grfFile, grfDto);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex);
        }
    }
    public static void OrdenarListaDeArquivosPorExtensao(List<string> listaArquivos)
    {

        listaArquivos.Sort();
        listaArquivos.Reverse();
        listaArquivos.Sort((s1, s2) => s1.Substring(s1.LastIndexOf(".")).CompareTo(s2.Substring(s2.LastIndexOf("."))));
        listaArquivos.Reverse();//Ordernar prioridade. extensão

    }
}