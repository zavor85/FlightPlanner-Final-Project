using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Data.Migrations;

namespace FlightPlanner.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("flight-planner")
        {
            Database.SetInitializer<FlightPlannerDbContext>(null);
            Database.SetInitializer<FlightPlannerDbContext>(new MigrateDatabaseToLatestVersion<FlightPlannerDbContext, Configuration>());
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airports> Airports { get; set; }
    }
}
