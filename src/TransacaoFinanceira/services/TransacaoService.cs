using System;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Repositories;

namespace TransacaoFinanceira.Services
{
    public class TransacaoService
    {
        private readonly ContaRepository _contaRepository;

        public TransacaoService(ContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public void Transferir(Transacao transacao)
        {
            var contaSaldoOrigem = _contaRepository.GetSaldo(transacao.ContaOrigem);
            if (contaSaldoOrigem.Saldo < transacao.Valor)
            {
                Console.WriteLine("Transacao numero {0} foi cancelada por falta de saldo", transacao.CorrelationId);
            }
            else
            {
                var contaSaldoDestino = _contaRepository.GetSaldo(transacao.ContaDestino);
                contaSaldoOrigem.Saldo -= transacao.Valor;
                contaSaldoDestino.Saldo += transacao.Valor;

                _contaRepository.AtualizarSaldo(contaSaldoOrigem);
                _contaRepository.AtualizarSaldo(contaSaldoDestino);

                Console.WriteLine("Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem: {1} | Conta Destino: {2}",
                    transacao.CorrelationId, contaSaldoOrigem.Saldo, contaSaldoDestino.Saldo);
            }
        }
    }
}
