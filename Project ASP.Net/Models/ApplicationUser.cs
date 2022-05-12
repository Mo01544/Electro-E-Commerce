using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Project_ASP.Net.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Order> Order { get; set; }

    }
}
