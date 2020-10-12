using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.Core.ResponseModels;
using FlightPlanner.Core.Services;
using FlightPlanner_SOLID.Models;

namespace FlightPlanner_SOLID.Controllers
{
    public class ClientApiController : BasicApiController
    {

        private readonly IAirportService _airportService;

        public ClientApiController(IFlightService flightService, IMapper mapper, IAirportService airportService) : base(flightService, mapper)
        {
            _airportService = airportService;
        }

        [HttpGet, Route("api/airports/")]
        public async Task<IHttpActionResult>  Get(string search)
        {
            var airport = await _airportService.GetAirportByWord(search);
            return  Ok(airport.Select(f => _mapper.Map<AirportResponse>(f)).ToList());
        }

        [HttpGet, Route("api/flights/{id}")]
        public async Task<IHttpActionResult>  GetById(int id)
        {
            var flightById = await _flightService.GetFlightById(id);
            if (flightById != null)
            {
                return Ok(_mapper.Map<FlightResponse>(flightById));
            }

            return NotFound();
        }

        [HttpPost, Route("api/flights/search")]
        public async Task<IHttpActionResult>  Post(SearchFlightRequest req)
        {
            if (req == null || req.From == req.To)
            {
                return BadRequest();
            }

            var pageResult = await _flightService.FindResult(req);
            return Ok(pageResult);
        }
    }
}
