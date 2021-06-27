namespace FlightCodingTest.Domain
{
    using FlightCodingTest.Contracts;
    using System;

    public class Segment : ISegment
    {
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public override bool Equals(object obj)
        {
            var segment = (ISegment)obj;
            return segment.ArrivalDate == ArrivalDate && segment.DepartureDate == DepartureDate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}