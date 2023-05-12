using DC.AnimalChipization.Application.Features.Areas.Comparers;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;

public static partial class AreaValidationHelper
{
    /// <summary>
    /// Checks duplicated polygon's points
    /// </summary>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static bool HasDuplicates(List<AreaPointDto> polygon)
    {
        return polygon.Distinct(new AreaPointComparer()).Count() != polygon.Count;
    }
}