using airtek.shipping;
using airtek.flight;

var appProcessPath = Path.GetDirectoryName(Environment.ProcessPath);
var cts = new CancellationTokenSource();
var cancelTkn = cts.Token;

Console.CancelKeyPress += (s, e) =>
{
    cts.Cancel();
    e.Cancel = true;
};

try
{
    while (true)
    {
        Console.Write("Enter user story # (1 or 2): ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
            continue;

        switch (choice)
        {
            case 1: RunUserStory1(); break;
            case 2: await RunUserStory2(cancelTkn); break;
        }

        Console.WriteLine();
        Console.WriteLine();
    }
}
catch (Exception e)
{
    await Console.Error.WriteAsync(e.Message);
}

void RunUserStory1()
{
    var fs = TestData.GetFlightSchedule();
    var fsp = new FlightSchedulePrinter();
    Console.Write(fsp.Print(fs));
}

async Task RunUserStory2(CancellationToken cancelTkn)
{
    if (appProcessPath == null)
        throw new Exception("Failed to retrieve executing assembly location");

    var defaultRelativeFileName = @"data\orders.json";
    var defaultOrderFileName = Path.Combine(appProcessPath, defaultRelativeFileName);

    Console.WriteLine(@$"Enter shipping orders filename or keep blank for default: {defaultOrderFileName}");
    Console.Write("filename: ");

    var orderFileName = Console.ReadLine();
    Console.WriteLine();

    if (string.IsNullOrWhiteSpace(orderFileName))
        orderFileName = defaultOrderFileName;

    var orders = await new ShippingOrdersSerializer().DeserializeAsync(orderFileName, IAtaCode.YUL, cancelTkn);

    var shippingOptions = new ShippingOptions
    {
        IncludedDestinations = new List<IAtaCode> { IAtaCode.YYZ, IAtaCode.YVR, IAtaCode.YYC },
        MaxBoxCountPerPlane = 20,
        PlaneCount = 3
    };

    var scheduledOrders = await new ShippingService().ScheduleAsync(orders, shippingOptions, cancelTkn);
    Console.Write(new ShippingOrderPrinter().Print(scheduledOrders, o => o.Order.OrderNumber));
}