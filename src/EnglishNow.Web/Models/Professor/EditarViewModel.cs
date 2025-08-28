using System.ComponentModel.DataAnnotations;

namespace EnglishNow.Web.Models.Professor
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Email { get; set; }

        public IList<TurmasProfessorViewModel>? TurmasProfessor { get; set; }

    }

    public class TurmasProfessorViewModel
    {
        public int Id { get; set; }
        public string? Nivel { get; set; }
        public string? Periodo { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }
    }
}
