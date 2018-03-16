using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class Customer
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
