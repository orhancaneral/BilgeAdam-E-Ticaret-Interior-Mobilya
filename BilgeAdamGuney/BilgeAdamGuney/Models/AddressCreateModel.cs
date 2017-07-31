using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class AddressCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}