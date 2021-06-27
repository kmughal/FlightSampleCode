namespace FlightCodingTest.FlightFilters
{
    using FlightCodingTest.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExcludeSegmentsWithArrivalBeforeDeparture : ISegmentFilterRule
    {
        public Func<IList<ISegment>, bool> GetSegmentsFilterRule() =>
             segments =>
             {
                 if (segments is null) return default;
                 return segments.Any(segment => segment.ArrivalDate < segment.DepartureDate);
             };
    }
}