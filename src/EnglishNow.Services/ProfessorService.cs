using EnglishNow.Repositories;
using EnglishNow.Services.Models.Professor;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Enums;
using EnglishNow.Services.Mappings;

namespace EnglishNow.Services
{
    public interface IProfessorService
    {
        CriarProfessorResult Criar(CriarProfessorRequest request);
    }

    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProfessorService(IProfessorRepository professorRepository, IUsuarioRepository usuarioRepository)
        {
            _professorRepository = professorRepository;
            _usuarioRepository = usuarioRepository;
        }

        public CriarProfessorResult Criar(CriarProfessorRequest request)
        {
            var result = new CriarProfessorResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null)
            {
                result.MensagemErro = "usuario já existente";

                return result;
            }

            //inserir usuario
            var usuarioId = _usuarioRepository.Inserir(request.MapToUsuario());
            

            if (!usuarioId.HasValue)
            {
                result.MensagemErro = "erro ao inserir o usario";
            }

            //inserir o professor
            _professorRepository.Inserir(request.MapToProfessor(usuarioId!.Value));
           

            result.Sucesso = true;

            return result;
        }
    }
}
