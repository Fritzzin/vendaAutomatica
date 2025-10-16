namespace venda_automatica.src
{
    public class Moeda(double preco, string nome)
    {
        public string Nome
        {
            get => nome;
        }
        public double Valor
        {
            get => preco;
        }
    }
}