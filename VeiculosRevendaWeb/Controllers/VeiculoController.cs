using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RabbitMQ.Client;
using VeiculosRevendaWeb.Data.DTO;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;
using VeiculosRevendaWeb.RabbitMQSender;

namespace VeiculosRevendaWeb.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoRepository veiculoRepository;
        private readonly IMarcaRepository marcaRepository;
        private readonly IProprietarioRepository proprietarioRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public VeiculoController(IVeiculoRepository veiculoRepository, 
            IMarcaRepository marcaRepository,
            IProprietarioRepository proprietarioRepository,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            this.veiculoRepository = veiculoRepository;
            this.marcaRepository = marcaRepository;
            this.proprietarioRepository = proprietarioRepository;
            this._rabbitMQMessageSender = rabbitMQMessageSender;
        }

        public IActionResult Index()
        {
            var model = veiculoRepository.GetVeiculos();
            return View(model);
        }

        public IActionResult Create()
        {
            GetListas();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Veiculo model)
        {
            if (CustomValidationIsValid(model))
            {
                veiculoRepository.Add(model);
                TempData["MSG_SUCCESS"] = "Veículo criado com sucesso!";
                GetProprietario(ref model, Convert.ToInt32(model.proprietarioId));
                VeiculoDTO dto = VeiculoMapper(model);

                _rabbitMQMessageSender.SendMessage(dto, "veiculoQueue");

                return RedirectToAction(nameof(Index));
            }

            GetListas();
            return View(model);
        }

        private void GetProprietario(ref Veiculo model, int proprietarioId)
        {
            model.Proprietario = proprietarioRepository.GetProprietarioById(proprietarioId);
        }

        public IActionResult Detail(int id)
        {
            GetListas();
            return View(veiculoRepository.GetVeiculoById(id));
        }

        public IActionResult Edit(int id)
        {
            GetListas();
            return View(veiculoRepository.GetVeiculoById(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Veiculo model)
        {
            if (CustomValidationIsValid(model))
            {
                veiculoRepository.Update(model);
                TempData["MSG_SUCCESS"] = "Veículo alterado com sucesso!";
                GetProprietario(ref model, Convert.ToInt32(model.proprietarioId));
                VeiculoDTO dto = VeiculoMapper(model);

                _rabbitMQMessageSender.SendMessage(dto, "veiculoQueue");

                return RedirectToAction(nameof(Index));
            }

            GetListas();
            return View(model);
        }

        private VeiculoDTO VeiculoMapper(Veiculo model)
        {
            return new VeiculoDTO
            {
                Id = model.Id,
                Renavam = model.Renavam,
                Modelo = model.Modelo,
                AnoFabricacao = model.AnoFabricacao,
                AnoModelo = model.AnoModelo,
                Quilometragem = model.Quilometragem,
                Valor = model.Valor,
                CodStatus = model.CodStatus,
                proprietarioId = model.proprietarioId,
                Proprietario = model.Proprietario,
                marcaId = model.marcaId
            };
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            veiculoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomValidationIsValid(Veiculo model)
        {
            bool ret = true;

            if (model.AnoFabricacao == null)
                ModelState.AddModelError("AnoFabricacao", "O campo Ano Fabricação é obrigatório");

            if (model.AnoModelo == null)
                ModelState.AddModelError("AnoModelo", "O campo Ano Modelo é obrigatório");

            if (model.marcaId == null)
                ModelState.AddModelError("marcaId", "O campo Marca é obrigatório");

            if (model.proprietarioId == null)
                ModelState.AddModelError("proprietarioId", "O campo Proprietário é obrigatório");

            int countErros = ModelState.Values.Count(x => x.Errors.Count > 0);

            if (countErros > 0)
            {
                ret = false;
            }
            else if (veiculoRepository.ValidaRenavam(model.Renavam, Convert.ToInt32(model.Id)))
            {
                ModelState.AddModelError("Renavam", "Já existe este RENAVAM cadastrado!");
                ret = false;
            }
            else if (model.Id > 0 && veiculoRepository.ValidaAlteracaoStatusIncorreto(Convert.ToInt32(model.CodStatus), model.Id))
            {
                ModelState.AddModelError("CodStatus", "Não é possível alterar o Status para Disponível!");
                ret = false;
            }

            return ret;
        }

        private void GetListas()
        {
            var marcas = marcaRepository.GetMarcas().Where(m => m.CodStatus == 1).ToList();
            ViewBag.ListaMarcasSelectList = new SelectList(marcas, "Id", "Nome");

            var proprietarios = proprietarioRepository.GetProprietarios().Where(m => m.CodStatus == 1).ToList();
            ViewBag.ListaProprietariosSelectList = new SelectList(proprietarios, "Id", "Nome");
        }


        private void SendVeiculoToQueue(Veiculo veiculo)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    {
                        channel.QueueDeclare(
                            queue: "veiculoQueue",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );

                        var strinfiedMessage = JsonConvert.SerializeObject(veiculo);
                        var body = Encoding.UTF8.GetBytes(strinfiedMessage);

                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "veiculoQueue",
                            basicProperties: null,
                            body: body
                        );
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}