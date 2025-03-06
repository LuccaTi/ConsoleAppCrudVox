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
    internal static class GrfProcessor
    {
        public static void ProcessarGrf(string file, string folder)
        {
            //Instancia o GRF
            Grf grfFile = GrfService.CriarGrfDoArquivo(file);

            //Verifica se há um registro para ser atualizado
            if (GravacaoService.VerificaSeGravacaoJaRegistrada(grfFile.NomeArquivo))
            {
                //Faz a atualização dos registros
                if (!GravacaoService.VerificaSeGravacaoJaAtualizada(grfFile.DataFinal))
                {


                    GrfDto grfDto = GrfService.CriarGrfDto
                        (
                        folder, grfFile.NomeArquivo, grfFile.CaminhoArquivo, grfFile.DataFinal
                        );
                    GravacaoService.AtualizarGravacao(grfFile, grfDto);
                }
            }
        }
    }
}
