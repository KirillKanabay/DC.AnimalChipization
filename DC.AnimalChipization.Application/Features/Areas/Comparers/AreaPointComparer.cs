using DC.AnimalChipization.Application.Features.Areas.DataTransfer;

namespace DC.AnimalChipization.Application.Features.Areas.Comparers
{
    public class AreaPointComparer : IEqualityComparer<AreaPointDto>
    {
        public bool Equals(AreaPointDto x, AreaPointDto y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Longitude.Equals(y.Longitude) && x.Latitude.Equals(y.Latitude);
        }

        public int GetHashCode(AreaPointDto obj)
        {
            return HashCode.Combine(obj.Longitude, obj.Latitude);
        }
    }
}
