namespace VendaAutomatica.src
{
    public class Moeda(string nome, decimal preco, int quantidade)
    {
        public string Nome
        {
            get => nome;
        }
        public decimal Valor
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
            if (Quantidade > 0)
            {
                Quantidade--;
            }
        }
    }
}
