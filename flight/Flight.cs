using System.Text.Json.Serialization;

namespace airtek.flight;

public record struct Flight(int Number, int DepartureDay, int PlaneId, IAtaCode Departure, IAtaCode Destination)
{
    public override string ToString() => $@"Flight: {Number}, departure: {Departure}, arrival: {Destination}, day: {DepartureDay}";
}
