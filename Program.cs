// See https://aka.ms/new-console-template for more information
Console.WriteLine("Bem Vindo!");
Console.WriteLine("----------");

Produto[] produtos = [new(10, 1.50, "Coca-cola"), new(10, 0.30, "Pastelina"), new(10, 1, "Agua")];

Moeda[] moedas =
[
    new(0.01, "0.01"),
    new(0.05, "0.05"),
    new(0.10, "0.10"),
    new(0.25, "0.25"),
    new(0.50, "0.50"),
    new(1.0, "1.0"),
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
while (inputUsuario != "q")
{
    Console.WriteLine("\n\n\n");
    Console.Write("Insira moedas e escolha produto: ");

    inputUsuario = Console.ReadLine();
    if (inputUsuario == null || inputUsuario.Trim() == "")
    {
        Console.WriteLine("Valor vazio. Favor inserir algum valor.");
        continue;
    }
    else
    {
        Console.WriteLine("INPUT USUARIO: " + inputUsuario);
        string[] opcoesDoUsuario = inputUsuario.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        foreach (string opcao in opcoesDoUsuario)
        {
            Console.WriteLine(opcao);
        }
    }
}

// Verificar se inputs existem no sistema (moeda e nome do produto)
// Adicionar moedas utilizadas
// Verificar se ha produtos suficiente
// Verificar se ha dinheiro o suficiente
// Caso valor for maior, verificar se ha moedas para troco
// Caso nao houver moedas o suficiente, dar erro NO_COINS
// Caso nao houver troco a ser entregue, enviar NO_CHANGE
