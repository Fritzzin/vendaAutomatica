using venda_automatica.src;

namespace VendaAutomatica.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Moeda moeda = new("0.01", 0.01m, 10);
        moeda.RemoverMoeda();
        Assert.Equal(9, moeda.Quantidade);
    }
}
