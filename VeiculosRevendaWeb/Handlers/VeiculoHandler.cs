using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Handlers
{
    public class VeiculoHandler
    {
        IVeiculoRepository _veiculoRepository;

        public VeiculoHandler(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public void Add(Veiculo model)
        {
            var marca = new Veiculo(model.Renavam, model.Modelo, model.AnoFabricacao, model.AnoModelo, 
                                    model.Quilometragem, model.Valor, model.CodStatus, 
                                    model.proprietarioId, model.marcaId);

            _veiculoRepository.Add(marca);
        }

        public void Update(Veiculo model)
        {
            _veiculoRepository.Update(model);
        }

        public void Delete(int id)
        {
            _veiculoRepository.Delete(id);
        }
       
    }
}
