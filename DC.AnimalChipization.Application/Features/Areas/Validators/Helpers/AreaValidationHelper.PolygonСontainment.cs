using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;

public static partial class AreaValidationHelper
{
    /// <summary>
    /// Checks that one polygon is inside another
    /// </summary>
    /// <param name="innerPolygon"></param>
    /// <param name="outerPolygon"></param>
    /// <returns></returns>
    public static bool AreContainedPolygons(List<AreaPointDto> outerPolygon, List<AreaPointDto> innerPolygon)
    {
        var polygon1PointCount = innerPolygon.Count;

        for (int polygon1PointIdx = 0; polygon1PointIdx < polygon1PointCount; polygon1PointIdx++)
        {
            if (!IsPointInsidePolygon(innerPolygon[polygon1PointIdx], outerPolygon))
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsPointInsidePolygon(AreaPointDto point, List<AreaPointDto> polygon)
    {
        var polygonPointsCount = polygon.Count;
        var isInside = false;

        for (int i = 0, j = polygonPointsCount - 1; i < polygonPointsCount; j = i++)
        {
            if (IsPointOnSegment(polygon[i], polygon[j], point, include: true))
            {
                return true;
            }

            if (((polygon[i].Latitude <= point.Latitude && point.Latitude < polygon[j].Latitude) ||
                 (polygon[j].Latitude <= point.Latitude && point.Latitude < polygon[i].Latitude)) &&
                 (point.Longitude < (polygon[j].Longitude - polygon[i].Longitude) * (point.Latitude - polygon[i].Latitude) / (polygon[j].Latitude - polygon[i].Latitude) + polygon[i].Longitude))
            {
                isInside = !isInside;
            }
        }

        return isInside;
    }


}