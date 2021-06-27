namespace FlightCodingTest.Contracts
{
    using System.Collections.Generic;

    public interface IFlightBuilderService
    {
        List<IFlight> GetFlights(IList<ISegmentFilterRule> segmentFilterRules = null);
    }
}