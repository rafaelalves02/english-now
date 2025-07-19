using System.ComponentModel.DataAnnotations;

namespace EnglishNow.Web.Models.Professor
{
    public class CriarViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Email { get; set; }
    }
}
