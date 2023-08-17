using System.Collections.Generic;
using System.Linq;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Data.Repositories
{
    public class ProprietarioRepository : IProprietarioRepository
    {
        private readonly ApplicationContext context;

        public ProprietarioRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IList<Proprietario> GetProprietarios()
        {
            return context.Proprietarios.ToList();
        }

        public bool ValidaDocumento(string documento, int id)
        {
            if (id > 0)
                return context.Proprietarios.Where(m => m.Id != id && m.Documento.ToLower() == documento.ToLower()).Any();
            else
                return context.Proprietarios.Where(m => m.Documento.ToLower() == documento.ToLower()).Any();
        }

        public void Add(Proprietario model)
        {
            context.Proprietarios.Add(model);
            context.SaveChanges();
        }

        public Proprietario GetProprietarioById(int id)
        {
            return context.Proprietarios.Find(id);
        }

        public void Update(Proprietario model)
        {
            var proprietario = GetProprietarioById(model.Id);
            proprietario.Nome = model.Nome;
            proprietario.Email = model.Email;
            proprietario.Endereco = model.Endereco;
            proprietario.CodStatus = model.CodStatus;
            proprietario.Documento = model.Documento;

            context.Proprietarios.Update(proprietario);
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var proprietario = GetProprietarioById((int)id);
            proprietario.CodStatus = 0;

            context.Proprietarios.Update(proprietario);
            context.SaveChanges();
        }
    }
}
