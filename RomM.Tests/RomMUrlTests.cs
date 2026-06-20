using RomM.Games;
using Xunit;

namespace RomM.Tests
{
    public class RomMUrlTests
    {
        [Theory]
        [InlineData("https://h", "api/roms", "https://h/api/roms")]
        [InlineData("https://h/", "api/roms", "https://h/api/roms")]
        [InlineData("https://h", "/api/roms", "https://h/api/roms")]
        [InlineData("https://h/", "/api/roms", "https://h/api/roms")]
        [InlineData("https://h///", "///api/roms", "https://h/api/roms")]
        public void Combine_joins_with_exactly_one_slash(string baseUrl, string relative, string expected)
        {
            Assert.Equal(expected, RomMUrl.Combine(baseUrl, relative));
        }

        [Fact]
        public void Null_relative_yields_base_with_trailing_slash()
        {
            Assert.Equal("https://h/", RomMUrl.Combine("https://h", null));
        }

        [Theory]
        [InlineData("https://h", "roms/2/32/fanart/fanart.png", "https://h/assets/romm/resources/roms/2/32/fanart/fanart.png")]
        [InlineData("https://h/", "/roms/2/32/fanart/fanart.png", "https://h/assets/romm/resources/roms/2/32/fanart/fanart.png")]
        public void Resource_prefixes_the_resource_mount(string baseUrl, string relative, string expected)
        {
            Assert.Equal(expected, RomMUrl.Resource(baseUrl, relative));
        }
    }
}
