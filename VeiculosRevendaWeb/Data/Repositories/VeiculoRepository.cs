using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Data.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly ApplicationContext context;


        public VeiculoRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public VeiculoRepository()
        {

        }

        public IList<Veiculo> GetVeiculos()
        {
            List<Veiculo> veiculos = context.Veiculos.ToList();
            return veiculos;
        }

        public Veiculo GetVeiculoById(int id)
        {
            return context.Veiculos.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            //return veiculo.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(Veiculo model)
        {
            context.Veiculos.Add(model);
            context.SaveChanges();
        }

        public void Update(Veiculo model)
        {
            context.Veiculos.Update(model);
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var veiculo = GetVeiculoById((int)id);
            veiculo.CodStatus = 2;

            context.Veiculos.Update(veiculo);
            context.SaveChanges();
        }

        public bool ValidaRenavam(string renavam, int id)
        {
            if (id > 0)
                return context.Veiculos.Where(v => v.Id != id && v.Renavam == renavam).Any();
            else
                return context.Veiculos.Where(v => v.Renavam == renavam).Any();
        }

        public bool ValidaAlteracaoStatus(int statusAtual, int id)
        {
            bool ret = false;
            var statusBanco = GetVeiculoById(id).CodStatus;

            // Disponível = 1 || Indisponível = 2 || Vendido = 3 
            if (statusBanco == 2 || statusBanco == 3)
            {
                if (statusAtual == 1)
                    ret = true;
            }

            return ret;
        }

        public bool GetMarcaIdInVeiculos(int id)
        {
            return GetVeiculos().Where(v => v.marcaId == id).Any();
        }

        public bool GetProprietarioIdInVeiculos(int id)
        {
            return GetVeiculos().Where(v => v.proprietarioId == id).Any();
        }
    }
}
