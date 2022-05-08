using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzsCompany.DBContext;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AzsCompany.Repositories
{
    public class AZSRepository : IAZSRepository
    {
        private readonly DataContext _context;

        public AZSRepository(DataContext context)
        {
            _context = context;
        }

        
        public ActionResult<IEnumerable<AZSNETDTOs>> GetAZSNET(int azsId)
        {
            var find = _context.Azs.Where(x => x.Id == azsId).FirstOrDefault();

            if (find != null)
            {
                var result = (from a in _context.Azs
                    join na in _context.NetAzses on a.Id equals na.AzsId
                    where na.AzsId == find.Id
                    select new AZSNETDTOs()
                    {
                        NameAZS = a.Name,
                        NamePointAZS = na.Name,
                    }).ToList();

                return result;
            }

            return new List<AZSNETDTOs>();
        }

        public ActionResult<IEnumerable<PayedInfoDriverDTOs>> GetPayed(int azsId)
        {
            var result = (from p in _context.Payeds
                join cp in _context.Companies on p.CompanyId equals cp.Id
                join d in _context.NetAzses on p.NetAzsId equals d.Id
                join o in _context.Oils on p.OilId equals o.Id
                join cr in _context.CompanyCars on p.CompanyCarId equals cr.Id
                where d.Id == azsId
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
                }).ToList();

            if (result != null)
            {
                return result;
            }

            return new List<PayedInfoDriverDTOs>();
        }

        public AZSLoginReturnDTOs GetAZSAuth(AZSLoginDTOs azs)
        {
            var result = _context.Azs
                .Where(x => x.Login == azs.Login && x.Password == azs.Password)
                .FirstOrDefault();

            if (result != null)
            {
                var result2 = (from a in _context.Azs
                    join na in _context.NetAzses on a.Id equals na.AzsId
                    where na.Name == azs.PointName
                    select new AZSLoginReturnDTOs()
                    {
                        Id = a.Id,
                        IdPoint = na.Id,
                        Name = a.Name,
                        NamePoint = na.Name,
                    }).FirstOrDefault();
                
                if (result2 != null)
                {
                    return result2;    
                }
                
            }

            return null;
        }

        public bool UpdatePayed(int payedId)
        {
            var result = _context.Payeds.Where(x => x.Id == payedId).FirstOrDefault();
            
            if (result != null)
            {
                result.AZSApplied = "Одобренно";
                // result.Id = payedId;
                // _context.UpdateRange(result);
                return Save();
            }

            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }   
    }
}