using System.Collections.Generic;
using VeiculosRevendaWeb.Models;

namespace VeiculosRevendaWeb.Data.Interfaces
{
    public interface IMarcaRepository
    {
        IList<Marca> GetMarcas();
        void Add(Marca model);
        bool ValidaNomeMarca(string nome);
        Marca GetMarcaById(int id);
        void Update(Marca model);
        void Delete(int? id);
    }
}