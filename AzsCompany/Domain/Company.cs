﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzsCompany.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Payed> Payeds { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public ICollection<CompanyCar> CompanyCars { get; set; }
    }
}