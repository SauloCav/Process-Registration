using Microsoft.AspNetCore.Mvc;
using ProcessRegistration.Models;

namespace ProcessRegistration.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new ProcessModel());
        }

        [HttpPost]
        public IActionResult Create(ProcessModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = true });
            }

            return PartialView("_Create", model);
        }

        [HttpGet]
        public IActionResult Details()
        {
            var model = new ProcessModel
            {
                Nome = "Exemplo de Processo",
                NPU = "1111111-11.1111.1.11.1111",
                DataCadastro = DateTime.Now,
                UF = "SP",
                Municipio = "São Paulo"
            };

            return PartialView("_Details", model);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = new ProcessModel
            {
                Nome = "Exemplo de Processo",
                NPU = "1111111-11.1111.1.11.1111",
                UF = "SP",
                Municipio = "São Paulo"
            };

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public IActionResult Edit(ProcessModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = true });
            }

            return PartialView("_Edit", model);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var model = new ProcessModel
            {
                Nome = "Exemplo de Processo",
                NPU = "1111111-11.1111.1.11.1111"
            };

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            return Json(new { success = true });
        }
    }
}
