namespace FlightCodingTest.FlightFilters
{
    using FlightCodingTest.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExcludeSegmentsWithTwoHourGaps : ISegmentFilterRule
    {
        public Func<IList<ISegment>, bool> GetSegmentsFilterRule() =>
            segments =>
            {
                if (segments is null) return default;

                var filteredSegments = new List<ISegment>();
                var segmentList = segments.ToList();
                for (var index = 0; index < segmentList.Count - 1; index++)
                {
                    var currentSegment = segmentList[index];
                    var nextSegment = segmentList[index + 1];
                    var currentSegmentArrivalDateTimeStamp = currentSegment.ArrivalDate;
                    var nextSegmentDepartureDateTimeStamp = nextSegment.DepartureDate;
                    var dateTimeStampDifference = (nextSegmentDepartureDateTimeStamp - currentSegmentArrivalDateTimeStamp).TotalHours;
                    if (dateTimeStampDifference > 2) return true;
                }

                return false;
            };
    }
}