namespace FlightCodingTest.Domain
{
    using FlightCodingTest.Contracts;
    using FlightCodingTest.IRepositories;
    using System.Collections.Generic;

    public class FlightBuilderService : IFlightBuilderService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightBuilderService(IFlightRepository flightRepositiry)
        {
            _flightRepository = flightRepositiry;
        }

        public List<IFlight> GetFlights(IList<ISegmentFilterRule> segmentFilterRules = null)
        {
            var flights = _flightRepository.GetFlights();
            if (flights is null) return default;

            if (segmentFilterRules is null) return (List<IFlight>)flights;

            var filteredFlights = new List<IFlight>();
            foreach (var flight in flights)
            {
                var segments = flight.Segments;
                var excludeFlight = RunFiltersAndDetermineIfFlightNeedsToBeExcluded(segmentFilterRules, segments);
                if (excludeFlight == false) filteredFlights.Add(flight);
            }
            return filteredFlights;
        }

        private bool RunFiltersAndDetermineIfFlightNeedsToBeExcluded(IList<ISegmentFilterRule> segmentFilterRules, List<ISegment> segments)
        {
            foreach (var segmentFilterRule in segmentFilterRules)
            {
                var rule = segmentFilterRule.GetSegmentsFilterRule();
                var excludeFlight = rule(segments) == false;
                if (excludeFlight) return true;
            }
            return false;
        }
    }
}