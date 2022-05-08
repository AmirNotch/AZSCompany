using System;
using System.Collections.Generic;

namespace AzsCompany.Domain
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long NumberIIN { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public long NumberPhone { get; set; }
        public ICollection<Payed> Payeds { get; set; }
        // public ICollection<NumberPhone> Phones { get; set; }
    }
}