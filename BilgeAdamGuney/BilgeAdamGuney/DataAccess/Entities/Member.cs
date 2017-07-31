using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmailHash { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        //public int? DistrictId { get; set; }
        //public virtual District District { get; set; }
        public int RoleId { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Address> Addresses { get; set; }

    }

}