namespace CarWorkshop.Domain.Interfaces
{
    using Entities;

    public interface ICarWorkshopRepository
    {
        Task Create(CarWorkshop carWorkshop);
        Task<CarWorkshop?> GetByName(string name);
        Task<IEnumerable<CarWorkshop>> GetAll();
        Task<CarWorkshop> GetByEncodedName(string encodedName);
        Task Update(CarWorkshop carWorkshop);
        Task Commit();
    }
}
