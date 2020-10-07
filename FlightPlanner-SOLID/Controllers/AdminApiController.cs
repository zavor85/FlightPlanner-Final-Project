using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner_SOLID.Attributes;
using FlightPlanner_SOLID.Models;

namespace FlightPlanner_SOLID.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : BasicApiController
    {
        public AdminApiController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
        }

        [HttpGet, Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var flight = await _flightService.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(flight, new FlightResponse()));
        }

        [HttpGet, Route("admin-api/get/flights/")]
        public async Task<IHttpActionResult> GetFlights()
        {
            var flights = await _flightService.GetFlights();
            return Ok(flights.Select(f => _mapper.Map<FlightResponse>(f)).ToList());
        }

        [HttpPut, Route("admin-api/flights/")]
        public async Task<IHttpActionResult> Put(Flight flight)
        {
            if (await _flightService.IsValidFlight(flight) == false)
            {
                return BadRequest();
            }

            if (await _flightService.IsFlightConflict(flight))
            {
                return Conflict();
            }

            var newFlight = await _flightService.AddFlight(flight);
            var mappedFlight = _mapper.Map<FlightResponse>(newFlight);
            return Created("admin-api/flights/", mappedFlight);
        }

        [HttpDelete, Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _flightService.DeleteFlightById(id);
            return Ok();
        }
    }
}
