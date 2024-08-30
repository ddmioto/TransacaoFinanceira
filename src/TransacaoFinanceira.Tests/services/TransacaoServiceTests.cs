using System;
using Xunit;
using Moq;
using TransacaoFinanceira.Services;
using TransacaoFinanceira.Repositories;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Tests.Services
{
    public class TransacaoServiceTests
    {
        private readonly TransacaoService _transacaoService;
        private readonly Mock<IContaRepository> _mockContaRepository;

        public TransacaoServiceTests()
        {
            _mockContaRepository = new Mock<IContaRepository>();
            _transacaoService = new TransacaoService(_mockContaRepository.Object);
        }

        [Fact]
        public void TestTransferir_TransferenciaValida()
        {
            // Arrange
            var contaOrigem = new ContaSaldo(938485762, 150);
            var contaDestino = new ContaSaldo(2147483649, 0);

            _mockContaRepository.Setup(repo => repo.GetSaldo(938485762)).Returns(contaOrigem);
            _mockContaRepository.Setup(repo => repo.GetSaldo(2147483649)).Returns(contaDestino);

            var transacao = new Transacao(1, "09/09/2023 14:15:00", 938485762, 2147483649, 100);

            // Act
            _transacaoService.Transferir(transacao);

            // Assert
            Assert.Equal(50, contaOrigem.Saldo);
            Assert.Equal(100, contaDestino.Saldo);
        }

        [Fact]
        public void TestTransferir_TransferenciaInvalida()
        {
            // Arrange
            var contaOrigem = new ContaSaldo(938485762, 50);
            var contaDestino = new ContaSaldo(2147483649, 0);

            _mockContaRepository.Setup(repo => repo.GetSaldo(938485762)).Returns(contaOrigem);
            _mockContaRepository.Setup(repo => repo.GetSaldo(2147483649)).Returns(contaDestino);

            var transacao = new Transacao(1, "09/09/2023 14:15:00", 938485762, 2147483649, 100);

            // Act
            _transacaoService.Transferir(transacao);

            // Assert
            Assert.Equal(50, contaOrigem.Saldo); // Saldo não deveria mudar
            Assert.Equal(0, contaDestino.Saldo); // Saldo não deveria mudar
        }
    }
}
