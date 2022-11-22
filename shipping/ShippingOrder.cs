using airtek.flight;

namespace airtek.shipping;

public record struct ShippingOrder(string Id, int OrderNumber, IAtaCode Departure, IAtaCode Destination)
{
}
