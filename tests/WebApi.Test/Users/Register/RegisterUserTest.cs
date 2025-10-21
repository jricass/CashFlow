using System.Net;
using System.Text.Json;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest : CashFlowClassFixure
{
    private const string METHOD = "api/User";

    public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await DoPost(METHOD, request);

        result.StatusCode.ShouldBe(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        response.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
    }

    /*
    Códigos que estão comentados representam trechos de teste para
    CultureMiddleware que ainda não foi implementado por completo.
    */

    [Fact]
    /*
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    */
    public async Task Error_Empty_Name(/*string cultureInfo*/)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await DoPost(METHOD, request);

        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        // var expectedMessage = ResourceErrorMessages.ResourceMenager.GetString("NAME_EMPTY", new CultureInfo(cultureInfo));

        errors.Count().ShouldBe(1);
        errors.Any(error => error.GetString()!.Equals(ResourceErrorMessages.NAME_EMPTY)).ShouldBeTrue();
        // errors.Any(error => error.GetString()!.Equals(expectedMessage)).ShouldBeTrue();
    }
}