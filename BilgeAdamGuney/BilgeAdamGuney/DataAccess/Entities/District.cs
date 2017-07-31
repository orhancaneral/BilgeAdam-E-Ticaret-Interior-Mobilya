using System.Collections.Generic;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual List<Address> Addresses { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}