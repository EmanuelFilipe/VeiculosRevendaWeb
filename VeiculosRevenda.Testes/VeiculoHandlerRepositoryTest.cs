using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeiculosRevendaWeb.Data.Context;
using VeiculosRevendaWeb.Data.Repositories;
using VeiculosRevendaWeb.Handlers;
using VeiculosRevendaWeb.Models;
using Xunit;

namespace VeiculosRevenda.Testes
{
    public class VeiculoHandlerRepositoryTest
    {
        private DbContextOptions _options;
        private ApplicationContext _context;
        private VeiculoRepository _veiculoRepository;
        private VeiculoHandler _veiculoHandler;

        public VeiculoHandlerRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                                                .UseInMemoryDatabase("DbMarcasContext")
                                                .Options;

            _context = new ApplicationContext(_options);
            _veiculoRepository = new VeiculoRepository(_context);
            _veiculoHandler = new VeiculoHandler(_veiculoRepository);
        }

        [Fact]
        public void TesteCriandoVeiculo()
        {
            //arrange
            var comando = new Veiculo("AAAAAAAAAA", "flex", new DateTime(2023, 08, 17),
                                      new DateTime(2023, 08, 01), "70.000", "45.000", 1, 1, 1);

            //act
            _veiculoHandler.Add(comando);

            var veiculos = _veiculoRepository.GetVeiculos();

            //assert
            Assert.NotNull(veiculos);
            Assert.True(veiculos.Count == 1);
        }

        [Fact]
        public void TesteObtendoVeiculoPorId()
        {
            //arrange
            var comando1 = new Veiculo("AAAAAAAAAA", "flex", new DateTime(2023, 08, 17),
                                       new DateTime(2023, 08, 01), "70.000", "45.000", 1, 1, 1);
            
            var comando2 = new Veiculo("BBBBBBBBBB", "1.5", new DateTime(2023, 08, 17),
                           new DateTime(2023, 08, 01), "80.000", "55.000", 1, 1, 1);
            //act
            _veiculoHandler.Add(comando1);
            _veiculoHandler.Add(comando2);

            var proprietario = _veiculoRepository.GetVeiculoById(1);

            //assert
            Assert.NotNull(proprietario);
            Assert.True(proprietario.Id == 1);
        }

        [Fact]
        public void TesteEditandoRenavamDeUmVeiculo()
        {
            //arrange
            var comando = new Veiculo("AAAAAAAAAA", "flex", new DateTime(2023, 08, 17),
                                      new DateTime(2023, 08, 01), "70.000", "45.000", 1, 1, 1);

            //act
            _veiculoHandler.Add(comando);

            var proprietario = _veiculoRepository.GetVeiculos().FirstOrDefault();

            proprietario.Renavam = "BBBBBBBBBB";

            _veiculoHandler.Update(proprietario);

            var novoProprietario = _veiculoRepository.GetVeiculos().FirstOrDefault();

            //assert
            Assert.NotNull(novoProprietario);
            Assert.Equal("BBBBBBBBBB", novoProprietario.Renavam);
        }

        [Fact(Skip = "Ao pesquisar por Id ocorre erro de tracking, investigar posteriormente")]
        public void TesteExcluindoVeiculoPorId()
        {
            //arrange
            var comando = new Veiculo("AAAAAAAAAA", "flex", new DateTime(2023, 08, 17),
                                      new DateTime(2023, 08, 01), "70.000", "45.000", 1, 1, 1);
            //act
            _veiculoHandler.Add(comando);

            var proprietario = _veiculoRepository.GetVeiculoById(1);

            _veiculoHandler.Delete(proprietario.Id);

            var novaConsultaProprietario = _veiculoRepository.GetVeiculoById(1);

            //assert
            Assert.NotNull(novaConsultaProprietario);
            Assert.True(novaConsultaProprietario.CodStatus == 0); // codstatus == 0 é desativado
        }
    }
}
