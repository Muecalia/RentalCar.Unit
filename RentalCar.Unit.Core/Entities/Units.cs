namespace RentalCar.Unit.Core.Entities;

public class Units
{
    public Units()
    {
        IsDeleted = false;
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
    }

    public string Id { get; set; }
    public required string Name { get; set; }
    public string Phone { get; set; }
    public required string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}