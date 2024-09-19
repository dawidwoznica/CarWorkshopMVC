namespace CarWorkshop.Application.CarWorkshopService.Commands;

using MediatR;

public class CreateCarWorkshopServiceCommand : CarWorkshopServiceDto, IRequest
{
    public string CarWorkshopEncodedName { get; set; } = default!;
}