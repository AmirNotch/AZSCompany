using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzsCompany.DBContext;
using AzsCompany.Domain;
using AzsCompany.DTOs;
using AzsCompany.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AzsCompany.Controllers
{
    public class AZSController : BaseApiController
    {
        private readonly IAZSRepository _azsRepository;
        private readonly DataContext _context;

        public AZSController(DataContext context, IAZSRepository azsRepository)
        {
            _context = context;
            _azsRepository = azsRepository;
        }
        
        [HttpPost("Auth")]
        public async Task<ActionResult<AZSLoginReturnDTOs>> GetDriversAuth([FromBody] AZSLoginDTOs azs)
        {
            var result = _azsRepository.GetAZSAuth(azs);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        
        [HttpGet("GetAZS/{pointId}")]
        public async Task<ActionResult<IEnumerable<AZSNETDTOs>>>  GetAZS(int pointId)
        {
            var result = _azsRepository.GetAZSNET(pointId);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        
        [HttpGet("GetPayed/{pointId}")]
        public async Task<ActionResult<IEnumerable<PayedInfoDriverDTOs>>> GetPayed(int pointId)
        {
            var result = _azsRepository.GetPayed(pointId);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        
        [HttpPost("Applied/{payedId}")]
        public async Task<IActionResult> Applied(int payedId)
        {
            if (_azsRepository.UpdatePayed(payedId))
            {
                return Ok(200);
            }

            return StatusCode(404);
        }
        
        
    }
}