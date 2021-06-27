namespace FlightCodingTest.UnitTests.FlightFilters
{
    using FlightCodingTest.Contracts;
    using FlightCodingTest.Domain;
    using FlightCodingTest.FlightFilters;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class FlightBuilderServiceUnitTests
    {
        private Func<IList<ISegment>, bool> _excludeFlightsDepartedInPastFilter = new ExcludeSegmentsDepartedInPast()
            .GetSegmentsFilterRule();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldNotThrowException_When_NullSegmentsArePassed()
        {
            // Act
            _excludeFlightsDepartedInPastFilter(null);

            // Assert
            Assert.Pass();
        }

        [Test]
        public void ShouldNotIncludePastDepartedSegments_When_ThisFilterIsApplied()
        {
            // Arrange
            var segment1 = new Segment
            {
                ArrivalDate = DateTime.UtcNow.AddHours(3),
                DepartureDate = DateTime.UtcNow.AddMinutes(30)
            };
            var segment2 = new Segment
            {
                ArrivalDate = DateTime.UtcNow.AddHours(6),
                DepartureDate = DateTime.UtcNow.AddHours(-1)
            };

            var segments = new List<ISegment> { segment1, segment2 };

            // Act
            var actual = _excludeFlightsDepartedInPastFilter(segments);

            // Assert
            Assert.IsTrue(actual);
        }
    }
}