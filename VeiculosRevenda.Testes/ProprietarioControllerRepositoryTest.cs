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
    public class ProprietarioControllerRepositoryTest
    {
        public DbContextOptions options;
        public ApplicationContext context;
        public VeiculoRepository veiculoRepo;
        public ProprietarioRepository proprietarioRepo;

        public ProprietarioControllerRepositoryTest()
        {
            options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ApplicationContext>()
                       .UseInMemoryDatabase("DbMarcasContext")
                       .Options;

            context = new ApplicationContext(options);
            veiculoRepo = new VeiculoRepository(context);
            proprietarioRepo = new ProprietarioRepository(context);
        }

        [Fact]
        public void ObtendoTodosOsProprietarios()
        {
            //arrange
            Proprietario proprietario = new Proprietario("Filipe Silva", "07553474630", "teste@teste.com", "avenida machado de assis", 1);
            context.Proprietarios.Add(proprietario);
            context.SaveChanges();

            var controller = new ProprietarioController(proprietarioRepo, veiculoRepo);

            //act
            var retorno = controller.Index() as ViewResult;
            var lista = retorno.Model as List<Proprietario>;

            //assert
            Assert.NotNull(lista);
            Assert.Equal(1, (int)lista.Count());
        }

        [Fact]
        public void IncluindoProprietario()
        {
            //arrange
            Proprietario proprietario = new Proprietario("Filipe Silva", "07553474630", "teste@teste.com", "avenida machado de assis", 1);

            var controller = new ProprietarioController(proprietarioRepo, veiculoRepo);

            //act
            controller.Create(proprietario);

            var retorno = controller.Index() as ViewResult;
            var lista = retorno.Model as List<Proprietario>;

            //assert
            Assert.NotNull(lista);
            Assert.Equal(1, (int)lista.Count());
        }

        [Fact]
        public void ObtendoProprietarioPorId()
        {
            //arrange
            Proprietario proprietario = new Proprietario("Filipe Silva", "07553474630", "teste@teste.com", "avenida machado de assis", 1);

            var controller = new ProprietarioController(proprietarioRepo, veiculoRepo);
            controller.Create(proprietario);

            var retorno = controller.Index() as ViewResult;
            var lista = retorno.Model as List<Proprietario>;

            //act
            var result = controller.Detail(lista.FirstOrDefault().Id) as ViewResult;
            var prop = result.Model;

            //assert
            Assert.NotNull(prop);
        }

        [Fact]
        public void EditandoProprietario()
        {
            //arrange
            Proprietario proprietario = new Proprietario("Filipe Silva", "07553474630", "teste@teste.com", "avenida machado de assis", 1);

            var controller = new ProprietarioController(proprietarioRepo, veiculoRepo);
            controller.Create(proprietario);

            var retorno = controller.Index() as ViewResult;
            var lista = retorno.Model as List<Proprietario>;

            //act
            var prop = lista.FirstOrDefault();

            prop.Nome = "Emanuel Filipe";

            var retEdit = controller.Edit(prop);
            retorno = controller.Index() as ViewResult;
            lista = retorno.Model as List<Proprietario>;

            //assert
            Assert.Equal("Emanuel Filipe", lista.FirstOrDefault().Nome);
        }

        [Fact]
        public void ExcluindoProprietario()
        {
            //arrange
            Proprietario proprietario = new Proprietario("Filipe Silva", "07553474630", "teste@teste.com", "avenida machado de assis", 1);

            var controller = new ProprietarioController(proprietarioRepo, veiculoRepo);
            controller.Create(proprietario);

            var ret = controller.Index() as ViewResult;
            var lista = ret.Model as List<Proprietario>;

            //act
            controller.Delete(lista.FirstOrDefault().Id);
            var result = controller.Detail(lista.FirstOrDefault().Id) as ViewResult;
            var prop = result.Model as Proprietario;

            //assert
            Assert.Equal(0, prop.CodStatus);
        }
    }
}
