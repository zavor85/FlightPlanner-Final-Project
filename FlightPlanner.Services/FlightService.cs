using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.ResponseModels;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.Ajax.Utilities;


namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await Query().ToListAsync();
        }

        public async Task<Flight> AddFlight(Flight flight)
        {
            var addFlight = _ctx.Set<Flight>().Add(flight);
            await _ctx.SaveChangesAsync();
            return addFlight;
        }

        public async Task<Flight> GetFlightById(int id)
        {
            var flight = await _ctx.Set<Flight>().SingleOrDefaultAsync(f => f.Id == id);
            return flight;
        }

        public async Task<Flight> DeleteFlightById(int id)
        {
            var flight = await _ctx.Set<Flight>().SingleOrDefaultAsync(f => f.Id == id);
            if (flight != null)
            {
                var deletedFlight = _ctx.Set<Flight>().Remove(flight);
                await _ctx.SaveChangesAsync();
                return deletedFlight;
            }
            return null;
        }

        public async Task DeleteFlight()
        {
            _ctx.Set<Airports>().RemoveRange(_ctx.Set<Airports>());
            _ctx.Set<Flight>().RemoveRange(_ctx.Set<Flight>());
            await _ctx.SaveChangesAsync();
        }

        public async Task<bool> IsValidFlight(Flight flight)
        {
            if (flight.Carrier == null ||
                flight.From == null ||
                flight.To == null ||
                flight.Carrier == String.Empty ||
                flight.From.Country == null ||
                flight.From.Country == String.Empty ||
                flight.To.Country == null ||
                flight.To.Country == String.Empty ||
                flight.DepartureTime == null ||
                flight.ArrivalTime == null ||
                flight.From.Airport == flight.To.Airport ||
                flight.From.Airport == flight.To.Airport.ToUpper().Trim() ||
                flight.DepartureTime == flight.ArrivalTime ||
                Convert.ToDateTime(flight.DepartureTime) >
                Convert.ToDateTime(flight.ArrivalTime))
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> IsFlightConflict(Flight flight)
        {
            foreach (var fly in _ctx.Set<Flight>().ToList())
            {
                if (
                    fly.From.Airport == flight.From.Airport &&
                    fly.From.Country == flight.From.Country &&
                    fly.From.City == flight.From.City &&
                    fly.To.Airport == flight.To.Airport &&
                    fly.To.Country == flight.To.Country &&
                    fly.To.City == flight.To.City &&
                    fly.Carrier == flight.Carrier &&
                    fly.DepartureTime == flight.DepartureTime &&
                    fly.ArrivalTime == flight.ArrivalTime)
                {
                    return await Task.FromResult(true);
                }
            }
            return await Task.FromResult(false);
        }

        public async Task<PageResult> FindResult(SearchFlightRequest req)
        {
            var findFlight = await _ctx.Set<Flight>()
                .Where(flight => flight.From.Airport == req.From &&
                                 flight.To.Airport == req.To &&
                                 flight.DepartureTime.Substring(0, 10) == req.DepartureDate).ToListAsync();
            var flightResponse = findFlight.DistinctBy(f => new 
            {
                f.From.Airport,
                f.From.City,
                f.From.Country,
                f.Carrier,
                f.DepartureTime,
                f.ArrivalTime
            }).ToList();
;            return new PageResult()
            {
                Page = flightResponse.Count(),
                TotalItems = flightResponse.Count(),
                Items = flightResponse
            };
        }
    }
}
