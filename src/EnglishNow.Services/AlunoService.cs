using EnglishNow.Repositories;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models.Aluno;

namespace EnglishNow.Services
{
    public interface IAlunoService
    {
        CriarAlunoResult Criar(CriarAlunoRequest request);

        EditarAlunoResult Editar(EditarAlunoRequest request);

        ExcluirAlunoResult Excluir(int id);

        IList<AlunoResult> Listar();

        IList<AlunoResult> ListarPorTurma(int turmaId);

        IList<AlunoResult> ListarPorProfessor(int usuarioId);

        IList<AlunoResult> ListarPorAluno(int usuarioId);

        AlunoResult? ObterPorId(int id);

        AlunoResult? ObterPorUsuarioId(int id);
    }

    public class AlunoService : IAlunoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IUsuarioRepository usuarioRepository, IAlunoRepository alunoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _alunoRepository = alunoRepository;
        }

        public CriarAlunoResult Criar(CriarAlunoRequest request)
        {
            var result = new CriarAlunoResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null)
            {
                result.MensagemErro = "Usuario já existente";

                return result;
            }

            var usuarioId = _usuarioRepository.Inserir(request.MapToUsuario());

            if (!usuarioId.HasValue)
            {
                result.MensagemErro = "Erro ao inserir usuário";

                return result;
            }

            _alunoRepository.Inserir(request.MapToAluno(usuarioId.Value));

            result.Sucesso = true;

            return result;
        }

        public EditarAlunoResult Editar(EditarAlunoRequest request)
        {
            var result = new EditarAlunoResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null && usuarioExistente.Id != request.UsuarioId)
            {
                result.MensagemErro = "Já existe um usuário com esse login";

                return result;
            }

            var aluno = request.MapToAluno();

            var affectedRows = _alunoRepository.Atualizar(aluno);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o aluno";

                return result;
            }

            var usuario = request.MapToUsuario();

            affectedRows = _usuarioRepository.Atualizar(usuario);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o usuário";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public ExcluirAlunoResult Excluir(int id)
        {
            var result = new ExcluirAlunoResult();

            var aluno = _alunoRepository.ObterPorId(id);

            if (aluno == null)
            {
                result.MensagemErro = "Não foi possível encontrar o aluno";

                return result;
            }

            var affectedRows = _alunoRepository.Apagar(id);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível apagar o aluno";

                return result;
            }

            affectedRows = _usuarioRepository.Apagar(aluno.UsuarioId);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível apagar o usuario";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public IList<AlunoResult> Listar()
        {
            var professores = _alunoRepository.Listar();

            var result = professores.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorTurma(int turmaId)
        {
            var professores = _alunoRepository.ListarPorTurma(turmaId);

            var result = professores.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorProfessor(int usuarioId)
        {
            var alunos = _alunoRepository.ListarPorProfessor(usuarioId);

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorAluno(int usuarioId)
        {
            var alunos = _alunoRepository.ListarPorAluno(usuarioId);

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public AlunoResult? ObterPorId(int id)
        {
            var aluno = _alunoRepository.ObterPorId(id);

            if (aluno == null)
            {
                return null;
            }

            var result = aluno.MapToAlunoResult();

            return result;
        }

        public AlunoResult? ObterPorUsuarioId(int usuarioId)
        {
            var aluno = _alunoRepository.ObterPorUsuarioId(usuarioId);

            if (aluno == null)
            {
                return null;
            }

            var result = aluno.MapToAlunoResult();

            return result;
        }
    }
}
