using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_ASP.Net.Models
{
    public class ApplicationUser : IdentityUser
    {
		[ MaxLength(100)]
		[Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public virtual List<Order> Order { get; set; }
    }
}
