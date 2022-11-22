using airtek.flight;

namespace airtek.shipping;

public class ShippingService
{
    public virtual async Task<List<ScheduledShippingOrder>> ScheduleAsync(List<ShippingOrder> orders, ShippingOptions options = default, CancellationToken cancelTkn = default)
    {
        if (options.PlaneCount < 1 || options.MaxBoxCountPerPlane < 1 || !options.IncludedDestinations.Any())
            return new List<ScheduledShippingOrder>();

        return await Task.Run(() =>
        {
            var excludedOrders = orders.Where(o => !options.IncludedDestinations.Contains(o.Destination)).ToList();
            var toBeProcessedOrders = orders.Except(excludedOrders).ToList();
            var processedOrdersCount = 0;
            var processedOrderBatches = new List<List<ShippingOrder>>();

            while (processedOrdersCount < toBeProcessedOrders.Count)
            {
                var groupBy = toBeProcessedOrders.Except(processedOrderBatches.SelectMany(o => o))
                                            .GroupBy(o => o.Destination)
                                            .ToDictionary(g => g.Key, g => g.ToList());

                if (!groupBy.Any())
                    break;

                var batchProcessed = groupBy.First().Value.Chunk(options.MaxBoxCountPerPlane).First().ToList();
                processedOrderBatches.Add(batchProcessed);

                processedOrdersCount += batchProcessed.Count;
            }

            var scheduledShippingOrders = new List<ScheduledShippingOrder>();

            for (var i = 0; i < processedOrderBatches.Count; i++)
            {
                var flightNumber = i + 1;
                var planeId = flightNumber % options.PlaneCount;
                var departureDay = (int)Math.Ceiling(flightNumber / (double)options.PlaneCount);

                for (var j = 0; j < processedOrderBatches[i].Count; j++)
                {
                    var so = processedOrderBatches[i][j];

                    scheduledShippingOrders.Add(new ScheduledShippingOrder
                    {
                        Flight = new Flight
                        {
                            Number = flightNumber,
                            Departure = so.Departure,
                            DepartureDay = departureDay,
                            Destination = so.Destination,
                            PlaneId = planeId
                        },
                        Order = so
                    });
                }
            }

            foreach (var o in excludedOrders)
                scheduledShippingOrders.Add(new ScheduledShippingOrder { Order = o });

            return scheduledShippingOrders;
        });
    }
}
