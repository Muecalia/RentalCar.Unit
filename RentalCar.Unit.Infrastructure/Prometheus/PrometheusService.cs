using System.Diagnostics.Metrics;
using RentalCar.Unit.Core.Services;

namespace RentalCar.Unit.Infrastructure.Prometheus;

public class PrometheusService : IPrometheusService
{
    private readonly Meter _meter;
    private readonly Counter<int> _counter;
    private readonly ObservableGauge<int> _totalOrderGauge;
    
    public PrometheusService()
    {
        _meter = new Meter("RentalCar.Unit");
        _counter = _meter.CreateCounter<int>("Unit_total_request", "status_code");
        //_totalOrderGauge = _meter.CreateObservableGauge("total_orders", () => new Measurement<int>() )
    }
    
    public void AddUnitCounter(string statusCode)
    {
        _counter.Add(1, KeyValuePair.Create<string, object?>("Unit_error_code",statusCode));
    }

    public void AddNewUnitCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddDeleteUnitCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddUpdateUnitCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddFindByIdUnitCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddFindAllUnitsCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }
    
}