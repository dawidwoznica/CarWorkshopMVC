namespace CarWorkshop.MVC.Tests.Controllers;

using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.CarWorkshop;
using Application.CarWorkshop.Queries.GetAllCarWorkshops;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

public class CarWorkshopControllerTest(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Index_ReturnsViewWithExpectedContent_ForExistingWorkshops()
    {
        var carWorkshops = new List<CarWorkshopDto>
        {
            new() { Name = "Workshop 1", },
            new() { Name = "Workshop 2", },
            new() { Name = "Workshop 3", },
        };

        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarWorkshopsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(carWorkshops);

        var client = factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services.AddScoped(_ => mediatorMock.Object)))
            .CreateClient();

        var response = await client.GetAsync($"/CarWorkshop/Index");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("<h1>Car Workshops</h1>")
            .And.Contain("Workshop 1")
            .And.Contain("Workshop 2")
            .And.Contain("Workshop 3");
    }

    [Fact]
    public async Task Index_ReturnsEmptyView_ForNoWorkshopsExist()
    {
        var carWorkshops = new List<CarWorkshopDto>();
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarWorkshopsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(carWorkshops);

        var client = factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                    services.AddScoped(_ => mediatorMock.Object)))
            .CreateClient();

        var response = await client.GetAsync($"/CarWorkshop/Index");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();

        content.Should().NotContain("<div class=\"card m-3\"");
    }
}