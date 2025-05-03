using Task1.Utils;

namespace TestsForTask1;

public class TestTaskTest1
{
    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("aaabbcccdde", "a3b2c3d2e")]
    [InlineData("abc", "abc")]
    [InlineData("aaaaa", "a5")]
    [InlineData("zzzzzzzzzz", "z10")] 
    [InlineData("abbbbbbbbbba", "ab10a")] 
    [InlineData("aabbccddeeff", "a2b2c2d2e2f2")] 
    [InlineData("aabccbaa", "a2bc2ba2")]
    [InlineData(null, "")] 
    [InlineData("ααββγγ", "α2β2γ2")] 
    public void Compress_ValidInput_ReturnsCorrectResult(string input, string expected)
    {
        var result = StringCompressor.Compress(input);
        Assert.Equal(expected, result);
    }
    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("a3b2c3d2e", "aaabbcccdde")]
    [InlineData("abc", "abc")]
    [InlineData("a5", "aaaaa")]
    [InlineData("ab10a", "abbbbbbbbbba")]
    [InlineData("a2bc2ba2", "aabccbaa")] 
    [InlineData("α2β2γ2", "ααββγγ")]
    public void Decompress_ValidInput_ReturnsCorrectResult(string input, string expected)
    {
        var result = StringCompressor.Decompress(input);
        Assert.Equal(expected, result);
    }
  
}
