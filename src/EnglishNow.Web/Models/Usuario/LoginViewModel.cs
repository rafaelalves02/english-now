using System.ComponentModel.DataAnnotations;

namespace EnglishNow.Web.Models.Usuario
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "o campo usuario é obrigatório")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "o campo senha é obrigatório")]
        public string? Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
