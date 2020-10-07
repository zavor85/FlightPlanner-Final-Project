using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airports>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public async Task<List<Airports>> GetAirportByWord(string search)
        {
            var resultAirport = await _ctx.Set<Airports>()
                .Where(fly => fly.Airport.ToLower().Contains(search.ToLower().Trim()) ||
                              fly.City.ToLower().Contains(search.ToLower().Trim()) ||
                              fly.Country.ToLower().Contains(search.ToLower().Trim())).ToListAsync();
            return new List<Airports>()
            {
                new Airports()
                {
                    Country = resultAirport.Last().Country,
                    City = resultAirport.Last().City,
                    Airport = resultAirport.Last().Airport
                }
            };
        }
    }
}
