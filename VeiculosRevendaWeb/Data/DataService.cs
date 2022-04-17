using Microsoft.EntityFrameworkCore;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Interfaces;

namespace VeiculosRevendaWeb.Data
{
    class DataService : IDataService
    {
        private readonly ApplicationContext context;

        public DataService(ApplicationContext context)
        {
            this.context = context;
        }

        public void InicializaDB()
        {
            context.Database.Migrate();
        }
    }
}
