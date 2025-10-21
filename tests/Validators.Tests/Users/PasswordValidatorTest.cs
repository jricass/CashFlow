using CashFlow.Application.UseCases.Users;
using CashFlow.Communication.Requests;
using FluentValidation;
using Shouldly;

namespace Validators.Tests.Users;

public class PasswordValidatorTest
{
    [Theory]
    // Vazio ou espaços em branco
    [InlineData("")]
    [InlineData("   ")]
    // Menos de 8 caracteres
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    // Sem letras maiúsculas
    [InlineData("aaaaaaaa")]
    // Sem letras minúsculas
    [InlineData("AAAAAAAA")]
    // Sem número
    [InlineData("Aaaaaaaa")]
    // Sem caractere especial
    [InlineData("Aaaaaaa1")]
    public void Error_Name_Empty(string password)
    {
        var validator = new PasswordValidator<RequestRegisterUserJson>();

        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

        result.ShouldBeFalse();
    }
}