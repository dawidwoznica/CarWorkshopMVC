namespace CarWorkshop.Application.CarWorkshopService.Queries.GetAllCarWorkshopsByEncodedNameQuery;

using MediatR;

public class GetAllCarWorkshopServicesByEncodedNameQuery : IRequest<IEnumerable<CarWorkshopServiceDto>>
{
    public string EncodedName { get; set; } = default!;
}