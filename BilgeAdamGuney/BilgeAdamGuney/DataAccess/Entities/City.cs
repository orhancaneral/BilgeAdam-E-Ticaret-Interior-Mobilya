using System.Collections.Generic;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<District> Districts { get; set; }

    }
}