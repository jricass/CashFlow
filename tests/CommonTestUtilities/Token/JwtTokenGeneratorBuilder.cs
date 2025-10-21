using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Token;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(accessTokenGenerator => accessTokenGenerator.Generate(It.IsAny<User>())).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImpvYW8iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiI1ZWUwOGRlYy0wNzA3LTQ5ZWYtOGIwMS0yYjk2Y2VlZDYzZDQiLCJuYmYiOjE3NTQ5OTg1MDcsImV4cCI6MTc1NTA1ODUwNywiaWF0IjoxNzU0OTk4NTA3fQ.n5xjSbjrZSwwCdp39G6EJYsoxaEL5yljByRcrC_MivY");

        return mock.Object;
    }
}