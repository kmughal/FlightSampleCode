namespace FlightCodingTest.FlightFilters
{
    using FlightCodingTest.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExcludeSegmentsDepartedInPast : ISegmentFilterRule
    {
        public Func<IList<ISegment>, bool> GetSegmentsFilterRule() =>
            segments =>
            {
                if (segments is null) return default;
                return segments.Any(segment => segment.DepartureDate < DateTime.UtcNow);
            };
    }
}