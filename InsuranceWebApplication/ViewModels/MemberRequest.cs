using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceWebApplication.ViewModels
{
    public class MemberRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string DateOfBirth { get; set; }

        public decimal DeathInsuredSum { get; set; }
        public decimal Premium { get; set; }
        public string  Occupation { get; set; }
        public int OccupationId { get; set; }
    }
}
