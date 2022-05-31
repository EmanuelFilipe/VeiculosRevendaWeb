using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Handlers
{
    public class MarcaHandler
    {
        IMarcaRepository _marcaRepository;

        public MarcaHandler(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public void Add(Marca model)
        {
            var marca = new Marca(model.Nome, model.CodStatus);
            _marcaRepository.Add(marca);
        }

        public IList<Marca> GetMarcas()
        {
            return _marcaRepository.GetMarcas();
        }
    }
}
