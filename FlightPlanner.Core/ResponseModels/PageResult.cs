using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.ResponseModels
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items = new List<Flight>();
    }
}
