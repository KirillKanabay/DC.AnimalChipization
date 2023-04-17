namespace DC.AnimalChipization.Application.Features.Locations.Services.Contracts;

public interface IGeoHashService
{
    public string GetGeoHash(double latitude, double longitude);
    public string GetGeoHashV2(double latitude, double longitude);
}