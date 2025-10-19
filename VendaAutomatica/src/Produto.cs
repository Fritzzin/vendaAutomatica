namespace VendaAutomatica.src
{
    public class Produto(string nome, decimal preco, int quantidade)
    {
        public int Quantidade
        {
            get => quantidade;
            set => quantidade = value;
        }
        public decimal Preco
        {
            get => preco;
        }
        public string Nome
        {
            get => nome;
        }

        public void RemoverProduto()
        {
            if (Quantidade > 0)
            {
                Quantidade--;
            }
        }
    }
}
