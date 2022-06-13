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
            var data = await _insuranceDBContext.Members                
                .Where(b => b.Id == memberId).ToListAsync();
            
            return data;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<MemberResponse>> GetMembers()
        {
            var data = await _insuranceDBContext.Members
                .Include(o => o.Occupation)
                .ToListAsync();

            IList<MemberResponse> result = new List<MemberResponse>();
            foreach (var d in data)
            {
                result.Add(new MemberResponse
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Age = d.Age,
                    DateOfBirth = d.DateOfBirth,
                    DeathInsuredSum = d.DeathInsuredSum,
                    Occupation = d.Occupation.Name,
                    Premium = d.Premium
                });                
            }
            return result;
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
                Premium = model.Premium
            };

             _insuranceDBContext.Members.Add(addRecord);
            await _insuranceDBContext.SaveChangesAsync();
            
            return addRecord;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Member>> DeleteMember(int id)
        {
            var deleteRecord = await _insuranceDBContext.Members.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (deleteRecord == null)
            {
                return NotFound();
            }

            _insuranceDBContext.Remove(deleteRecord);
            await _insuranceDBContext.SaveChangesAsync();

            return deleteRecord;
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


        [HttpGet]
        [Route("occupationlist")]
        public async Task<IEnumerable<OccupationResponse>> GetOccupation()
        {
            var data = await _insuranceDBContext.Occupations
                .ToListAsync();

            var result = new List<OccupationResponse>();
            foreach (var d in data)
            {
                result.Add(new OccupationResponse { Id = d.Id, Name = d.Name });
            }
            return result;
        }
    }
}
