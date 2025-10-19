using VendaAutomatica.src;

namespace VendaAutomatica.UnitTests;

public class ProdutoTests
{
    [Theory]
    [InlineData(10, 9)]
    [InlineData(11, 10)]
    [InlineData(4, 3)]
    [InlineData(0, 0)]
    public void RemoverProduto_ProdutoRemovido_Retornar9(
        int quantidadeInicial,
        int quantidadeEsperada
    )
    {
        // Arrange
        Produto produto = new("Coca-Cola", 1.5m, quantidadeInicial);

        // Act
        produto.RemoverProduto();

        // Assert
        Assert.Equal(quantidadeEsperada, produto.Quantidade);
    }
}
