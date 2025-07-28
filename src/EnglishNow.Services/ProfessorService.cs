using EnglishNow.Repositories;
using EnglishNow.Services.Models.Professor;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Enums;
using EnglishNow.Services.Mappings;
using MySqlX.XDevAPI.Common;

namespace EnglishNow.Services
{
    public interface IProfessorService
    {
        CriarProfessorResult Criar(CriarProfessorRequest request);

        EditarProfessorResult Editar(EditarProfessorRequest request);

        ExcluirProfessorResult Excluir(int id);

        IList<ProfessorResult> Listar();

        ProfessorResult? ObterPorId(int id);
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

        public EditarProfessorResult Editar(EditarProfessorRequest request)
        {
            var result = new EditarProfessorResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null && usuarioExistente.Id != request.UsuarioId)
            {
                result.MensagemErro = "Já existe um usuário com esse login";

                return result;
            }

            var professor = request.MapToProfessor();

            var affectedRows = _professorRepository.Atualizar(professor);

            if (affectedRows == 0)
            {
                result.MensagemErro = "não foi possível atualizar o professor";

                return result;
            }

            var usuario = request.MapToUsuario();

            affectedRows = _usuarioRepository.Atualizar(usuario);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o usuario";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public ExcluirProfessorResult Excluir(int id)
        {
            var result = new ExcluirProfessorResult();

            var professor = _professorRepository.ObterPorId(id);

            if (professor == null)
            {
                result.MensagemErro = "Não foi possível encontrar o professor";

                return result;
            }

            var affectedRows = _professorRepository.Apagar(id);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o professor";

                return result;
            }

            affectedRows = _usuarioRepository.Apagar(professor.UsuarioId);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o usuário";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public IList<ProfessorResult> Listar()
        {
            var professores = _professorRepository.Listar();

            var result = professores.Select(c => c.MapToProfessorResult()).ToList();

            return result;
        }

        public ProfessorResult? ObterPorId(int id)
        {
            var professor = _professorRepository.ObterPorId(id);

            if (professor == null)
            {
                return null;
            }

            var result = professor.MapToProfessorResult();

            return result;
        }
    }
}
