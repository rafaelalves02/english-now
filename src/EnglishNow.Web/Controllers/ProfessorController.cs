using EnglishNow.Services;
using EnglishNow.Services.Models.Professor;
using EnglishNow.Web.Mappings;
using EnglishNow.Web.Models.Professor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers
{
    [Route("professor")]
    [Authorize(Roles = "Administrador")]

    public class ProfessorController : Controller
    {
        private readonly IProfessorService _professorService;
        private readonly ITurmaService _turmaService;
        public ProfessorController(IProfessorService professorService, ITurmaService turmaService)
        {
            _professorService = professorService;

            _turmaService = turmaService;
        }

        [Route("criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //cria o professor

            var result = _professorService.Criar(model.MapToCriarProfessorRequest());
            

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Route("listar")]
        public IActionResult Listar()
        {
            var professores = _professorService.Listar();

            var result = professores.Select(c => c.MapToListarViewModel()).ToList();

            return View(result);
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var professor = _professorService.ObterPorId(id);

            var model = new EditarViewModel();

            model = professor?.MapToEditarViewModel();

            model!.TurmasProfessor = _turmaService.ListarPorProfessorId(id)
                .Select(t => t.MapToTurmasProfessorViewModel()).ToList();

            return View(model);
        }

        [Route("editar/{id}")]
        [HttpPost]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToEditarProfessorRequest();

            var result = _professorService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");

        }

        [Route("excluir/{id}")]
        [HttpPost]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _professorService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                TempData["Erro"] = result.MensagemErro;

                return RedirectToAction("Editar", new { id = model.Id });
            }

            return RedirectToAction("Listar");
        }

        public IActionResult Cancelar()
        {
            return RedirectToAction("Listar");
        }
    }
}
