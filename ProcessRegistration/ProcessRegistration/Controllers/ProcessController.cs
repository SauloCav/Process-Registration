using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessRegistration.Data;
using ProcessRegistration.Models;
using System.Text.Json;
using X.PagedList.Extensions;

namespace ProcessRegistration.Controllers
{
    public class ProcessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcessController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var processes = _context.Processes.ToList();
            return View(processes);
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 8;

            var pagedList = _context.Processes
                                    .OrderBy(p => p.Id)
                                    .ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new ProcessModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProcessModel model)
        {
            if (ModelState.IsValid)
            {
                model.RegistrationDate = DateTime.Now;

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://servicodados.ibge.gov.br/api/v1/localidades/municipios/{model.City}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var cityData = JsonSerializer.Deserialize<JsonElement>(responseContent);

                    model.City = cityData.GetProperty("nome").GetString();
                    model.CityCode = cityData.GetProperty("id").GetInt32();
                }
                else
                {
                    return BadRequest(new { message = "Erro ao obter informações do município." });
                }

                _context.Processes.Add(model);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Processo cadastrado com sucesso!" });
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _context.Processes.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            model.ViewDate = DateTime.Now;
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();

            return PartialView("_Details", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _context.Processes.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            return PartialView("_Edit", model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] ProcessModel model)
        {
            if (ModelState.IsValid)
            {
                var existingProcess = await _context.Processes.FindAsync(model.Id);
                if (existingProcess == null)
                {
                    return NotFound(new { message = "Processo não encontrado." });
                }

                existingProcess.Name = model.Name;
                existingProcess.NPU = model.NPU;
                existingProcess.State = model.State;

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://servicodados.ibge.gov.br/api/v1/localidades/municipios/{model.City}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var cityData = JsonSerializer.Deserialize<JsonElement>(responseContent);

                    existingProcess.City = cityData.GetProperty("nome").GetString();
                    existingProcess.CityCode = cityData.GetProperty("id").GetInt32();
                }
                else
                {
                    return BadRequest(new { message = "Erro ao obter informações do município." });
                }

                existingProcess.ViewDate = DateTime.Now;

                _context.Entry(existingProcess).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Processo atualizado com sucesso!" });
            }

            return BadRequest(ModelState);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _context.Processes.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Processes.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Processes.Remove(model);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmView(int id)
        {
            var model = await _context.Processes.FindAsync(id);
            if (model == null)
            {
                return NotFound(new { message = "Processo não encontrado." });
            }

            model.ViewDate = DateTime.Now;
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

    }
}
