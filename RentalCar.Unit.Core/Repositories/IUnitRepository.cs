using RentalCar.Unit.Core.Entities;

namespace RentalCar.Unit.Core.Repositories;

public interface IUnitRepository
{
    Task<Units> Create(Units unit, CancellationToken cancellationToken);
    Task Update(Units unit, CancellationToken cancellationToken);
    Task Delete(Units unit, CancellationToken cancellationToken);
    Task<bool> IsUnitExist(string name, CancellationToken cancellationToken);
    Task<Units?> GetById(string id, CancellationToken cancellationToken);
    Task<List<Units>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
}