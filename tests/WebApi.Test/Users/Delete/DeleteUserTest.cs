using System.Net;
using Shouldly;

namespace WebApi.Test.Users.Delete;

public class DeleteUserTest : CashFlowClassFixure
{
    private const string METHOD = "api/User";

    public readonly string _token;

    public DeleteUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(METHOD, _token);

        result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}
