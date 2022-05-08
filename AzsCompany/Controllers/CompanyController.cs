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
using Microsoft.EntityFrameworkCore;

namespace AzsCompany.Controllers
{
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly DataContext _context;

        public CompanyController(ICompanyRepository companyRepository, DataContext context)
        {
            _companyRepository = companyRepository;
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<CompanyDriversDTOs> GetDrivers()
        {
            return _companyRepository.GetDrivers().ToList();
        }
        
        [HttpGet("{companyId}")]
        public IEnumerable<CompanyDriversDTOs> GetDriversById([FromRoute] int companyId)
        {
            return _companyRepository.GetDriversById(companyId).ToList();
        }
        
        [HttpGet("payed/{companyId}")]
        public IEnumerable<PayedInfoDriverDTOs> GetPayedInfo([FromRoute] int companyId)
        {
            return _companyRepository.GetPayedInfo(companyId).ToList();
        }
        
        [HttpGet("companyCars/{companyId}")]
        public IEnumerable<CompanyCar> GetCompanyCars([FromRoute] int companyId)
        {
            return _companyRepository.GetCompanyCar(companyId).ToList();
        }
        
        [HttpGet("getPointAZS")]
        public IEnumerable<AZSNETDTOs> GetPointAZS()
        {
            return _companyRepository.GetInfoPointAzs().ToList();
        }
        
        [HttpPost("carRegister{companyId}")]
        public async Task<ActionResult<Company>> GetDriversAuth([FromRoute] int companyId,[FromBody] CompanyCar companyCar)
        {

            if (_companyRepository.CompanyCarExist(companyCar))
            {
                return StatusCode(302, "Номер машины уже зарегистрирован"); 
            }
            
            if (_companyRepository.CreateCompanyCar(companyId,companyCar))
            {
                return Ok(201);
            }

            return StatusCode(404);
        }
        
        [HttpPost("Auth")]
        public async Task<ActionResult<Company>> GetDriversAuth([FromBody] CompanyLoginDTOs companyLoginDtOs)
        {
            Console.WriteLine("hello");
            
            companyLoginDtOs.Password = Sha256(companyLoginDtOs.Password);
            
            var result = _companyRepository.GetDriversAuth(companyLoginDtOs);

            if (result == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.Where(x => x.Email == companyLoginDtOs.Email).FirstOrDefaultAsync();

            return company;
        }
        
        [HttpPost("CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            company.Password = Sha256(company.Password);
            
            if (_companyRepository.CompanyExist(company))
            {
                return StatusCode(302, "Email уже зарегистрирован");
            }
            
            if (_companyRepository.CreateCompany(company))
            {
                return Ok(201);
            }

            return StatusCode(404);
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
        //public async Task<ActionResult<List<Company>>> GetCompanies() => await Mediator.Send(new ListCompany.Query());
    }
}