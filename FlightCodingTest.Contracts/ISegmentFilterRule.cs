namespace FlightCodingTest.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ISegmentFilterRule
    {
        Func<IList<ISegment>, bool> GetSegmentsFilterRule();
    }
}