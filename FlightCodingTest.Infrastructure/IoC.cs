namespace FlightCodingTest.Infrastructure
{
    using FlightCodingTest.Contracts;
    using FlightCodingTest.Domain;
    using FlightCodingTest.IRepositories;
    using FlightRepostiry.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class IoC
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceList = new ServiceCollection();
            serviceList.AddSingleton<IFlightBuilderService, FlightBuilderService>();
            serviceList.AddSingleton<IFlightRepository, FlightRepository>();
            return serviceList.BuildServiceProvider();
        }
    }
}