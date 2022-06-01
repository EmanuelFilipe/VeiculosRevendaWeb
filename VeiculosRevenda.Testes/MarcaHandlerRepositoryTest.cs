using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Interfaces;
using VeiculosRevendaWeb.Data.Repositories;
using VeiculosRevendaWeb.Handlers;
using VeiculosRevendaWeb.Models;
using Xunit;

namespace VeiculosRevenda.Testes
{
    public class MarcaHandlerRepositoryTest
    {
        //private readonly IVeiculoRepository _marcaRepository;

        //public MarcaRepositoryTest()
        //{
        //    var servico = new ServiceCollection();
        //    servico.AddTransient<IVeiculoRepository, VeiculoRepository>();
        //    var provedor = servico.BuildServiceProvider();
        //    _marcaRepository = provedor.GetService<IVeiculoRepository>();
        //}

        public DbContextOptions options;

        public MarcaHandlerRepositoryTest()
        {
            options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ApplicationContext>()
                                    .UseInMemoryDatabase("DbMarcasContext")
                                    .Options;
        }

        [Fact]
        public void TesteCriandoMarca()
        {
            //arrange
            var comando = new Marca("Fiat", 1);
            var context = new ApplicationContext(options);
            var repo = new MarcaRepository(context);
            var handler = new MarcaHandler(repo);

            //act
            handler.Add(comando);

            var marcas = repo.GetMarcas();

            //assert
            Assert.NotNull(marcas);
        }

        [Fact]
        public void TesteObtendoMarcaPorId()
        {
            //arrange
            var comando1 = new Marca("Fiat", 1);
            var comando2 = new Marca("Ford", 1);
            var context = new ApplicationContext(options);
            var repo = new MarcaRepository(context);
            var handler = new MarcaHandler(repo);

            //act
            handler.Add(comando1);
            handler.Add(comando2);

            var marca = repo.GetMarcaById(1);

            //assert
            Assert.NotNull(marca);
        }

        [Fact]
        public void TesteEditandoNomeMarca()
        {
            //arrange
            var comando = new Marca("Fiat", 1);
            var context = new ApplicationContext(options);
            var repo = new MarcaRepository(context);
            var handler = new MarcaHandler(repo);

            //act
            handler.Add(comando);

            var marca = repo.GetMarcas().FirstOrDefault();

            marca.Nome = "Volks";

            handler.Update(marca);

            marca = repo.GetMarcas().FirstOrDefault();

            //assert
            Assert.Equal("Volks", marca.Nome);
        }

        [Fact]
        public void TesteExcluindoMarcaPorId()
        {
            //arrange
            var comando1 = new Marca("Fiat", 1);
            var comando2 = new Marca("Ford", 1);
            var context = new ApplicationContext(options);
            var repo = new MarcaRepository(context);
            var handler = new MarcaHandler(repo);

            //act
            handler.Add(comando1);
            handler.Add(comando2);

            var marca = repo.GetMarcaById(1);

            handler.Delete(marca.Id);

            marca = repo.GetMarcaById(1);

            //assert
            Assert.True(marca.CodStatus == 0); // codstatus == 0 é desativado
        }

        // NÃO FUNCIONOU
        [Fact(Skip = "Teste com mock nao funcionou nessa arquitetura específica")]
        public void TesteWithMock()
        {
            //arrange
            var comando = new Marca("Fiat", 1);

            var mock = new Mock<IMarcaRepository>();
            mock.Setup(r => r.Add(comando));
            var repo = mock.Object;

            //var context = new ApplicationContext(options);
            //var repo = new MarcaRepository(context);
            var handler = new MarcaHandler(repo);

            //act
            handler.Add(comando);

            var marcas = repo.GetMarcas();

            //assert
            Assert.NotNull(marcas);
        }

        
    }
}
