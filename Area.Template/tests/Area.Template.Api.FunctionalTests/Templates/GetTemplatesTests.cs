using System.Net;
using System.Text;
using Area.Template.Api.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace Area.Template.Api.FunctionalTests.Templates
{
    public class GetTemplatesTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        [Fact]
        public async Task GetFeatureFlagsTests_ShouldReturnNotFound_WhenRequestIsInvalid()
        {
            // Arrange
            var url = "api/templates";

            // Act
            HttpResponseMessage response = await HttpClient.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
