using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder(CarWorkshopDbContext dbContext)
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.CarWorkshops.Any())
                {
                    var carWorkshops = GetCarWorkshops();
                    dbContext.CarWorkshops.AddRange(carWorkshops);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public List<Domain.Entities.CarWorkshop> GetCarWorkshops()
        {
            var carWorkshops = new List<Domain.Entities.CarWorkshop>()
            {
                new()
                {
                    Name = "Mazda ASO",
                    Description = "Autoryzowany serwis Mazda",
                    ContactDetails = new CarWorkshopContactDetails()
                    {
                        City = "Kraków",
                        PhoneNumber = "+48698858452",
                        Street = "Szewska 2",
                        PostalCode = "30-001",
                    }
                }
            };

            carWorkshops.ForEach(w => w.EncodeName());

            return carWorkshops;
        }
    }
}
