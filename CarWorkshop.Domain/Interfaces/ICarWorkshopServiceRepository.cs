namespace CarWorkshop.Infrastructure.Repositories;

using Domain.Entities;

public interface ICarWorkshopServiceRepository
{
    Task Create(CarWorkshopService carWorkshopService);
    Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName);
}