using System.Collections.Generic;
using System.Linq;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AzsCompany.Repositories
{
    public interface IDriverRepository
    {
        IEnumerable<PayedInfoDriverNameDTOs> PayedInfoDriverDtOs(int driverId);
        IEnumerable<AZSDriverDTOs> GetCompaniesDrivers(int driverId);

        ActionResult<IEnumerable<Driver>> GetAuthDriver(DriverLoginDTOs driver);
        bool CreateDriver(DriverCreateDTOs driver);
        bool DriverExist(long NumberIIN);
        
        IEnumerable<AzsDTOs> GetAZS();

        bool CreatePayed(int driverId,PayedInfoDriverDTOs payed);
        bool Save();
    }
}