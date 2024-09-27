namespace CarWorkshop.Domain.Tests.Entities;

using System;
using FluentAssertions;
using Xunit;
using CarWorkshop = Domain.Entities.CarWorkshop;

public class CarWorkshopTest
{

    [Fact]
    public void EncodeName_ShouldSetEncodedName()
    {
        var carWorkshop = new CarWorkshop { Name = "Test Workshop" };
        carWorkshop.EncodeName();

        carWorkshop.EncodedName.Should().Be("test-workshop");
    }

    [Fact]
    public void EncodeName_ForNull_ShouldThrowArgumentNullException()
    {
        var carWorkshop = new CarWorkshop();
        var action = () => carWorkshop.EncodeName();

        action.Invoking(a => a.Invoke()).Should().Throw<NullReferenceException>();
    }
}