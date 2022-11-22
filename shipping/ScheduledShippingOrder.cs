using airtek.flight;

namespace airtek.shipping;

public class ScheduledShippingOrder
{
    public Flight? Flight { get; set; }
    public ShippingOrder Order { get; set; }

    public override string ToString()
    {
        if (!Flight.HasValue)
            return $"order: {Order.Id}, flightNumber: not scheduled";
        else
            return $"order: {Order.Id}, flightNumber: {Flight.Value.Number}, departure: {Order.Departure}, arrival: {Order.Destination}, day: {Flight.Value.DepartureDay}";
    }
}
