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
    public class ProprietarioHandlerRepositoryTest
    {
        private DbContextOptions _options;
        private ApplicationContext _context;
        private ProprietarioRepository _proprietarioRepository;
        private ProprietarioHandler _proprietarioHandler;

        public ProprietarioHandlerRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                                                .UseInMemoryDatabase("DbMarcasContext")
                                                .Options;

            _context = new ApplicationContext(_options);
            _proprietarioRepository = new ProprietarioRepository(_context);
            _proprietarioHandler = new ProprietarioHandler(_proprietarioRepository);
        }

        [Fact]
        public void TesteCriandoProprietario()
        {
            //arrange
            var comando = new Proprietario("Filipe teste", "07553474630", "filipe@gmail.com", "av machado de assis", 1);

            //act
            _proprietarioHandler.Add(comando);

            var proprietarios = _proprietarioRepository.GetProprietarios();

            //assert
            Assert.NotNull(proprietarios);
            Assert.True(proprietarios.Count == 1);
        }

        [Fact]
        public void TesteObtendoProprietarioPorId()
        {
            //arrange
            var comando1 = new Proprietario("Filipe teste", "07553474630", "filipe@gmail.com", "av machado de assis", 1);
            var comando2 = new Proprietario("Gustavo teste", "1234567890", "gustavo@teste.com", "rua treze", 1);

            //act
            _proprietarioHandler.Add(comando1);
            _proprietarioHandler.Add(comando2);

            var proprietario = _proprietarioRepository.GetProprietarioById(1);

            //assert
            Assert.NotNull(proprietario);
            Assert.True(proprietario.Id == 1);
        }

        [Fact]
        public void TesteEditandoNomeDeUmProprietario()
        {
            //arrange
            var comando = new Proprietario("Filipe teste", "07553474630", "filipe@gmail.com", "av machado de assis", 1);

            //act
            _proprietarioHandler.Add(comando);

            var proprietario = _proprietarioRepository.GetProprietarios().FirstOrDefault();

            proprietario.Nome = "Gustavo Teste";

            _proprietarioHandler.Update(proprietario);

            var novoProprietario = _proprietarioRepository.GetProprietarios().FirstOrDefault();

            //assert
            Assert.NotNull(novoProprietario);
            Assert.Equal("Gustavo Teste", novoProprietario.Nome);
        }

        [Fact]
        public void TesteExcluindoMarcaPorId()
        {
            //arrange
            var comando1 = new Proprietario("Filipe teste", "07553474630", "filipe@gmail.com", "av machado de assis", 1);
            
            //act
            _proprietarioHandler.Add(comando1);

            var proprietario = _proprietarioRepository.GetProprietarioById(1);

            _proprietarioHandler.Delete(proprietario.Id);

            var novaConsultaProprietario = _proprietarioRepository.GetProprietarioById(1);

            //assert
            Assert.NotNull(novaConsultaProprietario);
            Assert.True(novaConsultaProprietario.CodStatus == 0); // codstatus == 0 é desativado
        }
    }
}
