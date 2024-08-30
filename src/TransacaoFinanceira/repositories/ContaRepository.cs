using System;
using System.Collections.Generic;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Repositories
{
    public class ContaRepository
    {
        private readonly List<ContaSaldo> _tabelaSaldos;

        public ContaRepository()
        {
            _tabelaSaldos = new List<ContaSaldo>
            {
                new ContaSaldo(938485762, 180),
                new ContaSaldo(347586970, 1200),
                new ContaSaldo(2147483649, 0),
                new ContaSaldo(675869708, 4900),
                new ContaSaldo(238596054, 478),
                new ContaSaldo(573659065, 787),
                new ContaSaldo(210385733, 10),
                new ContaSaldo(674038564, 400),
                new ContaSaldo(563856300, 1200)
            };
        }

        public ContaSaldo GetSaldo(ulong contaId)
        {
            return _tabelaSaldos.Find(x => x.ContaId == contaId);
        }

        public bool AtualizarSaldo(ContaSaldo contaSaldo)
        {
            try
            {
                _tabelaSaldos.RemoveAll(x => x.ContaId == contaSaldo.ContaId);
                _tabelaSaldos.Add(contaSaldo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao atualizar saldo: {e.Message}");
                return false;
            }
        }
    }
}
