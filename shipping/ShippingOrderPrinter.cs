using System.Text;

namespace airtek.shipping;

public class ShippingOrderPrinter
{
    public virtual string Print(List<ScheduledShippingOrder> orders, Func<ScheduledShippingOrder, int> orderBy)
    {
        var sb = new StringBuilder();

        foreach (var so in orders.OrderBy(orderBy))
            sb.AppendLine(so.ToString());

        return sb.ToString();
    }
}
