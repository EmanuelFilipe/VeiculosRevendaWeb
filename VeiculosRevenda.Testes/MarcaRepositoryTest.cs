using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Data.Repositories;
using VeiculosRevendaWeb.Handlers;
using VeiculosRevendaWeb.Models;
using Xunit;
using Xunit.Abstractions;

namespace VeiculosRevenda.Testes
{
    public class MarcaRepositoryTest
    {
        //private readonly IVeiculoRepository _marcaRepository;

        //public MarcaRepositoryTest()
        //{
        //    var servico = new ServiceCollection();
        //    servico.AddTransient<IVeiculoRepository, VeiculoRepository>();
        //    var provedor = servico.BuildServiceProvider();
        //    _marcaRepository = provedor.GetService<IVeiculoRepository>();
        //}

        [Fact]
        public void TesteInMemory()
        {
            var comando = new Marca("Fiat", 1);

            var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("DbMarcasContext")
                .Options;

            var context = new ApplicationContext(options);
            var repo = new MarcaRepository(context);

            var handler = new MarcaHandler(repo);

            handler.Add(comando);

            var marcas = repo.GetMarcas();

            Assert.NotNull(marcas);
        }

    }
}
