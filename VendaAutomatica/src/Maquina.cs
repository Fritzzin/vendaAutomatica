namespace VendaAutomatica.src;

public class Maquina
{
    int quantidadeProdutosInicial = 10;
    int quantidadeMoedasInicial = 10;

    List<Produto> produtos;
    List<Moeda> moedas;

    string inputUsuario = "";
    decimal saldoNaMaquina = 0m;

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
        OrganizarMoedas(moedas);
    }

    public void IniciarSistema()
    {
        Apresentacao();
        while (inputUsuario != "q")
        {
            VerificarSaldo(saldoNaMaquina);
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
            Console.Write(CalcularTroco(saldoNaMaquina));
        }

        Console.Write("\n");
    }

    public bool VerificarSaldo(decimal saldo)
    {
        if (saldo > 0m)
        {
            Console.Write("Insira moedas e escolha um produto (Saldo Atual: R$" + saldo + "): ");
            return true;
        }
        else
        {
            Console.Write("Insira moedas e escolha um produto: ");
            return false;
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

    public bool VerificarMoeda(string opcao)
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
                    saldoNaMaquina += valorDaOpcao;
                    return true;
                }
            }

            if (!encontrouMoedaValida)
            {
                Console.WriteLine(valorDaOpcao + " Nao e uma moeda valida");
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }

    public bool VerificarDisponibilidadeProduto(string opcao)
    {
        foreach (Produto produto in produtos)
        {
            if (produto.Nome.Equals(opcao, StringComparison.CurrentCultureIgnoreCase))
            {
                if (produto.Quantidade > 0)
                {
                    listaDeCompras.Add(produto);
                    return true;
                }
                else
                {
                    Console.Write("NO_PRODUCT");
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
            ComprarProduto(produto, saldoNaMaquina);
        }
        listaDeCompras = [];
    }

    public bool ComprarProduto(Produto produto, decimal saldo)
    {
        if (produto.Quantidade > 0)
        {
            if (saldo >= produto.Preco)
            {
                produto.RemoverProduto();
                saldoNaMaquina -= produto.Preco;
                Console.Write(produto.Nome + "=" + saldoNaMaquina + " ");
                return true;
            }
            else
            {
                Console.WriteLine("Nao ha saldo suficiente para o produto");
                return false;
            }
        }
        return false;
    }

    public string CalcularTroco(decimal saldo)
    {
        decimal troco = 0m;
        moedasParaTroco = [];

        if (saldo > 0m)
        {
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
                string trocoEmString = "";

                foreach (Moeda moeda in moedasParaTroco)
                {
                    trocoEmString += " " + moeda.Nome;
                }

                saldoNaMaquina = 0m;

                return trocoEmString;
            }
            else
            {
                return "NO_COINS";
            }
        }
        else
        {
            return "NO_CHANGE";
        }
    }

    public static void OrganizarMoedas(List<Moeda> moedas)
    {
        moedas.Sort((x, y) => y.Valor.CompareTo(x.Valor));
    }
}
