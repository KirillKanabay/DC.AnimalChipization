namespace DC.AnimalChipization.Data.Repositories.Contracts;

public interface IAreaPointRepository
{
    public Task DeletePointsByAreaIdAsync(long areaId);
}