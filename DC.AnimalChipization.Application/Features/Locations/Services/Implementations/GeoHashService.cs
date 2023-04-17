using System.Text;
using DC.AnimalChipization.Application.Features.Locations.Services.Contracts;
using NGeoHash;

namespace DC.AnimalChipization.Application.Features.Locations.Services.Implementations;

public class GeoHashService : IGeoHashService
{
    private const int NumberOfChars = 12;
    public string GetGeoHash(double latitude, double longitude)
    {
        return GeoHash.Encode(latitude, longitude, NumberOfChars);
    }

    public string GetGeoHashV2(double latitude, double longitude)
    {
        var geoHash = GetGeoHash(latitude, longitude);
        var bytes = Encoding.UTF8.GetBytes(geoHash);

        return Convert.ToBase64String(bytes);
    }
}