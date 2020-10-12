using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner_SOLID.Models;

namespace FlightPlanner_SOLID
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AirportRequest, Airports>()
                    .ForMember(d => d.Id,
                        s => s.Ignore());
                cfg.CreateMap<Airports, AirportRequest>();
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
                cfg.CreateMap<AirportResponse, AirportRequest>()
                    .ForMember(d => d.Id,
                        opt =>
                            opt.Ignore());
                cfg.CreateMap<AirportRequest, AirportResponse>();
                cfg.CreateMap<FlightRequest, FlightResponse>();
                cfg.CreateMap<Airports, AirportResponse>();
                cfg.CreateMap<AirportResponse, Airports>()
                        .ForMember(m => m.Id,
                            opt => opt.Ignore());
                cfg.CreateMap<Flight, FlightResponse>();
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}