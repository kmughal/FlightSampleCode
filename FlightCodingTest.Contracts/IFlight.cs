namespace FlightCodingTest.Contracts
{
    using System.Collections.Generic;
    
    public interface IFlight
    {
        public List<ISegment> Segments { get; set; }
    }
}