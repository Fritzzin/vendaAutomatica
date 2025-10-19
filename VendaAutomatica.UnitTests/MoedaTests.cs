using VendaAutomatica.src;

namespace VendaAutomatica.UnitTests;

public class MoedaTests
{
    [Theory]
    [InlineData(10, 9)]
    [InlineData(11, 10)]
    [InlineData(4, 3)]
    [InlineData(0, 0)]
    public void RemoverMoeda_MoedaRemovida_Retornar9(int quantidadeInicial, int quantidadeEsperada)
    {
        Moeda moeda = new("0.01", 0.01m, quantidadeInicial);
        moeda.RemoverMoeda();
        Assert.Equal(quantidadeEsperada, moeda.Quantidade);
    }

    [Theory]
    [InlineData(10, 11)]
    [InlineData(11, 12)]
    [InlineData(4, 5)]
    [InlineData(0, 1)]
    public void InserirMoeda_MoedaInserida_Retornar11(int quantidadeInicial, int quantidadeEsperada)
    {
        Moeda moeda = new("0.01", 0.01m, quantidadeInicial);
        moeda.InserirMoeda();
        Assert.Equal(quantidadeEsperada, moeda.Quantidade);
    }
}
