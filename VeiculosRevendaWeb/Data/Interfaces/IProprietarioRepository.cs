using System.Collections.Generic;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Data.Interfaces
{
    public interface IProprietarioRepository
    {
        IList<Proprietario> GetProprietarios();
        bool ValidaDocumento(string nome, int id);
        void Add(Proprietario model);
        Proprietario GetProprietarioById(int id);
        void Update(Proprietario model);
        void Delete(int? id);
    }
}
