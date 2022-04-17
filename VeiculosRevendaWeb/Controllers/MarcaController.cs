using Microsoft.AspNetCore.Mvc;
using System;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IMarcaRepository marcaRepository;
        private readonly IVeiculoRepository veiculoRepository;

        public MarcaController(IMarcaRepository marcaRepository, IVeiculoRepository veiculoRepository)
        {
            this.marcaRepository = marcaRepository;
            this.veiculoRepository = veiculoRepository;
        }

        public IActionResult Index()
        {
            return View(marcaRepository.GetMarcas());
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Marca model)
        {
            if (ModelState.IsValid && CustomValidationIsValid(model.Nome))
            {
                marcaRepository.Add(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            return View(marcaRepository.GetMarcaById(id));
        }

        public IActionResult Edit(int id)
        {
            return View(marcaRepository.GetMarcaById(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Marca model)
        {
            if (ModelState.IsValid)
            {
                marcaRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            if (CustomValidationBeforeDelete((int)id))
            {
                TempData["MSG_ERRO"] = "Esta Marca esta sendo utilizada na tabela de Veículos!";
                return RedirectToAction(nameof(Index));
            }
            
            marcaRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomValidationBeforeDelete(int id)
        {
            return veiculoRepository.GetMarcaIdInVeiculos(id);
        }

        private bool CustomValidationIsValid(string nome)
        {
            bool ret = true;

            if (marcaRepository.ValidaNomeMarca(nome))
            {
                ModelState.AddModelError("Nome", "Já existe uma Marca cadastrada com o mesmo nome!");
                ret = false;
            }

            return ret;
        }
    }
}