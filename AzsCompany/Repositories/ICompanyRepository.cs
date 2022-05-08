using System.Collections.Generic;
using System.Threading.Tasks;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AzsCompany.Repositories
{
    public interface ICompanyRepository
    { 
        IEnumerable<CompanyDriversDTOs> GetDrivers();
        IEnumerable<CompanyCar> GetCompanyCar(int companyId);
        IEnumerable<PayedInfoDriverDTOs> GetPayedInfo(int companyId);
        IEnumerable<AZSNETDTOs> GetInfoPointAzs();
        IEnumerable<CompanyDriversDTOs> GetDriversById(int id); 
        ActionResult<IEnumerable<CompanyDriversDTOs>> GetDriversAuth(CompanyLoginDTOs companyLoginDtOs);
        bool CompanyExist(Company company);
        bool CompanyCarExist(CompanyCar companyCar);
        bool CreateCompany(Company company);
        bool CreateCompanyCar(int companyCarId, CompanyCar companyCar);
        bool Save();

        //IEnumerable<AzsDTOs> GetAzses();
    }
}