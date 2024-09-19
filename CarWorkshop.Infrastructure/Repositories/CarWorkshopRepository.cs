using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;

namespace CarWorkshop.Infrastructure.Repositories
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    internal class CarWorkshopRepository : ICarWorkshopRepository
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopRepository(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(CarWorkshop carWorkshop)
        {
            _dbContext.Add(carWorkshop);
            await _dbContext.SaveChangesAsync();
        }

        public Task<CarWorkshop?> GetByName(string name) =>
            _dbContext.CarWorkshops.FirstOrDefaultAsync(w => w.Name.ToLower() == name.ToLower());

        public async Task<IEnumerable<CarWorkshop>> GetAll() => await _dbContext.CarWorkshops.ToListAsync();

        public async Task<CarWorkshop> GetByEncodedName(string encodedName) =>
            await _dbContext.CarWorkshops.FirstAsync(w => w.EncodedName == encodedName);

        public async Task Update(CarWorkshop carWorkshop)
        {
            var carWorkshopToRemove = _dbContext.CarWorkshops.FirstAsync(w => w.Name == carWorkshop.Name);
            _dbContext.Remove(carWorkshopToRemove);
            _dbContext.Add(carWorkshop);


            _dbContext.Update(carWorkshop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
