using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.Core.Services;

namespace FlightPlanner_SOLID.Controllers
{
    public class TestApiController : BasicApiController
    {
        public TestApiController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
        }

        [HttpPost, Route("testing-api/clear")]
        public async Task<IHttpActionResult> ClearFlights()
        {
            await _flightService.DeleteFlight();
            return Ok();
        }
    }
}
