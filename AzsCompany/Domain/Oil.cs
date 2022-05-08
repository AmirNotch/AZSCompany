using System.Collections.Generic;

namespace AzsCompany.Domain
{
    public class Oil
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Payed> Payeds { get; set; }
    }
}