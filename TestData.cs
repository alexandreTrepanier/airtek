using airtek.flight;

public static class TestData
{
    public static FlightSchedule GetFlightSchedule()
    {
        return new FlightSchedule
        {
            Flights = new() { new Flight
                      {
                          Number = 1,
                          DepartureDay = 1,
                          Departure = IAtaCode.YUL,
                          Destination = IAtaCode.YYZ,
                      },
                      new Flight
                      {
                          Number = 2,
                          DepartureDay = 1,
                          Departure = IAtaCode.YUL,
                          Destination = IAtaCode.YYC,
                      },
                      new Flight
                      {
                          Number = 3,
                          DepartureDay = 1,
                          Departure = IAtaCode.YUL,
                          Destination = IAtaCode.YVR,
                      },
                      new Flight
                      {
                          Number = 4,
                          DepartureDay = 2,
                          Departure = IAtaCode.YUL,
                          Destination = IAtaCode.YYZ,
                      },
                      new Flight
                      {
                          Number = 5,
                          DepartureDay = 2,
                          Departure = IAtaCode.YUL,
                          Destination = IAtaCode.YYC,
                      }, new Flight
                      {
                          Number = 6,
                          DepartureDay = 2,
                          Departure = IAtaCode.YUL,
                          Destination = IAtaCode.YVR,
                      } }
        };
    }
}