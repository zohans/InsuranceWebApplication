using InsuranceWebApplication.Data;
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
        [Route("member/{memberId}")]
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
    }
}
