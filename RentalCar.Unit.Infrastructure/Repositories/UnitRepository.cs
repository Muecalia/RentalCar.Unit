using Microsoft.EntityFrameworkCore;
using RentalCar.Unit.Core.Entities;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Infrastructure.Persistence;

namespace RentalCar.Unit.Infrastructure.Repositories;

public class UnitRepository : IUnitRepository
{
    private readonly UnitContext _context;

    public UnitRepository(UnitContext context)
    {
        _context = context;
    }

    public async Task<Units> Create(Units unit, CancellationToken cancellationToken)
    {
        _context.Units.Add(unit);
        await _context.SaveChangesAsync(cancellationToken);
        return unit;
    }

    public async Task Delete(Units unit, CancellationToken cancellationToken)
    {
        unit.IsDeleted = true;
        unit.DeletedAt = DateTime.UtcNow;
        _context.Units.Update(unit);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Units>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Units
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Units?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _context.Units.FirstOrDefaultAsync(c => !c.IsDeleted && string.Equals(c.Id, id), cancellationToken);
    }

    public async Task<bool> IsUnitExist(string name, CancellationToken cancellationToken)
    {
        return await _context.Units.AnyAsync(c => string.Equals(c.Name, name), cancellationToken);
    }

    public async Task Update(Units unit, CancellationToken cancellationToken)
    {
        unit.UpdatedAt = DateTime.UtcNow;
        _context.Units.Update(unit);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
}