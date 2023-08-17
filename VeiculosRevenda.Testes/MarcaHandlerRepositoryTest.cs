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
        public DbContextOptions _options;
        private ApplicationContext _context;
        private MarcaRepository _marcaRepository;
        private MarcaHandler _marcaHandler;

        public MarcaHandlerRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                                    .UseInMemoryDatabase("DbMarcasContext")
                                    .Options;

            _context = new ApplicationContext(_options);
            _marcaRepository = new MarcaRepository(_context);
            _marcaHandler = new MarcaHandler(_marcaRepository);
        }

        [Fact]
        public void TesteCriandoMarca()
        {
            //arrange
            var comando = new Marca("Fiat", 1);

            //act
            _marcaHandler.Add(comando);

            var marcas = _marcaRepository.GetMarcas();

            //assert
            Assert.NotNull(marcas);
            Assert.True(marcas.Count == 1);
        }

        [Fact]
        public void TesteObtendoMarcaPorId()
        {
            //arrange
            var comando1 = new Marca("Fiat", 1);
            var comando2 = new Marca("Ford", 1);

            //act
            _marcaHandler.Add(comando1);
            _marcaHandler.Add(comando2);

            var marca = _marcaRepository.GetMarcaById(1);

            //assert
            Assert.NotNull(marca);
            Assert.True(marca.Id == 1);
        }

        [Fact]
        public void TesteEditandoNomeMarca()
        {
            //arrange
            var comando = new Marca("Fiat", 1);

            //act
            _marcaHandler.Add(comando);

            var marca = _marcaRepository.GetMarcas().FirstOrDefault();

            marca.Nome = "Volks";

            _marcaHandler.Update(marca);

            marca = _marcaRepository.GetMarcas().FirstOrDefault();

            //assert
            Assert.NotNull(marca);
            Assert.Equal("Volks", marca.Nome);
        }

        [Fact]
        public void TesteExcluindoMarcaPorId()
        {
            //arrange
            var comando = new Marca("Fiat", 1);

            //act
            _marcaHandler.Add(comando);

            var marca = _marcaRepository.GetMarcaById(1);

            _marcaHandler.Delete(marca.Id);

            var novaConsultaMarca = _marcaRepository.GetMarcaById(1);

            //assert
            Assert.NotNull(novaConsultaMarca);
            Assert.True(novaConsultaMarca.CodStatus == 0); // codstatus == 0 é desativado
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
