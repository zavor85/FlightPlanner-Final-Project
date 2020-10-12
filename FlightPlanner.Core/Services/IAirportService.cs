using System.Collections.Generic;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airports>
    {
        Task<List<Airports>> GetAirportByWord(string search);
    }
}
