namespace FlightCodingTest.Domain
{
    using FlightCodingTest.Contracts;
    using System.Collections.Generic;

    public class Flight : IFlight
    {
        public List<ISegment> Segments { get; set; }
    }
}