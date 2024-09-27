namespace CarWorkshop.Application.Tests.ApplicationUser;

using System.Collections.Generic;
using System.Security.Claims;
using Application.ApplicationUser;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

public class UserContextTest
{

    [Fact]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@test.com"),
            new(ClaimTypes.Role, "Admin"),
            new(ClaimTypes.Role, "User"),
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext { User = user });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        var currentUser = userContext.GetCurrentUser();

        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder("Admin", "User");
    }
}