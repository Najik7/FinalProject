using System;

namespace FinalProject.ViewModels
{
    public class CreateOrderViewModel
    {
        public int CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TilDate { get; set; }
        public bool HasAdditionalDriver { get; set; }
        public bool HasKidChair { get; set; }
        public bool HasInsurance { get; set; }
        public string UserId { get; set; }
        
    }
}