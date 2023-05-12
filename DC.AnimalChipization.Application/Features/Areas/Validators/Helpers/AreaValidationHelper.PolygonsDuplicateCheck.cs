using DC.AnimalChipization.Application.Features.Areas.Comparers;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;

public static partial class AreaValidationHelper
{
    /// <summary>
    /// Checks that polygons are equal
    /// </summary>
    /// <param name="polygon1"></param>
    /// <param name="polygon2"></param>
    /// <returns></returns>
    public static bool IsPolygonsDuplicate(List<AreaPointDto> polygon1, List<AreaPointDto> polygon2)
    {
        if (polygon1.Count != polygon2.Count)
        {
            return false;
        }

        var comparer = new AreaPointComparer();

        var polygon1FirstPoint = polygon1.First();
        var polygonPointOffset = polygon2.FindIndex(x => comparer.Equals(x, polygon1FirstPoint));

        if (polygonPointOffset == -1)
        {
            return false;
        }

        for (int pointIdx = 1; pointIdx < polygon1.Count; pointIdx++)
        {
            var polygon1Point = polygon1[pointIdx];
            var polygon2Point = polygon2[(pointIdx + polygonPointOffset) % polygon1.Count];

            if (!comparer.Equals(polygon1Point, polygon2Point))
            {
                return false;
            }
        }

        return true;
    }
}