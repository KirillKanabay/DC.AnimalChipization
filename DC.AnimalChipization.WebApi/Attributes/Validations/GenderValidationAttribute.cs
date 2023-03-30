using System.ComponentModel.DataAnnotations;
using System.Linq;
using DC.AnimalChipization.WebApi.Immutable;

namespace DC.AnimalChipization.WebApi.Attributes.Validations
{
    public class GenderValidationAttribute : ValidationAttribute
    { 
        public bool AllowNull { get; set; }
        
        public override bool IsValid(object value)
        {
            if(AllowNull && value == null) return true;

            return value is string gender && Genders.All.Contains(gender);
        }
    }
}
