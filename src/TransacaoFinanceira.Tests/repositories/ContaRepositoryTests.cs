using System;
using Xunit;
using TransacaoFinanceira.Repositories;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Tests.Repositories
{
    public class ContaRepositoryTests
    {
        private readonly ContaRepository _contaRepository;

        public ContaRepositoryTests()
        {
            _contaRepository = new ContaRepository();
        }

        [Fact]
        public void TestGetSaldo_ContaExistente()
        {
            // Arrange
            var contaSaldo = new ContaSaldo(938485762, 150);
            _contaRepository.Adicionar(contaSaldo);

            // Act
            var resultado = _contaRepository.GetSaldo(938485762);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(150, resultado.Saldo);
        }

        [Fact]
        public void TestGetSaldo_ContaNaoExistente()
        {
            // Act
            var resultado = _contaRepository.GetSaldo(1234567890);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public void TestAtualizarConta()
        {
            // Arrange
            var contaSaldo = new ContaSaldo(938485762, 150);
            _contaRepository.Adicionar(contaSaldo);

            var contaAtualizada = new ContaSaldo(938485762, 200);
            _contaRepository.Atualizar(contaAtualizada);

            // Act
            var resultado = _contaRepository.GetSaldo(938485762);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.Saldo);
        }
    }
}
