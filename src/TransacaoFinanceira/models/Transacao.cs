using System;

namespace TransacaoFinanceira.Models
{
    public class Transacao
    {
        public int CorrelationId { get; }
        public ulong ContaOrigem { get; }
        public ulong ContaDestino { get; }
        public decimal Valor { get; }
        public DateTime DataHora { get; }

        public Transacao(int correlationId, string dataHora, ulong contaOrigem, ulong contaDestino, decimal valor)
        {
            CorrelationId = correlationId;
            DataHora = DateTime.Parse(dataHora);
            ContaOrigem = contaOrigem;
            ContaDestino = contaDestino;
            Valor = valor;
        }
    }
}