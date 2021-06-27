using FlightCodingTest.Contracts;
using FlightCodingTest.Domain;
using FlightCodingTest.FlightFilters;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FlightCodingTest.UnitTests.FlightFilters
{
    public class ExcludeSegmentsWithArrivalBeforeDepartureUnitTests
    {
        private Func<IList<ISegment>, bool> _excludeSegmentsWithArrivalBeforeDepartureFilter = new ExcludeSegmentsWithArrivalBeforeDeparture()
            .GetSegmentsFilterRule();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Not_Throw_Exception_When_Null_Segments_Are_Passed()
        {
            // Act
            _excludeSegmentsWithArrivalBeforeDepartureFilter(null);

            // Assert
            Assert.Pass();
        }

        [Test]
        public void ShouldNotIncludeSegmentsWithDepartureDateBeforeArrivalDate_When_ThisFilterIsApplied()
        {
            // Arrange
            var segment1 = new Segment
            {
                ArrivalDate = DateTime.UtcNow.AddHours(1),
                DepartureDate = DateTime.UtcNow.AddHours(3)
            };
            var segment2 = new Segment
            {
                ArrivalDate = DateTime.UtcNow.AddHours(6),
                DepartureDate = DateTime.UtcNow.AddHours(3)
            };

            var segments = new List<ISegment> { segment1, segment2 };

            // Act
            var actual = _excludeSegmentsWithArrivalBeforeDepartureFilter(segments);

            // Assert
            Assert.IsTrue(actual);
        }
    }
}