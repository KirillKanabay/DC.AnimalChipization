using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Areas.Validators.Helpers
{
    public static partial class AreaValidationHelper
    {
        /// <summary>
        /// Checks that polygon has intersecting edges
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static bool AreEdgesIntersecting(List<AreaPointDto> polygon)
        {
            int pointsCount = polygon.Count;

            for (int firstEdgePointIdx = 0; firstEdgePointIdx < pointsCount; firstEdgePointIdx++)
            {
                var point1 = polygon[firstEdgePointIdx];
                var point2 = polygon[(firstEdgePointIdx + 1) % pointsCount];

                for (int secondEdgePointIdx = firstEdgePointIdx + 1; secondEdgePointIdx < pointsCount; secondEdgePointIdx++)
                {
                    var point3 = polygon[secondEdgePointIdx];
                    var point4 = polygon[(secondEdgePointIdx + 1) % pointsCount];

                    if (AreSegmentsIntersecting(point1, point2, point3, point4))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool AreSegmentsIntersecting(AreaPointDto point1, AreaPointDto point2, AreaPointDto point3, AreaPointDto point4)
        {
            var direction1 = CalculateDirection(point3, point4, point1);
            var direction2 = CalculateDirection(point3, point4, point2);
            var direction3 = CalculateDirection(point1, point2, point3);
            var direction4 = CalculateDirection(point1, point2, point4);

            if ((direction1 > 0 && direction2 < 0 || direction1 < 0 && direction2 > 0) &&
                (direction3 > 0 && direction4 < 0 || direction3 < 0 && direction4 > 0))
            {
                return true;
            }

            if (direction1 == 0 && IsPointOnSegment(point3, point4, point1) ||
                direction2 == 0 && IsPointOnSegment(point3, point4, point2) ||
                direction3 == 0 && IsPointOnSegment(point1, point2, point3) ||
                direction4 == 0 && IsPointOnSegment(point1, point2, point4))
            {
                return true;
            }

            return false;
        }

        private static double CalculateDirection(AreaPointDto point1, AreaPointDto point2, AreaPointDto point3)
        {
            return (point3.Longitude - point1.Longitude) * (point2.Latitude - point1.Latitude) - (point3.Latitude - point1.Latitude) * (point2.Longitude - point1.Longitude);
        }

        private static bool IsPointOnSegment(AreaPointDto p1, AreaPointDto p2, AreaPointDto p, bool include = false)
        {
            if (include)
            {
                return p.Longitude >= Math.Min(p1.Longitude, p2.Longitude) && p.Longitude <= Math.Max(p1.Longitude, p2.Longitude) &&
                       p.Latitude >= Math.Min(p1.Latitude, p2.Latitude) && p.Latitude <= Math.Max(p1.Latitude, p2.Latitude);
            }

            return p.Longitude > Math.Min(p1.Longitude, p2.Longitude) && p.Longitude < Math.Max(p1.Longitude, p2.Longitude) &&
                   p.Latitude > Math.Min(p1.Latitude, p2.Latitude) && p.Latitude < Math.Max(p1.Latitude, p2.Latitude);
        }
    }
}
