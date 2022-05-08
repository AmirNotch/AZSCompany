using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzsCompany.Domain
{
    public class Payed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int NetAzsId { get; set; }
        public NetAZS NetAzs { get; set; }
        
        public int CompanyCarId { get; set; }
        public CompanyCar CompanyCar { get; set; }
        
        public int OilId { get; set; }
        public Oil Oil { get; set; }
        
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int Money { get; set; }
        
        public int Count { get; set; }
        
        public string DriverPayed { get; set; }

        public string AZSApplied { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;


        // public Payed(int companyId, int driverId, int azsId, 
        //     int oilId, int money, int count, 
        //     string driverPayed, string azsApplied)
        // {
        //     CompanyId = companyId;
        //     DriverId = driverId;
        //     AzsId = azsId;
        //     OilId = oilId;
        //     Money = money;
        //     Count = count;
        //     DriverPayed = driverPayed;
        //     AZSApplied = azsApplied;
        // }
    }
}