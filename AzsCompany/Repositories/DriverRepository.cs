using System;
using System.Collections.Generic;
using System.Linq;
using AzsCompany.DBContext;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzsCompany.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DataContext _context;

        public DriverRepository(DataContext context)
        {
            _context = context;
        }
        
        public IEnumerable<PayedInfoDriverNameDTOs> PayedInfoDriverDtOs(int driverId)
        {
            var result = (from p in _context.Payeds
                join cp in _context.Companies on p.CompanyId equals cp.Id
                join d in _context.NetAzses on p.NetAzsId equals d.Id
                join o in _context.Oils on p.OilId equals o.Id
                join cc in _context.CompanyCars on cp.Id equals cc.CompanyId
                join dr in _context.Drivers on p.DriverId equals dr.Id
                where dr.Id == driverId
                select new PayedInfoDriverNameDTOs()
                {
                      CompanyName = cp.Name,
                      NumberCar = cc.NumberCar,
                      AzsName = d.Name,
                      DriverName = dr.FirstName,
                      Count = p.Count,
                      Money = p.Money,
                      Oil = o.Name,
                      DriverPayed = p.DriverPayed,
                      AZSApplied = p.AZSApplied,
                      Date = p.Date,
                });

            return result;
        }

        public IEnumerable<AZSDriverDTOs> GetCompaniesDrivers(int driverId)
        {
            var result = (from c in _context.Companies
                join d in _context.Drivers on c.Id equals d.CompanyId
                join cp in _context.CompanyCars on c.Id equals cp.CompanyId
                where d.Id == driverId
                select new AZSDriverDTOs()
                {
                    NameCompany = c.Name,
                    NumberCar = cp.NumberCar,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                });

            return result;
        }

        public ActionResult<IEnumerable<Driver>> GetAuthDriver(DriverLoginDTOs driver)
        {
            var find = _context.Drivers.Where(x => x.NumberPhone == driver.NumberPhone && x.Password == driver.Password)
                .FirstOrDefault();

            if (find != null)
            {
                var result = (from d in _context.Drivers
                    where d.Id == find.Id
                    select new Driver()
                    {
                        Id = find.Id,
                        Password = find.Password,
                        FirstName = find.FirstName,
                        LastName = find.LastName,
                        CompanyId = find.CompanyId,
                        NumberIIN = find.NumberIIN,
                        NumberPhone = find.NumberPhone,
                    }).ToList();
                
                return result;
            }

            return null;
        }

        public bool CreateDriver(DriverCreateDTOs driver)
        {
            var find = _context.Companies.Where(x => x.Email == driver.CompanyEmail).FirstOrDefault();

            if (find == null)
            {
                return false;
            }

            var result = new Driver();

            result.FirstName = driver.FirstName;
            result.LastName = driver.LastName;
            result.Password = driver.Password;
            result.CompanyId = find.Id;
            result.NumberPhone = driver.NumberPhone;
            result.NumberIIN = driver.NumberIIN;
            
            _context.AddAsync(result); 
            return Save();

        }

        public bool DriverExist(long NumberIIN)
        {
            return _context.Drivers.Any(x => x.NumberIIN == NumberIIN);
        }

        public IEnumerable<AzsDTOs> GetAZS()
        {
            var result = (from a in _context.Azs
                select new AzsDTOs()
                {
                    Id = a.Id,
                    AzsName = a.Name,
                });

            return result;
        }

        public bool CreatePayed(int driverId,PayedInfoDriverDTOs Payed)
        {
            var company = _context.Companies.Where(c => c.Name.ToUpper() == Payed.CompanyName.ToUpper()).FirstOrDefault();

            var companyCar = _context.CompanyCars.Where(a => a.NumberCar.ToUpper() == Payed.NumberCar.ToUpper()).FirstOrDefault();
            var driversId = _context.Drivers.Where(a => a.Id == driverId).FirstOrDefault();

            var netAzses = _context.NetAzses.Where(d => d.Name.ToUpper() == Payed.AzsName.ToUpper()).FirstOrDefault();

            var oil = _context.Oils.Where(o => o.Name.ToUpper() == Payed.Oil.ToUpper()).FirstOrDefault();

            if (company == null || companyCar == null || netAzses == null || oil == null)
            {
                return false;

            }
            Payed payed = new Payed();
            Payed.DriverPayed = "Оплаченно";
            Payed.AZSApplied = "В ожидании";

            payed.CompanyId = company.Id;
            payed.CompanyCarId = companyCar.Id;
            payed.NetAzsId = netAzses.Id;
            payed.OilId = oil.Id;
            payed.DriverId = driversId.Id;
            payed.Money = Payed.Money;
            payed.Count = Payed.Count;
            payed.DriverPayed = Payed.DriverPayed;
            payed.AZSApplied = Payed.AZSApplied;
            payed.Date = DateTime.UtcNow;

            _context.AddAsync(payed);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }
    }
}