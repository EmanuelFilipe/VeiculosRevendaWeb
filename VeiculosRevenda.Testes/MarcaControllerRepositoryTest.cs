using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VeiculosRevendaWeb.Controllers;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Repositories;
using VeiculosRevendaWeb.Models;
using Xunit;

namespace VeiculosRevenda.Testes
{
    public class MarcaControllerRepositoryTest
    {
        public DbContextOptions options;
        public ApplicationContext context;
        public MarcaRepository marcaRepo;
        public VeiculoRepository veiculoRepo;

        public MarcaControllerRepositoryTest()
        {
            options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ApplicationContext>()
                        .UseInMemoryDatabase("DbMarcasContext")
                        .Options;

            context = new ApplicationContext(options);
            marcaRepo = new MarcaRepository(context);
            veiculoRepo = new VeiculoRepository(context);
        }

        [Fact]
        public void ObtendoListaDeMarcas()
        {
            //arrange
            context.Marcas.Add(new Marca("hyundai", 1));
            context.SaveChanges();

            var controller = new MarcaController(marcaRepo, veiculoRepo);

            //act
            var retorno = controller.Index() as ViewResult;
            var lista = retorno.Model as List<Marca>;
            //var retorno = ((ViewResult)controller.Index()).Model;

            //assert
            Assert.NotNull(retorno);
            Assert.True(lista.Count > 0);
        }

        [Fact]
        public void IncluindoMarca()
        {
            //arrange
            var model = new Marca("hyundai", 1);

            var controller = new MarcaController(marcaRepo, veiculoRepo);

            //act
            var retorno = controller.Create(model);

            var ret = controller.Index() as ViewResult;
            var lista = ret.Model as List<Marca>;

            //assert
            Assert.NotNull(retorno);
            Assert.True(lista.Count > 0);
        }

        [Fact]
        public void ObtendoMarcaPorId()
        {
            //arrange
            var controller = new MarcaController(marcaRepo, veiculoRepo);

            var model = new Marca("hyundai", 1);
            controller.Create(model);

            var ret = controller.Index() as ViewResult;
            var lista = ret.Model as List<Marca>;

            //act
            var result = controller.Detail(lista.FirstOrDefault().Id) as ViewResult;
            var marca = result.Model;

            //assert
            Assert.NotNull(marca);
        }

        [Fact]
        public void EditandoMarca()
        {
            //arrange
            var controller = new MarcaController(marcaRepo, veiculoRepo);

            var model = new Marca("hyundai", 1);
            controller.Create(model);

            var ret = controller.Index() as ViewResult;
            var lista = ret.Model as List<Marca>;
            var marca = lista.FirstOrDefault();

            //act
            marca.Nome = "Fiat";

            var retEdit = controller.Edit(marca);
            ret = controller.Index() as ViewResult;
            lista = ret.Model as List<Marca>;

            //assert
            Assert.Equal("Fiat", lista.FirstOrDefault().Nome);
        }

        [Fact]
        public void ExcluindoMarca()
        {
            //arrange
            var model = new Marca("Hyundai", 1);

            var controller = new MarcaController(marcaRepo, veiculoRepo);
            controller.Create(model);

            var ret = controller.Index() as ViewResult;
            var lista = ret.Model as List<Marca>;

            //act
            controller.Delete(lista.FirstOrDefault().Id);
            var result = controller.Detail(lista.FirstOrDefault().Id) as ViewResult;
            var marca = result.Model as Marca;

            //assert
            Assert.Equal(0, marca.CodStatus);
        }
    }
}
