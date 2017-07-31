using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class OrderCreateAsGuestModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string AddressDescription { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}