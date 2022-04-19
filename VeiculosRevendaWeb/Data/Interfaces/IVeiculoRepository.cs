using System.Collections.Generic;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Data.Interfaces
{
    public interface IVeiculoRepository
    {
        IList<Veiculo> GetVeiculos();
        Veiculo GetVeiculoById(int id);
        void Add(Veiculo model);
        void Update(Veiculo model);
        void Delete(int? id);
        bool ValidaRenavam(string renavam, int id);
        bool ValidaAlteracaoStatusIncorreto(int statusAtual, int id);
        bool GetMarcaIdInVeiculos(int id);
        bool GetProprietarioIdInVeiculos(int id);
    }
}
