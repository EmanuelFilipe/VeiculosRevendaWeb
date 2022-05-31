using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevenda.Testes
{
    class MarcaRepositorioFake : IMarcaRepository
    {
        List<Marca> marcas = new List<Marca>();

        public void Add(Marca model)
        {
            marcas.Add(model);
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Marca GetMarcaById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Marca> GetMarcas()
        {
            return marcas;
        }

        public void Update(Marca model)
        {
            throw new NotImplementedException();
        }

        public bool ValidaNomeMarca(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
