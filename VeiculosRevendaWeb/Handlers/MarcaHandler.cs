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

        public void Update(Marca model)
        {
            _marcaRepository.Update(model);
        }


        public void Delete(int id)
        {
            //var marca = GetMarcaById(id);
            _marcaRepository.Delete(id);
        }

        public IList<Marca> GetMarcas()
        {
            return _marcaRepository.GetMarcas();
        }

        public Marca GetMarcaById(int id)
        {
            var marcas = GetMarcas();
            return marcas.Where(m => m.Id == id).FirstOrDefault();
        }

    }
}
