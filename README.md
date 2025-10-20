# Maquina de Vendas Automática

Projeto feito em .NET 9 (C#). Uma Máquina de Vendas (Vending Machine), que calculará e entregará o troco ao usuário após a compra.

## Rodando O Projeto
Você pode abrir o projeto em um editor de código como VSCode ou Visual Studio, e rodar diretamente por lá.

Para realizar o "Build" do código, você pode digitar o comando `dotnet publish` e o executável do programa será criado dentro da pasta `VendaAutomatica\bin\Release\net9.0\publish`.

## Utilizando O Programa
O sistema possui 3 produtos e 6 tipos de moedas, aos quais serão mostrados ao usuário ao abrir o programa. Apenas digite as moedas que você pretende utilizar (Ex:`0.5 0.10 0.05`), O produto (Ex:`agua`), e, caso quiser o troco imediatamente, colocar a opção `change`. Estes podem ser escritos em qualquer ordem.

Exemplo de uso: `1 pastelina change` <br>
O programa responderá: `Pastelina=0.70  0.50 0.10 0.10` (Produto=Troco, seguido das moedas utilizadas para troco).


