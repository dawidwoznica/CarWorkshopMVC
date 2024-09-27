namespace CarWorkshop.Application.Tests.Mappings;

using Application.ApplicationUser;
using Application.Mappings;
using AutoMapper;
using CarWorkshop;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

public class CarWorkshopMappingProfileTest
{

    [Fact]
    public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
    {
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("1", "test@test.com", ["Moderator"]));

        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

        var mapper = configuration.CreateMapper();

        var dto = new CarWorkshopDto()
        {
            City = "test",
            PhoneNumber = "1234",
            PostalCode = "1234",
            Street = "test",
        };

        var result = mapper.Map<CarWorkshop>(dto);

        result.Should().NotBeNull();
        result.ContactDetails.Should().NotBeNull();
        result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);
        result.ContactDetails.Street.Should().Be(dto.Street);
        result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
        result.ContactDetails.City.Should().Be(dto.City);
    }

    [Fact]
    public void MappingProfile_ShouldMapCarWorkshopToCarWorkshopDto()
    {
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("1", "test@test.com", ["Moderator"]));

        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

        var mapper = configuration.CreateMapper();

        var carWorkshop = new CarWorkshop()
        {
            Id = 1,
            CreatedById = "1",
            ContactDetails = new CarWorkshopContactDetails()
            {
                City = "test",
                PhoneNumber = "1234",
                PostalCode = "1234",
                Street = "test",
            }
        };

        var result = mapper.Map<CarWorkshopDto>(carWorkshop);

        result.Should().NotBeNull();
        result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
        result.Street.Should().Be(carWorkshop.ContactDetails.Street);
        result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
        result.City.Should().Be(carWorkshop.ContactDetails.City);
        result.IsEditable.Should().BeTrue();
    }
}