using venda_automatica.src;

Console.WriteLine("Bem Vindo!");
Console.WriteLine("----------");

List<Produto> produtos =
[
    new("Coca-cola", 1.50m, 0),
    new("Pastelina", 0.30m, 10),
    new("Agua", 1m, 10),
];

List<Moeda> moedas =
[
    new("0.01", 0.01m, 10),
    new("0.05", 0.05m, 10),
    new("0.10", 0.10m, 10),
    new("0.25", 0.25m, 10),
    new("0.50", 0.50m, 10),
    new("1.0", 1.0m, 10),
];

Console.WriteLine("Produtos Disponiveis:");
foreach (Produto produto in produtos)
{
    Console.Write(string.Concat(produto.Nome, " | "));
}

Console.WriteLine();
Console.WriteLine("----------");

Console.WriteLine("Moedas Disponiveis:");
foreach (Moeda moeda in moedas)
{
    Console.Write(string.Concat(moeda.Nome, " | "));
}

string? inputUsuario = "";
decimal saldo = 0m;
decimal precoTotal = 0m;

// Loop para execucao do programa
while (inputUsuario != "q")
{
    Console.WriteLine("\n\n");

    if (saldo > 0m)
    {
        Console.Write("Insira moedas e escolha um produto (Saldo Atual:" + saldo + "):");
    }
    else
    {
        Console.Write("Insira moedas e escolha um produto: ");
    }

    inputUsuario = Console.ReadLine();
    if (inputUsuario == null || inputUsuario.Trim() == "")
    {
        Console.WriteLine("Valor vazio. Favor inserir algum valor.");
        continue;
    }

    // verificar inputs do usuario
    Console.WriteLine("INPUT USUARIO: " + inputUsuario);
    string[] opcoesDoUsuario = inputUsuario.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    foreach (string opcao in opcoesDoUsuario)
    {
        // bool encontrouOpcaoValida = false;
        bool encontrouMoeda = false;
        bool encontrouProduto = false;

        encontrouMoeda = VerificarMoeda(opcao);
        encontrouProduto = VerificarProduto(opcao);

        // Realizar Compra
        RealizarCompra(encontrouProduto);
        if (opcao.Equals("CHANGE", StringComparison.CurrentCultureIgnoreCase))
        {
            CalcularTroco();
        }
        else
        {
            Console.WriteLine("SALDO: " + saldo);
            Console.WriteLine("PRECO: " + precoTotal);
        }

        // Mensagens de erro
        // if (!encontrouMoeda && !encontrouProduto)
        // {
        //     Console.WriteLine("Digite moedas e/ou produtos validos");
        // }
    }
}

bool VerificarMoeda(string opcao)
{
    // try/catch para caso nao seja possivel realizar parse
    try
    {
        bool encontrouMoedaValida = false;
        decimal valorDaOpcao = 0m;
        if (opcao.Contains(','))
        {
            valorDaOpcao = decimal.Parse(opcao.Replace(",", "."));
        }
        else
        {
            valorDaOpcao = decimal.Parse(opcao);
        }
        foreach (Moeda moeda in moedas)
        {
            if (valorDaOpcao == moeda.Valor)
            {
                encontrouMoedaValida = true;
                moeda.InserirMoeda();
                saldo += valorDaOpcao;
                Console.WriteLine(moeda.Nome + ": " + moeda.Quantidade + " quantidade");
                return true;
            }
        }
        if (!encontrouMoedaValida)
        {
            Console.WriteLine(valorDaOpcao + " Nao e uma moeda valida");
            return false;
        }
        else
        {
            return false;
        }
    }
    catch (Exception)
    {
        return false;
    }
}

bool VerificarProduto(string opcao)
{
    foreach (Produto produto in produtos)
    {
        if (produto.Nome.Equals(opcao, StringComparison.CurrentCultureIgnoreCase))
        {
            if (produto.Quantidade > 0)
            {
                Console.WriteLine("Produto disponivel");
                produto.RemoverProduto();
                precoTotal += produto.Preco;
                return true;
            }
            else
            {
                Console.WriteLine("Produto em falta");
                return false;
            }
        }
    }
    return false;
}

void RealizarCompra(bool produtoSelecionado)
{
    if (produtoSelecionado && ((saldo - precoTotal) >= 0))
    {
        saldo -= precoTotal;
        Console.WriteLine("Compra realizada");
    }
}

void CalcularTroco()
{
    if (saldo > 0)
    {
        Console.WriteLine("ENTREGAR TROCO");
        Console.WriteLine("TROCO: " + saldo);
        // Verificar se ha a possibilidade de troco com as moedas atuais
        // Criar lista com moedas de troco
    }
    else
    {
        Console.WriteLine("Nao ha troco a ser entregue");
        Console.WriteLine("NO_CHANGE");
    }
}

// Verificar se ha dinheiro o suficiente
// remover quantidade de moedas
// Caso valor for maior, verificar se ha moedas para troco
// Caso nao houver moedas o suficiente, dar erro NO_COINS
