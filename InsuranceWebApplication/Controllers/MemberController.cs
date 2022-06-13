using InsuranceWebApplication.Data;
using InsuranceWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace InsuranceWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        readonly InsuranceDBContext _insuranceDBContext;

        public MemberController(InsuranceDBContext InsuranceDBContext)
        {
            _insuranceDBContext = InsuranceDBContext;
        }

        [HttpGet]
        [Route("{memberId}")]
        public async Task<IEnumerable<Member>> GetMember(int memberId)
        {
            var data = await _insuranceDBContext.Members.Where(b => b.Id == memberId).ToListAsync();
            return data;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<Member>> GetMembers()
        {
            var data = await _insuranceDBContext.Members.ToListAsync();
            return data;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> InsertMember([FromBody] MemberRequest model)
        {
            if (model.Id > 0)
                return BadRequest();

            var dateOfBirth = DateTime.ParseExact(model.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var addRecord = new Member
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                DateOfBirth = dateOfBirth,
                DeathInsuredSum = model.DeathInsuredSum,
                OccupationId = model.OccupationId,
            };

             _insuranceDBContext.Members.Add(addRecord);
            await _insuranceDBContext.SaveChangesAsync();
            
            return addRecord;
        }

        [HttpPost]
        [Route("deathpremiumcalculate")]
        public async Task<decimal> calculateDeathPremium([FromBody]MemberRequest memberRequest)
        {
            var data = await _insuranceDBContext.Occupations
                            .Include(o => o.OccupationRating)
                            .Where(o => o.Id == memberRequest.OccupationId).FirstOrDefaultAsync();

            if (data == null) return 0;

            decimal premiumAmount = ((memberRequest.DeathInsuredSum * data.OccupationRating.Factor * memberRequest.Age) / 1000) * 12;
            return premiumAmount;
        }
    }
}
