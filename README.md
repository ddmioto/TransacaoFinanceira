# TransacaoFinanceira
Case para refatoração

Passos a implementar:
1. Corrija o que for necessario para resolver os erros de compilação.
2. Execute o programa para avaliar a saida, identifique e corrija o motivo de algumas transacoes estarem sendo canceladas mesmo com saldo positivo e outras sem saldo sendo efetivadas.
3. Aplique o code review e refatore conforme as melhores praticas(SOLID,Patterns,etc).
4. Implemente os testes unitários que julgar efetivo.
5. Crie um git hub e compartilhe o link respondendo o ultimo e-mail.

Obs: Voce é livre para implementar na linguagem de sua preferência desde que respeite as funcionalidades e saídas existentes, além de aplicar os conceitos solicitados.

# Avaliação do AS IS

### Retorno ao executar o projeto

``` dotnet run ```

Retorno:

```
/usr/local/share/dotnet/sdk/8.0.401/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.EolTargetFrameworks.targets(32,5): warning NETSDK1138: The target framework 'net5.0' is out of support and will not receive security updates in the future. Please refer to https://aka.ms/dotnet-core-support for more information about the support policy. [/Users/ddmioto/Documents/Itaú/case-sr/TransacaoFinanceira/TransacaoFinanceira.csproj]

/usr/local/share/dotnet/sdk/8.0.401/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.EolTargetFrameworks.targets(32,5): warning NETSDK1138: The target framework 'net5.0' is out of support and will not receive security updates in the future. Please refer to https://aka.ms/dotnet-core-support for more information about the support policy. [/Users/ddmioto/Documents/Itaú/case-sr/TransacaoFinanceira/TransacaoFinanceira.csproj]

/usr/local/share/dotnet/sdk/8.0.401/Sdks/Microsoft.NET.Sdk/targets/Microsoft.NET.EolTargetFrameworks.targets(32,5): warning NETSDK1138: The target framework 'net5.0' is out of support and will not receive security updates in the future. Please refer to https://aka.ms/dotnet-core-support for more information about the support policy. [/Users/ddmioto/Documents/Itaú/case-sr/TransacaoFinanceira/TransacaoFinanceira.csproj]

/Users/ddmioto/Documents/Itaú/case-sr/TransacaoFinanceira/Program.cs(72,48): error CS1503: Argument 1: cannot convert from 'uint' to 'int' [/Users/ddmioto/Documents/Itaú/case-sr/TransacaoFinanceira/TransacaoFinanceira.csproj]

/Users/ddmioto/Documents/Itaú/case-sr/TransacaoFinanceira/Program.cs(15,30): error CS0826: No best type found for implicitly-typed array [/Users/ddmioto/Documents/Itaú/case-sr/TransacaoFinanceira/TransacaoFinanceira.csproj]

The build failed. Fix the build errors and run again.
```

- **Warning NETSDK1138** indica que o framework do projeto aponta para a versão 5.0, que está fora de suporte e não receberá mais atualizações de segurança no futuro. Será necessário atualização para a versão mais recente, no momento, net8.0.
  
- **Erro CS1503**: Este erro indica que há uma incompatibilidade de tipos de dados em uma chamada de método no arquivo Program.cs na linha 72. O argumento fornecido é do tipo uint, mas o método espera um argumento do tipo int.

- **Erro CS0826**: Este erro indica que o compilador não conseguiu determinar o melhor tipo para um array tipado implicitamente, que está localizado na linha 15 do arquivo Program.cs.

### Teste de mesa

Contas e Saldos:

```
(938485762, 180)
(347586970, 1200)
(2147483649, 0)
(675869708, 4900)
(238596054, 478)
(573659065, 787)
(210385733, 10)
(674038564, 400)
(563856300, 1200)
```

Transações:

