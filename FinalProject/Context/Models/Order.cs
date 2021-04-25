using System;

namespace FinalProject.Context.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TilDate { get; set; }
        public bool HasGPS { get; set; }
        public bool HasAdditionalDriver { get; set; }
        public bool HasKidChair { get; set; }
        public bool HasInsurance { get; set; }
        
        public int CarId { get; set; }
        public string UserId { get; set; }
        
        
    }
}