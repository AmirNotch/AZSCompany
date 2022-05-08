using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzsCompany.DBContext;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzsCompany.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<CompanyCar> GetCompanyCar(int companyId)
        {
            return _context.CompanyCars.Where(x => x.CompanyId == companyId).ToList();
        }

        public IEnumerable<PayedInfoDriverDTOs> GetPayedInfo(int companyId)
        {
            var result = (from p in _context.Payeds
                join cp in _context.Companies on p.CompanyId equals cp.Id
                join d in _context.NetAzses on p.NetAzsId equals d.Id
                join o in _context.Oils on p.OilId equals o.Id
                join cr in _context.CompanyCars on p.CompanyCarId equals cr.Id
                where cp.Id == companyId
                select new PayedInfoDriverDTOs()
                {
                    Id = p.Id,
                    CompanyName = cp.Name,
                    NumberCar = cr.NumberCar,
                    AzsName = d.Name,
                    Count = p.Count,
                    Money = p.Money,
                    Oil = o.Name,
                    DriverPayed = p.DriverPayed,
                    AZSApplied = p.AZSApplied,
                    Date = p.Date,
                }).ToList().OrderBy(p => p.Id);

            return result;
        }

        public IEnumerable<AZSNETDTOs> GetInfoPointAzs()
        {
            var result = (from p in _context.NetAzses
                join cp in _context.Azs on p.AzsId equals cp.Id
                select new AZSNETDTOs()
                {
                    NameAZS = cp.Name,
                    NamePointAZS = p.Name,
                });

            return result;
        }

        public IEnumerable<CompanyDriversDTOs> GetDriversById(int id)
        {
            var drivers = (from d in _context.Drivers
                join cp in _context.Companies on d.CompanyId equals cp.Id
                where d.CompanyId == id
                select new CompanyDriversDTOs()
                {
                    Id = d.Id,
                    CompanyName = cp.Name,
                    Email = cp.Email,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                }).ToList();

            return drivers;
        }

        public ActionResult<IEnumerable<CompanyDriversDTOs>> GetDriversAuth(CompanyLoginDTOs companyLoginDtOs)
        {
            var result = _context.Companies
                .Where(a => a.Email == companyLoginDtOs.Email && a.Password == companyLoginDtOs.Password)
                .FirstOrDefault();

            if (result != null)
            {
                var drivers = (from d in _context.Drivers
                    join cp in _context.Companies on d.CompanyId equals cp.Id
                    where d.CompanyId == result.Id
                    select new CompanyDriversDTOs()
                    {
                        Id = d.Id,
                        CompanyName = cp.Name,
                        Email = cp.Email,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                    }).ToList();

                return drivers;
            }

            return null;

        }

        public bool CompanyExist(Company company)
        {
            return _context.Companies.Any(c => c.Email == company.Email);
        }

        public bool CompanyCarExist(CompanyCar companyCar)
        {
            return _context.CompanyCars.Any(c => c.NumberCar == companyCar.NumberCar);
        }

        public bool CreateCompany(Company company)
        {
            _context.AddAsync(company); 
            return Save();
        }

        public bool CreateCompanyCar(int companyCarId,CompanyCar companyCar)
        {
            companyCar.CompanyId = companyCarId;
            _context.AddAsync(companyCar);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        public IEnumerable<CompanyDriversDTOs> GetDrivers()
        {
            var drivers = (from d in _context.Drivers
                join cp in _context.Companies on d.CompanyId equals cp.Id
                select new CompanyDriversDTOs()
                {
                    Id = d.Id,
                    CompanyName = cp.Name,
                    Email = cp.Email,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                }).ToList();

            return drivers;
        }

        // public IEnumerable<AzsDTOs> GetAzses()
        // {
        //     var azses = (from a in _context.Azs
        //         join na in _context.NetAzses on a.NetAZSId equals na.Id
        //         select new AzsDTOs()
        //         {
        //             Id = a.Id,
        //             Login = 
        //         });
        //     
        //     return azses;
        // }
    }
}