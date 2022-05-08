using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AzsCompany.DBContext;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using AzsCompany.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AzsCompany.Controllers
{
    public class DriverController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IDriverRepository _driverRepository;
        private readonly ICompanyRepository _companyRepository;

        
        public DriverController(DataContext context, IDriverRepository driverRepository, ICompanyRepository companyRepository)
        {
            _context = context;
            _driverRepository = driverRepository;
            _companyRepository = companyRepository;
        }
        
        
        // [HttpGet("{driverId}")]
        // public IEnumerable<PayedInfoDriverDTOs> InfoPayed(int driverId)
        // {
        //     return;
        // }

        [HttpPost("Auth")]
        public async Task<ActionResult<IEnumerable<Driver>>> AuthDriver([FromBody] DriverLoginDTOs driver)
        {
            driver.Password = Sha256(driver.Password);
            
            var result = _driverRepository.GetAuthDriver(driver);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        
        [HttpPost("createDriver")]
        public async Task<IActionResult> CreateDriver(DriverCreateDTOs driverCreateDtOs)
        {
            driverCreateDtOs.Password = Sha256(driverCreateDtOs.Password);
            
            if (_driverRepository.DriverExist(driverCreateDtOs.NumberIIN))
            {
               return StatusCode(302, "ИИН уже зарегистрирован");
            }
            
            if (_driverRepository.CreateDriver(driverCreateDtOs))
            {
                return Ok(201);
            }

            return StatusCode(404); 
        }

        [HttpPost("paying")]
        public async Task<IActionResult> Paying(int driverId,PayedInfoDriverDTOs payedInfoDriverDtOs)
        {
            if (_driverRepository.CreatePayed(driverId,payedInfoDriverDtOs))
            {
                return Ok(200);
            }
 
            return StatusCode(400);
        }

        [HttpGet("payed/{driverId}")]
        public IEnumerable<PayedInfoDriverNameDTOs> InfoPayed(int driverId)
        {
            return _driverRepository.PayedInfoDriverDtOs(driverId);
        }
        
        [HttpGet("getPointAZS")]
        public IEnumerable<AZSNETDTOs> GetPointAZS()
        {
            return _companyRepository.GetInfoPointAzs().ToList();
        }
        
        static string Sha256(string password)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}