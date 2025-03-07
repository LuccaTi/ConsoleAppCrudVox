using ConsoleAppCrudVox.Models.Entities;
using ConsoleAppCrudVox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrudVox.Processors
{
    internal static class TktProcessor
    {
        public static void ProcessarTkt(string file)
        {
            Tkt tktFile = TktService.CriarTkt(file);

            int codRamal = RamalService.BuscarCodRamal(tktFile.Ramal);
            int codLogin = LoginService.BuscarCodLogin(codRamal);
            int codGravacao = GravacaoService.BuscarCodGravacao(codLogin);

            if (codRamal != 0 && codLogin != 0 && codGravacao != 0)
            {
                //Busca data inicio da gravação e reduz dez segundos
                DateTime dataInicio = GravacaoService.BuscarDataInicio(codGravacao).AddSeconds(-10);

                //Busca data final da gravação e adiciona dez segundos
                DateTime dataFinal = GravacaoService.BuscarDataFinal(codGravacao).AddSeconds(10);

                //Verificar se as datas estão no intervalo e atualiza o registro
                if (tktFile.DataInicio >= dataInicio && tktFile.DataFinal <= dataFinal
                    && !GravacaoService.VerificaSeGravacaoJaAtualizada(tktFile.TxtIdChamada, tktFile.CodDirecao))
                {
                    GravacaoService.AtualizarGravacao(codGravacao, tktFile.TxtIdChamada, tktFile.CodDirecao);
                }
            }
        }
    }
}
