using System.ComponentModel.DataAnnotations;

namespace Project_ASP.Net.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
