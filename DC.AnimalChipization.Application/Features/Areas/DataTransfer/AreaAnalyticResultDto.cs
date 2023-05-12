namespace DC.AnimalChipization.Application.Features.Areas.DataTransfer;

public class AreaAnalyticResultDto
{
    public long TotalQuantityAnimals { get; set; }
    public long TotalAnimalsArrived { get; set; }
    public long TotalAnimalsGone { get; set; }
    public List<AreaAnalyticItemDto> AnimalsAnalytics { get; set; }
}