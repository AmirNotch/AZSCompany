using System.Collections.Generic;
using System.Threading.Tasks;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AzsCompany.Repositories
{
    public interface IAZSRepository
    {
        ActionResult<IEnumerable<AZSNETDTOs>> GetAZSNET(int azsId);
        ActionResult<IEnumerable<PayedInfoDriverDTOs>> GetPayed(int azsId);
        AZSLoginReturnDTOs GetAZSAuth(AZSLoginDTOs azs);
        
        bool UpdatePayed(int AzsId);
        bool Save();

    }
}