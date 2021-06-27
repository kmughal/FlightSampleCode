using FlightCodingTest.Contracts;
using FlightCodingTest.FlightFilters;
using FlightCodingTest.Infrastructure;
using System;
using System.Collections.Generic;
using static System.Console;

namespace Source.FlightCodingTest.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var provider = IoC.ConfigureServices();
            IFlightBuilderService flightBuilderService = (IFlightBuilderService)provider.GetService(typeof(IFlightBuilderService));
            var filters = new List<ISegmentFilterRule> {
                new ExcludeSegmentsDepartedInPast(),
                new ExcludeSegmentsWithArrivalBeforeDeparture(),
                new ExcludeSegmentsWithTwoHourGaps()
            };
            var flights = flightBuilderService.GetFlights(filters);
            var index = 0;
            WriteLine("After applying all the filters....");
            WriteLine("Total flights : {0}", flights.Count);

            foreach (var flight in flights)
            {
                WriteLine("Flight:{0}", index++);
                foreach (var segment in flight.Segments)
                {
                   WriteLine("Arrival Date:{0}, Deparrture:{1}", segment.ArrivalDate, segment.DepartureDate);
                }
            }
            ReadLine();
        }
    }
}