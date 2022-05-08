using System.Collections.Generic;

namespace AzsCompany.Domain
{
    public class CompanyCar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumberCar { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Payed> Payeds { get; set; }
    }
}