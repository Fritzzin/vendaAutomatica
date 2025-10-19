namespace VendaAutomatica.src;

class Maquina
{
    int quantidadeProdutosInicial = 10;
    int quantidadeMoedasInicial = 10;

    List<Produto> produtos;
    List<Moeda> moedas;

    string? inputUsuario = "";
    decimal saldo = 0m;

    List<Produto> listaDeCompras = [];
    List<Moeda> moedasParaTroco = [];

    public Maquina()
    {
        produtos =
        [
            new("Coca-cola", 1.50m, quantidadeProdutosInicial),
            new("Pastelina", 0.30m, quantidadeProdutosInicial),
            new("Agua", 1m, quantidadeProdutosInicial),
        ];

        moedas =
        [
            new("0.01", 0.01m, quantidadeMoedasInicial),
            new("0.05", 0.05m, quantidadeMoedasInicial),
            new("0.10", 0.10m, quantidadeMoedasInicial),
            new("0.25", 0.25m, quantidadeMoedasInicial),
            new("0.50", 0.50m, quantidadeMoedasInicial),
            new("1.00", 1.0m, quantidadeMoedasInicial),
        ];
    }

    public void IniciarSistema()
    {
        OrganizarMoedas(moedas);
        Apresentacao();
        while (inputUsuario != "q")
        {
            VerificarSaldo();
            if (VerificaInputUsuario())
            {
                RealizarLogica();
            }
        }
    }

    void RealizarLogica()
    {
        bool precisaTroco = false;
        string[] opcoesDoUsuario = inputUsuario.Split(" ", StringSplitOptions.RemoveEmptyEntries);
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

        // Se houver algo na lista de compras, realizar as compras
        if (listaDeCompras.Count > 0)
        {
            RealizarCompra();
        }

        // se usuario requisitou troco, realizar o calculo de troco
        if (precisaTroco)
        {
            CalcularTroco();
        }
    }

    void VerificarSaldo()
    {
        if (saldo > 0m)
        {
            Console.Write("Insira moedas e escolha um produto (Saldo Atual: R$" + saldo + "): ");
        }
        else
        {
            Console.Write("Insira moedas e escolha um produto: ");
        }
    }

    bool VerificaInputUsuario()
    {
        inputUsuario = Console.ReadLine();
        if (inputUsuario == null || inputUsuario.Trim() == "")
        {
            Console.WriteLine("Favor inserir algum valor.");
            return false;
        }
        return true;
    }

    void Apresentacao()
    {
        Console.WriteLine("\n\n\n\n");
        Console.WriteLine("Bem Vindo!");
        Console.WriteLine("----------");
        Console.WriteLine("Produtos Disponiveis:");
        foreach (Produto produto in produtos)
        {
            Console.Write(string.Concat(produto.Nome, " | "));
        }

        Console.WriteLine("\n----------");

        Console.WriteLine("Moedas Disponiveis:");
        foreach (Moeda moeda in moedas)
        {
            Console.Write(string.Concat(moeda.Nome, " | "));
        }
        Console.WriteLine("\n");
    }

    void VerificarMoeda(string opcao)
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
                    // Console.WriteLine(moeda.Nome + ": " + moeda.Quantidade + " quantidade(s)");
                }
            }

            if (!encontrouMoedaValida)
            {
                Console.WriteLine(valorDaOpcao + " Nao e uma moeda valida");
            }
        }
        catch (Exception)
        {
            //
        }
    }

    void VerificarDisponibilidadeProduto(string opcao)
    {
        foreach (Produto produto in produtos)
        {
            if (produto.Nome.Equals(opcao, StringComparison.CurrentCultureIgnoreCase))
            {
                if (produto.Quantidade > 0)
                {
                    // Console.WriteLine("Produto disponivel");
                    listaDeCompras.Add(produto);
                }
                else
                {
                    Console.Write("NO_PRODUCT");
                    // Console.WriteLine("Produto " + produto.Nome + " em falta");
                }
            }
        }
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
                    Console.Write(produto.Nome + "=" + saldo + " ");
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
        decimal troco = 0m;
        moedasParaTroco = [];

        if (saldo > 0m)
        {
            // Console.WriteLine("CALCULANDO TROCO...");
            // Console.WriteLine("TROCO: " + saldo);
            foreach (Moeda moeda in moedas)
            {
                if (saldo - troco == 0)
                {
                    break;
                }

                if (moeda.Quantidade > 0 && (saldo - troco > 0))
                {
                    for (int i = 0; i < moeda.Quantidade; i++)
                    {
                        if (troco < saldo && (saldo - troco >= moeda.Valor))
                        {
                            troco += moeda.Valor;
                            moeda.RemoverMoeda();
                            moedasParaTroco.Add(moeda);
                        }
                    }
                }
            }

            if (troco == saldo)
            {
                // Console.WriteLine("Moedas para troco: ");
                Console.Write("|");
                foreach (Moeda moeda in moedasParaTroco)
                {
                    Console.Write(" " + moeda.Nome);
                }
                saldo = 0m;
            }
            else
            {
                Console.WriteLine("NO_COINS");
            }
        }
        else
        {
            // Console.WriteLine("Nao ha troco a ser entregue");
            Console.Write("NO_CHANGE");
        }
    }

    static void OrganizarMoedas(List<Moeda> moedas)
    {
        moedas.Sort((x, y) => y.Valor.CompareTo(x.Valor));
    }
}
