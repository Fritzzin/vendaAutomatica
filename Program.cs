using venda_automatica.src;

Console.WriteLine("Bem Vindo!");
Console.WriteLine("----------");

List<Produto> produtos =
[
    new("Coca-cola", 1.50m, 10),
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

List<Produto> listaDeCompras = [];
List<Moeda> moedasParaTroco = [];

organizarMoedas();

// Loop para execucao do programa
while (inputUsuario != "q")
{
    Console.WriteLine("\n\n");

    if (saldo > 0m)
    {
        Console.Write("Insira moedas e escolha um produto (Saldo Atual: R$" + saldo + "):");
    }
    else
    {
        Console.Write("Insira moedas e escolha um produto: ");
    }

    // verificar inputs do usuario
    inputUsuario = Console.ReadLine();
    if (inputUsuario == null || inputUsuario.Trim() == "")
    {
        Console.WriteLine("Valor vazio. Favor inserir algum valor.");
        continue;
    }
    // Console.WriteLine("INPUT USUARIO: " + inputUsuario);
    realizarLogica(inputUsuario);
}

void realizarLogica(string inputUsuario)
{
    string[] opcoesDoUsuario = inputUsuario.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    bool precisaTroco = false;
    foreach (string opcao in opcoesDoUsuario)
    {
        // se a opcao se referir a uma moeda, adicionara ao saldo total
        VerificarMoeda(opcao);

        // se for um produto, adicionara a lista de compras
        VerificarDisponibilidadeProduto(opcao);

        // se a opcao for "CHANGE", entregara troco no final
        if (opcao.Equals("CHANGE", StringComparison.CurrentCultureIgnoreCase))
        {
            precisaTroco = true;
        }
    }

    if (listaDeCompras.Count > 0)
    {
        RealizarCompra();
    }

    if (precisaTroco)
    {
        CalcularTroco();
    }
}

bool VerificarMoeda(string opcao)
{
    bool encontrouMoedaValida = false;
    decimal valorDaOpcao = 0m;

    try
    {
        // Aceitar , ou . para decimais
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
                Console.WriteLine(moeda.Nome + ": " + moeda.Quantidade + " quantidade(s)");
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

bool VerificarDisponibilidadeProduto(string opcao)
{
    foreach (Produto produto in produtos)
    {
        if (produto.Nome.Equals(opcao, StringComparison.CurrentCultureIgnoreCase))
        {
            if (produto.Quantidade > 0)
            {
                Console.WriteLine("Produto disponivel");
                listaDeCompras.Add(produto);
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

void RealizarCompra()
{
    foreach (Produto produto in listaDeCompras)
    {
        if (produto.Quantidade > 0)
        {
            if (saldo >= produto.Preco)
            {
                produto.RemoverProduto();
                saldo -= produto.Preco;
            }
            else
            {
                Console.WriteLine("Nao ha saldo suficiente para o produto");
            }
        }
    }
    listaDeCompras = [];
}

void CalcularTroco()
{
    if (saldo > 0m)
    {
        Console.WriteLine("CALCULANDO TROCO...");
        Console.WriteLine("TROCO: " + saldo);
        foreach (Moeda moeda in moedas)
        {
            Console.WriteLine(moeda.Nome);
        }
        // Verificar se ha a possibilidade de troco com as moedas atuais
        // Criar lista com moedas de troco
    }
    else
    {
        Console.WriteLine("Nao ha troco a ser entregue");
        Console.WriteLine("NO_CHANGE");
    }
}

void organizarMoedas()
{
    moedas.Sort((x, y) => y.Valor.CompareTo(x.Valor));
    // moedas.OrderBy(moeda => moeda.Valor);
    foreach (Moeda moeda in moedas)
    {
        Console.WriteLine("Valor Moeda: " + moeda.Valor);
    }
}

// Processo de compra
// INPUT: Moedas -> Produto -> Troco (opicional)
// - Verificar moedas
//      * Contabilizar moedas no sistema
//      * Somar ao saldo total do usuario
// - Verificar qual produto (se for digitado)
//      * Verificar se ha quantidade suficiente no sistema
//      * Adicionar ao preco total
// - Compra:
//      * Se houver quantidade do produto, continuar processo
//      * Remover uma quantidade do produto do estoque
//      * Remover preco do saldo atual
// - Troco:
//      * Se houver valor maior que 0, realizar troco
//      * Comecar pelas moedas de valores maiores e ir diminuindo ate o valor chegar em 0
//          * Se houver quantidade maior que 0, remover quantidade e adicionar a lista de troco
//          * Retornar lista de moedas
//      * Caso nao for possivel chegar a 0, aparecer erro NO_COINS
