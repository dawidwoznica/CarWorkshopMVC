using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshopsByEncodedName
{
    public class GetAllCarWorkshopsByEncodedNameQuery(string encodedName) : IRequest<CarWorkshopDto>
    {
        public string EncodedName { get; set; } = encodedName;
    }
}
