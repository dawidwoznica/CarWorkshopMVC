namespace CarWorkshop.Infrastructure.Repositories;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class CarWorkshopServiceRepository(CarWorkshopDbContext dbContext) : ICarWorkshopServiceRepository
{
    public async Task Create(CarWorkshopService carWorkshopService)
    {
        dbContext.Service.Add(carWorkshopService);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName)
        => await dbContext.Service.Where(s => s.CarWorkshop.EncodedName == encodedName).ToListAsync();
}