using System.Web.Http;
using AutoMapper;
using FlightPlanner.Core.Services;

namespace FlightPlanner_SOLID.Controllers
{
    public class BasicApiController : ApiController
    {
        protected readonly IFlightService _flightService;
        protected readonly IMapper _mapper;

        public BasicApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }
    }
}
