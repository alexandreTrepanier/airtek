namespace airtek.flight;

public class FlightSchedule
{
    public List<Flight> Flights { get; set; } = new();
    public override string ToString() => string.Join(Environment.NewLine, Flights.Select(f => f.ToString()));
}
