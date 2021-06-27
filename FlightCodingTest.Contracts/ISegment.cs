namespace FlightCodingTest.Contracts
{
    using System;

    public interface ISegment
    {
        DateTime DepartureDate { get; set; }
        DateTime ArrivalDate { get; set; }
    }
}