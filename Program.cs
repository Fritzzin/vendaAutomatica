// See https://aka.ms/new-console-template for more information
using venda_automatica.src;

Console.WriteLine("Bem Vindo!");
Console.WriteLine("----------");

List<Produto> produtos =
[
    new(10, 1.50, "Coca-cola"),
    new(10, 0.30, "Pastelina"),
    new(10, 1, "Agua"),
];

List<Moeda> moedas =
[
    new("0.01", 0.01, 10),
    new("0.05", 0.05, 10),
    new("0.10", 0.10, 10),
    new("0.25", 0.25, 10),
    new("0.50", 0.50, 10),
    new("1.0", 1.0, 10),
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
double saldoAtual = 0;

// Loop para execucao do programa
while (inputUsuario != "q")
{
    Console.WriteLine("\n\n");

    if (saldoAtual > 0)
    {
        Console.Write("Insira moedas e escolha um produto (Saldo Atual:" + saldoAtual + "):");
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

    Console.WriteLine("INPUT USUARIO: " + inputUsuario);
    string[] opcoesDoUsuario = inputUsuario.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    foreach (string opcao in opcoesDoUsuario)
    {
        bool encontrouOpcaoValida = false;

        // Verificar a opcao se referencia a um produto
        foreach (Produto produto in produtos)
        {
            if (produto.Nome.Equals(opcao, StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Encontrei produto!");
                encontrouOpcaoValida = true;
            }
        }

        // try/catch para caso nao seja possivel realizar parse
        try
        {
            bool encontrouMoedaValida = false;
            double valorDaOpcao = 0;
            if (opcao.Contains(','))
            {
                valorDaOpcao = Double.Parse(opcao.Replace(",", "."));
            }
            else
            {
                valorDaOpcao = Double.Parse(opcao);
            }
            foreach (Moeda moeda in moedas)
            {
                if (valorDaOpcao == moeda.Valor)
                {
                    encontrouOpcaoValida = true;
                    encontrouMoedaValida = true;
                    moeda.InserirMoeda();
                    saldoAtual = Math.Round((saldoAtual + valorDaOpcao), 2);
                    Console.WriteLine(moeda.Nome + ": " + moeda.Quantidade + " quantidade");
                }
            }
            if (!encontrouMoedaValida)
            {
                Console.WriteLine(valorDaOpcao + " Nao e uma moeda valida");
            }
        }
        catch (System.Exception)
        {
            // Console.WriteLine("Nao é uma moeda");
        }

        // Mensagens de erro
        if (!encontrouOpcaoValida)
        {
            Console.WriteLine("Digite moedas e/ou produtos validos");
        }
    }
}

// Verificar quantidade
// Dizer se o produto e valido para compra
// Verificar se ha dinheiro o suficiente
// Reduzir saldo
// remover quantidade de moedas
// remover quantidade de produto
// Caso valor for maior, verificar se ha moedas para troco
// Caso nao houver moedas o suficiente, dar erro NO_COINS
// Caso nao houver troco a ser entregue, enviar NO_CHANGE
