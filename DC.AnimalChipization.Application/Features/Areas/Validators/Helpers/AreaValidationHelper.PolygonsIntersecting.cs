using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;

public static partial class AreaValidationHelper
{
    /// <summary>
    /// Checks that polygons are intersected
    /// </summary>
    /// <param name="polygon1"></param>
    /// <param name="polygon2"></param>
    /// <returns></returns>
    public static bool ArePolygonsIntersecting(List<AreaPointDto> polygon1, List<AreaPointDto> polygon2)
    {
        int polygon1PointsCount = polygon1.Count;
        int polygon2PointsCount = polygon2.Count;

        for (int firstEdgePointIdx = 0; firstEdgePointIdx < polygon1PointsCount; firstEdgePointIdx++)
        {
            var point1 = polygon1[firstEdgePointIdx];
            var point2 = polygon1[(firstEdgePointIdx + 1) % polygon1PointsCount];

            for (int secondEdgePointIdx = 0; secondEdgePointIdx < polygon2PointsCount; secondEdgePointIdx++)
            {
                var point3 = polygon2[secondEdgePointIdx];
                var point4 = polygon2[(secondEdgePointIdx + 1) % polygon2PointsCount];

                if (AreSegmentsIntersecting(point1, point2, point3, point4))
                {
                    return true;
                }
            }
        }

        return false;
    }
}