using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTest
{
    [Fact]
    public void Success()
    {
        // Arrange - Instâncias, tudo que é necessário para realizar o teste
        var validator = new ExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();

        // Act - Etapa de validação, Execução do método.
        var result = validator.Validate(request);

        // Assert - Espera do resulatdo, no caso, True.
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Tag_Invalid()
    {
        var validator = new ExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Tags.Add((Tag)1000);

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TAG_TYPE_NOT_SUPPORTED));
    }
}