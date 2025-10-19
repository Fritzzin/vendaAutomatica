using VendaAutomatica.src;

namespace VendaAutomatica.UnitTests;

public class ProdutoTests
{
    [Fact]
    public void RemoverProduto_ProdutoRemovido_Retornar9()
    {
        // Arrange
        int quantidadeEsperada = 9;
        Produto produto = new("Coca-Cola", 1.5m, 10);

        // Act
        produto.RemoverProduto();

        // Assert
        Assert.Equal(quantidadeEsperada, produto.Quantidade);
    }
}