```
{correlation_id= 1,datetime="09/09/2023 14:15:00", conta_origem= 938485762, conta_destino= 2147483649, VALOR= 150}
{correlation_id= 2,datetime="09/09/2023 14:15:05", conta_origem= 2147483649, conta_destino= 210385733, VALOR= 149}
{correlation_id= 3,datetime="09/09/2023 14:15:29", conta_origem= 347586970, conta_destino= 238596054, VALOR= 1100}
{correlation_id= 4,datetime="09/09/2023 14:17:00", conta_origem= 675869708, conta_destino= 210385733, VALOR= 5300}
{correlation_id= 5,datetime="09/09/2023 14:18:00", conta_origem= 238596054, conta_destino= 674038564, VALOR= 1489}
{correlation_id= 6,datetime="09/09/2023 14:18:20", conta_origem= 573659065, conta_destino= 563856300, VALOR= 49}
{correlation_id= 7,datetime="09/09/2023 14:19:00", conta_origem= 938485762, conta_destino= 2147483649, VALOR= 44}
{correlation_id= 8,datetime="09/09/2023 14:19:01", conta_origem= 573659065, conta_destino= 675869708, VALOR= 150}
```

Resultado esperado:

```
ID 1: 09/09/2023 14:15:00 - Origem: 938485762 (180 ➔ 30), Destino: 2147483649 (0 ➔ 150), Valor: 150 - Sucesso
ID 2: 09/09/2023 14:15:05 - Origem: 2147483649 (150 ➔ 1), Destino: 210385733 (10 ➔ 159), Valor: 149 - Sucesso
ID 3: 09/09/2023 14:15:29 - Origem: 347586970 (1200 ➔ 100), Destino: 238596054 (478 ➔ 1578), Valor: 1100 - Sucesso
ID 4: 09/09/2023 14:17:00 - Origem: 675869708 (4900), Destino: 210385733 (159), Valor: 5300 - Erro: Saldo Insuficiente
ID 5: 09/09/2023 14:18:00 - Origem: 238596054 (1578 ➔ 89), Destino: 674038564 (400 ➔ 1889), Valor: 1489 - Sucesso
ID 6: 09/09/2023 14:18:20 - Origem: 573659065 (787 ➔ 738), Destino: 563856300 (1200 ➔ 1249), Valor: 49 - Sucesso
ID 7: 09/09/2023 14:19:00 - Origem: 938485762 (30), Destino: 2147483649 (1), Valor: 44 - Erro: Saldo Insuficiente
ID 8: 09/09/2023 14:19:01 - Origem: 573659065 (738 ➔ 588), Destino: 675869708 (4900 ➔ 5050), Valor: 150 - Sucesso
```

### Avaliação código

- Correção de erros de compilação por tipagem
- Paralelismo
- Falta de modelagem de dados
- Ausência de injeção de dependência
- Uso incorreto de herança
- Violação de encapsulamento
- Ausência de testes unitários

## Solução

### 1. Correção de tipagem
```csharp
    public ulong ContaOrigem { get; }
    public ulong ContaDestino { get; }
```

### 2. Manter ordem e consistência das transações
```csharp
    var contaRepository = new ContaRepository();
    var executor = new TransacaoService(contaRepository);

    foreach (var item in transacoes)
    {
        executor.Transferir(item);
    }
```

### 3. Utilização de models
```csharp
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
```

### 4. Uso de repository removendo uso incorreto de herança
```csharp
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
```

### 5. service para transações
```csharp
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
```

### 6. Ajuste estrutura do projeto

TransacaoFinanceira/
├── Models/
│   ├── Transacao.cs
│   ├── ContaSaldo.cs
├── Services/
│   ├── TransacaoService.cs
├── Repositories/
│   ├── ContaRepository.cs
├── Program.cs
├── TransacaoFinanceira.csproj

TransacaoFinanceira.Tests/
├── Models/
│   ├── TransacaoTests.cs
├── Services/
│   ├── TransacaoServiceTests.cs
├── Repositories/
│   ├── ContaRepositoryTests.cs
├── TransacaoFinanceira.Tests.csproj
