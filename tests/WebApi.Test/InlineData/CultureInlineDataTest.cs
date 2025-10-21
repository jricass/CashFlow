using System.Collections;

namespace WebApi.Test.InlineData;

public class CultureInlineDataTest : IEnumerable<Object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "fr" };
        yield return new object[] { "en" };
        yield return new object[] { "pt-Br" };
        yield return new object[] { "pt-PT" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}