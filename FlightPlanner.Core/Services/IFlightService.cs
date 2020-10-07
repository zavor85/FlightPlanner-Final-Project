using System.Collections.Generic;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.ResponseModels;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Task<IEnumerable<Flight>> GetFlights();
        Task<Flight> AddFlight(Flight flight);
        Task<Flight> DeleteFlightById(int id);
        Task DeleteFlight();
        Task<PageResult> FindResult(SearchFlightRequest req);
        Task<bool> IsFlightConflict(Flight flight);
        Task<bool> IsValidFlight(Flight flight);
        Task<Flight> GetFlightById(int id);
    }
}
