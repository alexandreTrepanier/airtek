using airtek.flight;

namespace airtek.shipping;

public record struct ShippingOptions(int PlaneCount, int MaxBoxCountPerPlane)
{
    public List<IAtaCode> IncludedDestinations { get; init; } = new();
}
