using VendaAutomatica.src;

namespace VendaAutomatica.UnitTests;

public class MoedaTests
{
    [Fact]
    public void RemoverMoeda_MoedaRemovida_Retornar9()
    {
        Moeda moeda = new("0.01", 0.01m, 10);
        moeda.RemoverMoeda();
        Assert.Equal(9, moeda.Quantidade);
    }

    [Fact]
    public void InserirMoeda_MoedaInserida_Retornar11()
    {
        Moeda moeda = new("0.01", 0.01m, 10);
        moeda.InserirMoeda();
        Assert.Equal(11, moeda.Quantidade);
    }
}
