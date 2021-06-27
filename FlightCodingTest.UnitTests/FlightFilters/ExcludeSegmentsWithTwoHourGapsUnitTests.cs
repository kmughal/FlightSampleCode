namespace FlightCodingTest.UnitTests.FlightFilters
{
    using FlightCodingTest.Contracts;
    using FlightCodingTest.Domain;
    using FlightCodingTest.FlightFilters;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class ExcludeSegmentsWithTwoHourGapsUnitTests
    {
        private Func<IList<ISegment>, bool> _excludeSegmentsWithMoreThanTwoHourGapFilter = new ExcludeSegmentsWithTwoHourGaps()
            .GetSegmentsFilterRule();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldNotThrowException_When_NullSegmentsArePassed()
        {
            // Act
            _excludeSegmentsWithMoreThanTwoHourGapFilter(null);

            // Assert
            Assert.Pass();
        }

        [Test]
        public void ShouldNotIncludeSegmentsWithMoreThan2HourGapBetweenArrivalDeparture_When_ThisFilterIsApplied()
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
                DepartureDate = DateTime.UtcNow.AddHours(4)
            };

            var segments = new List<ISegment> { segment1, segment2 };

            // Act
            var actual = _excludeSegmentsWithMoreThanTwoHourGapFilter(segments);

            // Assert
            Assert.IsTrue(actual);
        }
    }
}