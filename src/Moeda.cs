namespace venda_automatica.src
{
    public class Moeda(string nome, double preco, int quantidade)
    {
        public string Nome
        {
            get => nome;
        }
        public double Valor
        {
            get => preco;
        }

        public int Quantidade
        {
            get => quantidade;
            set => quantidade = value;
        }

        public void InserirMoeda()
        {
            Quantidade++;
        }

        public void RemoverMoeda()
        {
            Quantidade--;
        }
    }
}
