using InsuranceWebApplication.Data;
using InsuranceWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        [Route("deathpremiumcalculate")]
        public async Task<decimal> calculateDeathPremium([FromBody]MemberRequest memberRequest)
        {
            var data = await _insuranceDBContext.Occupations
                            .Include(o => o.OccupationRating)
                            .Where(o => o.Name == memberRequest.Occupation).FirstOrDefaultAsync();

            decimal premiumAmount = ((memberRequest.DeathInsuredSum * data.OccupationRating.Factor * memberRequest.Age) / 1000) * 12;
            return premiumAmount;
        }
    }
}
