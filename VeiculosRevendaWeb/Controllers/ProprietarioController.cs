using Microsoft.AspNetCore.Mvc;
using System;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Controllers
{
    public class ProprietarioController : Controller
    {
        private readonly IProprietarioRepository proprietarioRepository;
        private readonly IVeiculoRepository veiculoRepository;

        public ProprietarioController(IProprietarioRepository proprietarioRepository, IVeiculoRepository veiculoRepository)
        {
            this.proprietarioRepository = proprietarioRepository;
            this.veiculoRepository = veiculoRepository;
    }

        public IActionResult Index()
        {
            return View(proprietarioRepository.GetProprietarios());
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Proprietario model)
        {
            if (ModelState.IsValid && CustomValidationIsValid(model.Documento))
            {
                proprietarioRepository.Add(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            return View(proprietarioRepository.GetProprietarioById(id));
        }

        public IActionResult Edit(int id)
        {
            return View(proprietarioRepository.GetProprietarioById(id));
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Proprietario model)
        {
            if (ModelState.IsValid)
            {
                proprietarioRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            if (CustomValidationBeforeDelete((int)id))
            {
                TempData["MSG_ERRO"] = "Este Proprietário esta sendo utilizado na tabela de Veículos!";
                return RedirectToAction(nameof(Index));
            }

            proprietarioRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomValidationBeforeDelete(int id)
        {
            return veiculoRepository.GetMarcaIdInVeiculos(id);
        }

        private bool CustomValidationIsValid(string documento, int? id = null)
        {
            bool ret = true;

            if (proprietarioRepository.ValidaDocumento(documento, Convert.ToInt32(id)))
            {
                ModelState.AddModelError("Documento", "Já existe um proprietário com o mesmo número de Documento cadastrado!");
                ret = false;
            }

            return ret;

        }
    }
}