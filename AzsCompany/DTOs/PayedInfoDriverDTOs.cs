using System;
using AzsCompany.Domain;

namespace AzsCompany.DTOs
{
    public class PayedInfoDriverDTOs
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string NumberCar { get; set; }

        public string AzsName { get; set; }
        public string Oil { get; set; }

        public int Money { get; set; }
        
        public int Count { get; set; }
        
        public string DriverPayed { get; set; }

        public string AZSApplied { get; set; }
        public DateTime Date { get; set; }
    }
}