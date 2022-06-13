using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceWebApplication.Data
{
    public class Member 
    {        
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        public int Age { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DeathInsuredSum { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Premium { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        /*Foreign Key */
        public int OccupationId { get; set; }
        public Occupation Occupation { get; set; }



    }
}
