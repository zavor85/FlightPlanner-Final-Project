namespace FlightPlanner.Core.Models
{
    public class Flight : Entity
    {
        public virtual Airports From { get; set; }
        public virtual Airports To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}