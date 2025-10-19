using VendaAutomatica.src;

namespace VendaAutomatica.UnitTests;

public class MaquinaTests
{
    [Theory]
    [InlineData("0.05")]
    [InlineData("0,05")]
    [InlineData("0.10")]
    [InlineData(".5")]
    [InlineData("0,10")]
    [InlineData("1")]
    public void VerificarMoeda_MoedaValida_RetornarTrue(string moeda)
    {
        Maquina maquina = new();
        Assert.True(maquina.VerificarMoeda(moeda));
    }

    [Theory]
    [InlineData("0.005")]
    [InlineData("-1")]
    [InlineData("0.55")]
    [InlineData("0.3")]
    public void VerificarMoeda_MoedaInvalida_RetornarFalse(string moeda)
    {
        Maquina maquina = new();
        Assert.False(maquina.VerificarMoeda(moeda));
    }

    [Theory]
    [InlineData("Coca-cola")]
    [InlineData("CoCa-CoLa")]
    [InlineData("pastelina")]
    [InlineData("agua")]
    public void VerificarProduto_NomeValido_RetornarTrue(string produto)
    {
        Maquina maquina = new();
        Assert.True(maquina.VerificarDisponibilidadeProduto(produto));
    }

    [Theory]
    [InlineData("pepsi")]
    [InlineData("0.5")]
    [InlineData("abcdefg")]
    public void VerificarProduto_NomeInvalido_RetornarFalse(string produto)
    {
        Maquina maquina = new();
        Assert.False(maquina.VerificarDisponibilidadeProduto(produto));
    }

    [Theory]
    [InlineData("1.1")]
    [InlineData("0.1")]
    [InlineData("0.01")]
    public void VerificarSaldo_SaldoPositivo_RetornarTrue(string saldo)
    {
        Maquina maquina = new();
        // Inlinedata nao aceita deciamal com 'm'
        // Apos pesquisar, me foi recomendado converter string -> decimal
        Assert.True(maquina.VerificarSaldo(decimal.Parse(saldo)));
    }

    [Fact]
    public void VerificarSaldo_SaldoZerado_RetornarFalse()
    {
        Maquina maquina = new();
        Assert.False(maquina.VerificarSaldo(0m));
    }
}
