using DC.AnimalChipization.Application.Features.Locations.Services.Contracts;
using DC.AnimalChipization.Application.Features.Locations.Services.Implementations;

namespace DC.AnimalChipization.Application.Tests;

public class GeoHashServiceTests
{
    [TestCase(83.90197060970792, -125.49999198336496, "cnyv21vsqhzm")]
    public void GetGeoHashTests(double latitude, double longitude, string expectedResult)
    {
        var service = GetGeoHashService();
        var result = service.GetGeoHash(latitude, longitude);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase(83.90197060970792, -125.49999198336496, "Y255djIxdnNxaHpt")]
    public void GetGeoHashV2Tests(double latitude, double longitude, string expectedResult)
    {
        var service = GetGeoHashService();
        var result = service.GetGeoHashV2(latitude, longitude);

        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    private IGeoHashService GetGeoHashService()
    {
        return new GeoHashService();
    }
}