namespace venda_automatica.src
{
    public class Produto(int quantidade, double preco, string nome)
    {
        public int Quantidade
        {
            get => quantidade;
            set => quantidade = value;
        }
        public double Preco
        {
            get => preco;
        }
        public string Nome
        {
            get => nome;
        }
    }
}
