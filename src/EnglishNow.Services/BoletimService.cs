using EnglishNow.Repositories;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models.Boletim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services
{
    public interface IBoletimService
    {
        BoletimResult? ObterBoletimPorAlunoTurma(int alunoId, int turmaId);

        AtualizarBoletimResult Atualizar(AtualizarBoletimRequest atualizarBoletimRequest);
    }
    public class BoletimService : IBoletimService
    {
        private readonly IAlunoTurmaBoletimRepository _alunoTurmaBoletimRepository;
        public BoletimService(IAlunoTurmaBoletimRepository alunoTurmaBoletimRepository)
        {
            _alunoTurmaBoletimRepository = alunoTurmaBoletimRepository;
        }

        public AtualizarBoletimResult Atualizar(AtualizarBoletimRequest request)
        {
            var result = new AtualizarBoletimResult();

            var alunoTurmaBoletim = request.MapToAlunoTurmaBoletim();

            var affectedRows = _alunoTurmaBoletimRepository.Atualizar(alunoTurmaBoletim);

            if (!affectedRows.HasValue || affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o boletim.";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public BoletimResult? ObterBoletimPorAlunoTurma(int alunoId, int turmaId)
        {
            var alunoTurmaBoletim = _alunoTurmaBoletimRepository.ObterPorAlunoTurma(alunoId, turmaId);

            if (alunoTurmaBoletim == null)
            {
                return null;
            }

            var result = alunoTurmaBoletim.MapToBoletimResult();

            return result;
        }
    }
}
