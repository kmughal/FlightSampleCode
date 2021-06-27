namespace FlightCodingTest.IRepositories
{
    using FlightCodingTest.Contracts;
    using System.Collections.Generic;

    public interface IFlightRepository
    {
        IList<IFlight> GetFlights();
    }
}