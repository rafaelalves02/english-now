using EnglishNow.Repositories;
using EnglishNow.Services.Models.Professor;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Enums;

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
            var usuariId = _usuarioRepository.Inserir(new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                PapelId = (int)Papel.Professor
            });

            if (!usuariId.HasValue)
            {
                result.MensagemErro = "erro ao inserir o usario";
            }

            //inserir o professor
            _professorRepository.Inserir(new Professor
            {
                Nome = request.Nome,
                Email = request.Email,
                UsuarioId = usuariId!.Value
            });

            result.Sucesso = true;

            return result;
        }
    }
}
