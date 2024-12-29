namespace RentalCar.Unit.Core.Services;

public interface IPrometheusService
{
    void AddNewUnitCounter(string statusCodes);
    void AddDeleteUnitCounter(string statusCodes);
    void AddUpdateUnitCounter(string statusCodes);
    void AddFindByIdUnitCounter(string statusCodes);
    void AddFindAllUnitsCounter(string statusCodes);
}