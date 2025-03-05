using System.Data;
using System.Text;
using System.Configuration;
using System.Globalization;
using ConsoleAppCrudVox.Data;
using ConsoleAppCrudVox.Dtos;
using ConsoleAppCrudVox.Services;
using FirebirdSql.Data.FirebirdClient;
using ConsoleAppCrudVox.Models.Entities;


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

            ReverterListaDeArquivosPorExtensao(files);

            foreach (string file in files)
            {
                string gravPath = @"C:\Users\lucca\Área de Trabalho\TreinamentoVox\Grav";

                //Processa o Gri
                if (file.EndsWith(".GRI"))//Jogar num metodo - classe de processamento 
                {
                    //Cria o Gri
                    Gri griFile = GriService.CriarGri(file);

                    //Valida se o ramal do Gri já está ativo em outro canal
                    if (AcessoFb.VerificaRamalAtivoOutroCanal(griFile.Canal, griFile.Ramal))
                    {
                        continue;
                        //Log out do ramal na tabela login,
                        //Desativa o ramal, flgRegAtivo, flgRamalAtivo = 'N'
                        //Cria o registro do ramal, vinculando com o novo canal.
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

                    //Validação dos dados do Gri antes de inserir na base
                    if (!AcessoFb.RamalExiste(griFile.Ramal))
                    {
                        RamalInsert ramalInsert = new RamalInsert()
                        {
                            CodServidor = AcessoFb.ProcurarCodServidor(griFile.Servidor),
                            CodRamal = AcessoFb.FornecerCodRamal(),
                            DataInicio = griFile.DataInicio,
                            NumCanal = griFile.Canal,
                            TxtNumRamal = griFile.Ramal
                        };
                        AcessoFb.InserirRamal(ramalInsert);
                    }

                    //Verifica se usuário não está registrado
                    if (!AcessoFb.UsuarioExiste(griFile.Usuario))
                    {
                        RegistroUsuario usuario = new RegistroUsuario()
                        {
                            CodUsuario = AcessoFb.FornecerCodUsuario(),
                            NomUsuario = griFile.Usuario.ToUpper(),
                            Login = griFile.Usuario.ToUpper(),
                            NomCurto = TornarCurtoNome(griFile.Usuario)
                        };
                        AcessoFb.InserirUsuario(usuario);
                    }

                    //Valida se tem outros usuários logados no ramal e faz o log out, depois faz o login do atual
                    string codUsuario = AcessoFb.ProcurarCodUsuario(griFile.Usuario);
                    int codRamal = AcessoFb.ProcurarCodRamal(griFile.Ramal);
                    if (codUsuario != "" && codRamal != 0)
                    {
                        if (AcessoFb.ChecarRamalEmOutroUsuario(codRamal, codUsuario))
                        {
                            List<string> usuarios = AcessoFb.MostrarUsuariosNoRamal(codRamal, codUsuario);
                            AcessoFb.AtualizarUsuarioLogOut(usuarios);

                            if (!AcessoFb.VerificaSeUsuarioLogado(codUsuario))
                            {
                                UsuarioLogin usuario = new UsuarioLogin()
                                {
                                    //login do usuario não logado
                                };
                            }
                        }
                    }

                    //Faz o login do usuário MASTER quando campo usuário não está preenchido no Gri
                    if (griFile.Usuario.Equals("MASTER") && !AcessoFb.VerificaExisteRegistroGravacao(griFile.NomeArquivo))
                    {
                        Master master = AcessoFb.ProcurarDadosInsertMasterLogin(griFile.Ramal, griFile.Usuario, griFile.DataInicio);
                        AcessoFb.FazerLoginUsuarioMaster(master);
                    }

                    //Recupera (SELECT) os dados necessários e insere (INSERT) na tabela gravação.
                    if (!AcessoFb.VerificaExisteRegistroGravacao(griFile.NomeArquivo))
                    {
                        GriDto griDto = AcessoFb.ProcurarDadosInsertGri(griFile.Ramal);
                        AcessoFb.InserirGravacao(griFile, griDto, new GriInsert());
                    }
                }


                //Processa o Grf
                if (file.EndsWith(".GRF"))
                {
                    //Cria o Grf
                    Grf grfFile = GrfService.CriarGrfDoArquivo(file);

                    //Verifica se há um registro (Gri) para ser atualizado
                    if (AcessoFb.VerificaExisteRegistroGravacao(grfFile.NomeArquivo))
                    {
                        //Faz a atualização (UPDATE) dos registros após o fim da gravação.
                        if (!AcessoFb.VerificaSeAtualizado(grfFile.DataFinal))
                        {
                            int codGravacao = AcessoFb.ProcurarCodGravacao(grfFile.NomeArquivo);

                            string wavFilePath = $"{gravPath + @"\" + grfFile.CaminhoArquivo + @"\" + grfFile.NomeArquivo}";

                            FileInfo fi = new FileInfo(wavFilePath);
                            long numBytes = fi.Length;

                            DateTime dataInicio = AcessoFb.ProcurarDataInicioUpdate(codGravacao);

                            int numSegundos = grfFile.DataFinal.Subtract(dataInicio).Seconds;
                            //Usar o GRF SERVICE agora
                            GrfUpdate grfUpdate = new GrfUpdate(codGravacao, numBytes, numSegundos);
                            AcessoFb.AlterarDadosGravacao(grfFile, grfUpdate);

                            int codLogin = AcessoFb.ProcurarCodLogin(codGravacao);
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
    public static void ReverterListaDeArquivosPorExtensao(List<string> listaArquivos)
    {

        listaArquivos.Sort();
        listaArquivos.Reverse();
        listaArquivos.Sort((s1, s2) => s1.Substring(s1.LastIndexOf(".")).CompareTo(s2.Substring(s2.LastIndexOf("."))));
        listaArquivos.Reverse();//Ordernar prioridade. extensão

    }
    public static string TornarCurtoNome(string nomeUsuario)
    {
        string[] nomeCompleto = nomeUsuario.Split(" ");
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < nomeCompleto.Length; i++)
        {
            sb.Append(nomeCompleto[i] + " ");
        }
        return sb.ToString().ToUpper().Trim();
    }
}