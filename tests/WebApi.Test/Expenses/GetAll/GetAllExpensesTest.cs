using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Test.Expenses.GetAll;

public class GetAllExpensesTest : CashFlowClassFixure
{
    private const string METHOD = "api/Expense";
    private readonly string _token;

    public GetAllExpensesTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);

        result.StatusCode.ShouldBe(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("expenses").EnumerateArray().ShouldNotBeEmpty();
    }
}