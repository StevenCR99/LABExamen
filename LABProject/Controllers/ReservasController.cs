using LABproject.Data;
using LABproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace labproject.Controllers
{
    public class ReservasController : Controller
    {
        public IActionResult Index()
        {
            var reservas = ReservasRepo.ObtenerTodos();
            return View(reservas);
        }

        public IActionResult Create()
        {
            return View(new Reserva());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ReservasRepo.ExisteCodigo(model.Codigo))
            {
                ModelState.AddModelError(nameof(model.Codigo), $"El código '{model.Codigo}' ya existe.");
                return View(model);
            }

            ReservasRepo.Agregar(model);
            TempData["Mensaje"] = $"Reserva '{model.Codigo}' registrada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
