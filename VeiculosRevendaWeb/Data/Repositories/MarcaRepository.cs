using System;
using System.Collections.Generic;
using System.Linq;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Data.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationContext context;

        public MarcaRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(Marca model)
        {
            context.Marcas.Add(model);
            context.SaveChanges();
        }

        public IList<Marca> GetMarcas()
        {
            return context.Marcas.ToList();
        }

        public Marca GetMarcaById(int id)
        {
            return context.Marcas.Find(id);
        }

        public void Update(Marca model)
        {
            context.Marcas.Update(model);
            context.SaveChanges();
        }

        public bool ValidaNomeMarca(string nome)
        {
            return context.Marcas.Where(m => m.Nome.ToLower() == nome.ToLower()).Any();
        }

        public void Delete(int? id)
        {
            var marca = GetMarcaById(Convert.ToInt32(id));
            marca.CodStatus = 0;

            context.Marcas.Update(marca);
            context.SaveChanges();
        }
    }
}
