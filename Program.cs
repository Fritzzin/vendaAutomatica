// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


Produto cocaCola = new Produto(10, 1.50, "Coca-cola");
Produto pastelina = new Produto(10, 0.30, "Pastelina");
Produto agua = new Produto(10, 1, "Agua");

double[] tiposMoedas = [0.01, 0.05, 0.10, 0.25, 0.50, 1.0];


foreach (double moeda in tiposMoedas)
{
    Console.WriteLine(moeda);
}
// Ler input de usuario
// Separar inputs por espaco
// Verificar se inputs existem no sistema (moeda e nome do produto)
// Adicionar moedas utilizadas
// Verificar se ha produtos suficiente
// Verificar se ha dinheiro o suficiente
// Caso valor for maior, verificar se ha moedas para troco
// Caso nao houver moedas o suficiente, dar erro NO_COINS
// Caso nao houver troco a ser entregue, enviar NO_CHANGE


