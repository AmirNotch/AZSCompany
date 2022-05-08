using System;
using System.Collections.Generic;

namespace AzsCompany.Domain
{
    public class Azs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICollection<NetAZS> NetAzses { get; set; }
        public ICollection<Payed> Payeds { get; set; }
    }
}