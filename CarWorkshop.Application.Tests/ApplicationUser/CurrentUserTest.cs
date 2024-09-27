using CarWorkshop.Application.ApplicationUser;

namespace CarWorkshop.Application.Tests.ApplicationUser;

using FluentAssertions;
using Xunit;

public class CurrentUserTest
{

    [Fact]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue()
    {
        var user = new CurrentUser("1", "test@test.com", ["Admin", "User"]);
        var result = user.IsInRole("User");

        result.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithNotMatchingRole_ShouldReturnFalse()
    {
        var user = new CurrentUser("1", "test@test.com", ["Admin", "User"]);
        var result = user.IsInRole("Tester");

        result.Should().BeFalse();
    }
}