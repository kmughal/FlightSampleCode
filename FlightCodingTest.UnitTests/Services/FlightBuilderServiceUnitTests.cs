namespace FlightCodingTest.UnitTests.Services
{
    using FlightCodingTest.Contracts;
    using FlightCodingTest.Domain;
    using FlightCodingTest.FlightFilters;
    using FlightCodingTest.IRepositories;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class FlightBuilderServiceUnitTests
    {
        [Test]
        public void ShouldNotThrowException_When_ThereAreNotFlights()
        {
            var mockFlightRepository = new Mock<IFlightRepository>();
            mockFlightRepository.Setup(m => m.GetFlights()).Returns(() => default);

            var _sut = new FlightBuilderService(mockFlightRepository.Object);
            var filters = new List<ISegmentFilterRule> { new ExcludeSegmentsWithArrivalBeforeDeparture() };
            var actual = _sut.GetFlights(filters);

            Assert.AreEqual(default, actual);
        }

        [Test]
        public void ShouldNotThrowException_When_ThereAreNoFilters()
        {
            var mockFlightRepository = new Mock<IFlightRepository>();
            mockFlightRepository.Setup(m => m.GetFlights()).Returns(() => default);

            var _sut = new FlightBuilderService(mockFlightRepository.Object);
            var actual = _sut.GetFlights(null);

            Assert.AreEqual(default, actual);
        }

        [Test]
        public void ShouldFilterFlights_When_ThereAreFilters()
        {
            var mockFlightRepository = new Mock<IFlightRepository>();

            var flight1 = new Flight
            {
                Segments = new List<ISegment>
                {
                    new Segment { ArrivalDate = DateTime.UtcNow, DepartureDate = DateTime.UtcNow.AddDays(-1) }
                }
            };
            var flight2 = new Flight
            {
                Segments = new List<ISegment>
                {
                    new Segment { ArrivalDate = DateTime.UtcNow, DepartureDate = DateTime.UtcNow.AddDays(1) }
                }
            };

            mockFlightRepository.Setup(m => m.GetFlights()).Returns(() => new List<IFlight> { flight1, flight2 });

            var _sut = new FlightBuilderService(mockFlightRepository.Object);
            var filters = new List<ISegmentFilterRule> { new ExcludeSegmentsDepartedInPast() };
            var actual = _sut.GetFlights(filters);

            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public void ShouldFilterFlights_When_MultipleAreApplied()
        {
            var mockFlightRepository = new Mock<IFlightRepository>();

            var flight1 = new Flight
            {
                Segments = new List<ISegment>
                {
                    new Segment { ArrivalDate = DateTime.UtcNow, DepartureDate = DateTime.UtcNow.AddDays(-1) }
                }
            };

            var flight2 = new Flight
            {
                Segments = new List<ISegment>
                {
                    new Segment { ArrivalDate = DateTime.UtcNow, DepartureDate = DateTime.UtcNow.AddHours(2) },
                     new Segment { ArrivalDate = DateTime.UtcNow.AddHours(2), DepartureDate = DateTime.UtcNow.AddHours(4) }
                }
            };

            mockFlightRepository.Setup(m => m.GetFlights()).Returns(() => new List<IFlight> { flight1, flight2 });

            var _sut = new FlightBuilderService(mockFlightRepository.Object);
            var filters = new List<ISegmentFilterRule> { new ExcludeSegmentsDepartedInPast(), new ExcludeSegmentsWithTwoHourGaps() };
            var actual = _sut.GetFlights(filters);

            Assert.AreEqual(0, actual.Count);
        }
    }
}