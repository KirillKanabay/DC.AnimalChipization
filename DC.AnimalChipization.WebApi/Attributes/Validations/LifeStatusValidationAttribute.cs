using DC.AnimalChipization.WebApi.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DC.AnimalChipization.WebApi.Attributes.Validations;

public class LifeStatusValidationAttribute : ValidationAttribute
{
    public bool AllowNull { get; set; }

    public override bool IsValid(object value)
    {
        if (AllowNull && value == null) return true;

        return value is string lifeStatus && LifeStatuses.All.Contains(lifeStatus);
    }
}