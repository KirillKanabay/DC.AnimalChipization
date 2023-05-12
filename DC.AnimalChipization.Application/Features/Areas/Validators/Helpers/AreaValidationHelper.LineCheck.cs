using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;

public static partial class AreaValidationHelper
{
    /// <summary>
    /// Checks that all points of polygon lay on one axis
    /// </summary>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static bool IsLine(List<AreaPointDto> polygon)
    {
        var firstPoint = polygon.First();

        return polygon.All(point => firstPoint.Longitude.Equals(point.Longitude)) ||
               polygon.All(point => firstPoint.Latitude.Equals(point.Latitude));
    }
}