using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Handlers
{
    public class ProprietarioHandler
    {
        IProprietarioRepository _proprietarioRepository;

        public ProprietarioHandler(IProprietarioRepository proprietarioRepository)
        {
            _proprietarioRepository = proprietarioRepository;
        }

        public void Add(Proprietario model)
        {
            var marca = new Proprietario(model.Nome, model.Documento, model.Email, model.Endereco, model.CodStatus);
            _proprietarioRepository.Add(marca);
        }

        public void Update(Proprietario model)
        {
            _proprietarioRepository.Update(model);
        }

        public void Delete(int id)
        {
            _proprietarioRepository.Delete(id);
        }
       
    }
}
