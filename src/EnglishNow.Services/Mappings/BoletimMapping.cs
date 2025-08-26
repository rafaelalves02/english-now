using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Models.Boletim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Mappings
{
    public static class BoletimMapping
    {
        public static BoletimResult MapToBoletimResult(this AlunoTurmaBoletim entity)
        {
            var model = new BoletimResult
            {
                BoletimId = entity.Id,
                AlunoId = entity.AlunoId,
                TurmaId = entity.TurmaId,
                NotaBim1Escrita = entity.NotaBim1Escrita,
                NotaBim1Leitura = entity.NotaBim1Leitura,
                NotaBim1Conversacao = entity.NotaBim1Conversacao,
                NotaBim1Final = entity.NotaBim1Final,
                NotaBim2Escrita = entity.NotaBim2Escrita,
                NotaBim2Leitura = entity.NotaBim2Leitura,
                NotaBim2Conversacao = entity.NotaBim2Conversacao,
                NotaBim2Final = entity.NotaBim2Final,
                NotaFinalSemestre = entity.NotaFinalSemestre,
                FaltasSemestre = entity.FaltasSemestre,
                NomeAluno = entity.Aluno?.Nome,
                AnoTurma = entity.Turma?.Ano,
                SemestreTurma = entity.Turma?.Semestre,
                PeriodoTurma = entity.Turma?.Periodo,
                NivelTurma = entity.Turma?.Nivel
            };

            return model;
        }

        public static AlunoTurmaBoletim MapToAlunoTurmaBoletim(this AtualizarBoletimRequest request)
        {
            var entity = new AlunoTurmaBoletim
            {
                Id = request.BoletimId,
                NotaBim1Escrita = request.NotaBim1Escrita,
                NotaBim1Leitura = request.NotaBim1Leitura,
                NotaBim1Conversacao = request.NotaBim1Conversacao,
                NotaBim1Final = request.NotaBim1Final,
                NotaBim2Escrita = request.NotaBim2Escrita,
                NotaBim2Leitura = request.NotaBim2Leitura,
                NotaBim2Conversacao = request.NotaBim2Conversacao,
                NotaBim2Final = request.NotaBim2Final,
                NotaFinalSemestre = request.NotaFinalSemestre,
                FaltasSemestre = request.FaltasSemestre
            };
            return entity;
        }
    }
}
