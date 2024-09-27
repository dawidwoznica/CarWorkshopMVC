namespace CarWorkshop.MVC.Tests.Controllers;

using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class HomeControllerTest(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task About_ReturnsViewWithModel()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/Home/About");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().Contain("<title>About - CarWorkshop.MVC</title>")
            .And.Contain("<li>Me</li>")
            .And.Contain("<li>I</li>")
            .And.Contain("<li>So me!</li>");;
    }
}