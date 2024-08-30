using System;

namespace TransacaoFinanceira.Models
{
    public class ContaSaldo
    {
        public ulong ContaId { get; }
        public decimal Saldo { get; set; }

        public ContaSaldo(ulong contaId, decimal saldo)
        {
            ContaId = contaId;
            Saldo = saldo;
        }
    }
}
