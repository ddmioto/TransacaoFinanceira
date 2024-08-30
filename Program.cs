using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransacaoFinanceira
{
    class Program
    {

        static void Main(string[] args)
        {
            var transacoes = new Transacao[] {
                new Transacao(1,"09/09/2023 14:15:00", 938485762, 2147483649, 150),
                new Transacao(2,"09/09/2023 14:15:05", 2147483649, 210385733, 149),
                new Transacao(3,"09/09/2023 14:15:29", 347586970, 238596054, 1100),
                new Transacao(4,"09/09/2023 14:17:00", 675869708, 210385733, 5300),
                new Transacao(5,"09/09/2023 14:18:00", 238596054, 674038564, 1489),
                new Transacao(6,"09/09/2023 14:18:20", 573659065, 563856300, 49),
                new Transacao(7,"09/09/2023 14:19:00", 938485762, 2147483649, 44),
                new Transacao(8,"09/09/2023 14:19:01", 573659065, 675869708, 150)
            };
            var executor = new executarTransacaoFinanceira();

            foreach (var item in transacoes)
            {
                executor.transferir(item.CorrelationId, item.ContaOrigem, item.ContaDestino, item.Valor);
            }
        }
    }

    public class Transacao
    {
        public int CorrelationId { get; set; }
        public string Datetime { get; set; }
        public ulong ContaOrigem { get; set; }
        public ulong ContaDestino { get; set; }
        public decimal Valor { get; set; }

        // Construtor
        public Transacao(int correlationId, string datetime, ulong contaOrigem, ulong contaDestino, decimal valor)
        {
            CorrelationId = correlationId;
            Datetime = datetime;
            ContaOrigem = contaOrigem;
            ContaDestino = contaDestino;
            Valor = valor;
        }
    }

    class executarTransacaoFinanceira: acessoDados
    {
        public void transferir(int correlation_id, ulong conta_origem, ulong conta_destino, decimal valor)
        {
            contas_saldo conta_saldo_origem = getSaldo<contas_saldo>(conta_origem) ;
            if (conta_saldo_origem.saldo < valor)
            {
                Console.WriteLine("Transacao numero {0 } foi cancelada por falta de saldo", correlation_id);

            }
            else
            {
                contas_saldo conta_saldo_destino = getSaldo<contas_saldo>(conta_destino);
                conta_saldo_origem.saldo -= valor;
                conta_saldo_destino.saldo += valor;
                Console.WriteLine("Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem:{1} | Conta Destino: {2}", correlation_id, conta_saldo_origem.saldo, conta_saldo_destino.saldo);
            }
        }
    }
    class contas_saldo
    {
        public contas_saldo(ulong conta, decimal valor)
        {
            this.conta = conta;
            this.saldo = valor;
        }
        public ulong conta { get; set; }
        public decimal saldo { get; set; }
    }
    class acessoDados
    {
        Dictionary<ulong, decimal> SALDOS { get; set; }
        private List<contas_saldo> TABELA_SALDOS;
        public acessoDados()
        {
            TABELA_SALDOS = new List<contas_saldo>();
            TABELA_SALDOS.Add(new contas_saldo(938485762, 180));
            TABELA_SALDOS.Add(new contas_saldo(347586970, 1200));
            TABELA_SALDOS.Add(new contas_saldo(2147483649, 0));
            TABELA_SALDOS.Add(new contas_saldo(675869708, 4900));
            TABELA_SALDOS.Add(new contas_saldo(238596054, 478));
            TABELA_SALDOS.Add(new contas_saldo(573659065, 787));
            TABELA_SALDOS.Add(new contas_saldo(210385733, 10));
            TABELA_SALDOS.Add(new contas_saldo(674038564, 400));
            TABELA_SALDOS.Add(new contas_saldo(563856300, 1200));


            SALDOS = new Dictionary<ulong, decimal>();
            this.SALDOS.Add(938485762, 180);
           
        }
        public T getSaldo<T>(ulong id)
        {          
            return (T)Convert.ChangeType(TABELA_SALDOS.Find(x => x.conta == id), typeof(T));
        }
        public bool atualizar<T>(T  dado)
        {
            try
            {
                contas_saldo item = (dado as contas_saldo);
                TABELA_SALDOS.RemoveAll(x => x.conta == item.conta);
                TABELA_SALDOS.Add(dado as contas_saldo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

    }
}
