public class Moeda(double preco, string nome)
{
    private readonly double valor = preco;
    private readonly string nome = nome;
    public string Nome => nome;
    public double Valor => valor;
}