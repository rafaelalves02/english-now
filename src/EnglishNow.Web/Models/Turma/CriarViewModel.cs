using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnglishNow.Web.Models.Turma
{
    public class CriarViewModel
    {
        [Required(ErrorMessage = "O campo ano é obrigatório")]
        public int? Ano { get; set; }

        [Required(ErrorMessage = "O campo semestre é obrigatório")]
        public int Semestre { get; set; }

        [Required(ErrorMessage = "O campo professor é obrigatório")]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "O campo nivel é obrigatório")]
        public string? Nivel { get; set; }

        [Required(ErrorMessage = "O campo periodo é obrigatório")]
        public string? Periodo { get; set; }

        public List<SelectListItem>? Semestres { get; set; }

        public List<SelectListItem>? Professores { get; set; }
    }
}
