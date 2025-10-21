using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Extensions;

public static class PaymentTypeExtension
{
    // 'this' indica que isso é um método de extensão. Dessa forma pode ser chamado naturalmente pela classe PaymentType.
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de Crédito",
            PaymentType.DebitCard => "Cartão de Débito",
            PaymentType.EletronicTransfer => "Transferência Bancária",
            _ => string.Empty // Qualquer valor não listado retorna uma string vazia.
        };
    }

    /*
    Resumo: A classe define um método de extensão para PaymentType que quando chamado converte o valor do enum
    em uma string amigável para exibição. Utiliza o 'switch expression' para simplificar a lógica.
    */
}