using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Project_ASP.Net.Models
{
    public class UniqueAttribute:ValidationAttribute
    {

        public string Massage { get; set; }
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            Category std= (Category)validationContext.ObjectInstance;
            string name = value.ToString();
            if (name != null)
            {
                ASPContext context = new ASPContext();
                Category findCategory=
                    context.Categories.FirstOrDefault(s => s.Name == name);
                if (findCategory == null)
                {
                    return ValidationResult.Success;//unique
                }
                else
                    return new ValidationResult("Name Already Found");
            }
            
            return base.IsValid(value, validationContext);
        }
    }
}
