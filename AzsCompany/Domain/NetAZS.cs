using System;
using System.Collections.Generic;

namespace AzsCompany.Domain
{
    public class NetAZS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AzsId { get; set; }
        public Azs Azs { get; set; }
        public ICollection<Payed> Payeds { get; set; }
    }
}