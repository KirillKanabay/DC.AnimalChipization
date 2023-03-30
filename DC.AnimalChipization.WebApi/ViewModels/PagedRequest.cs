namespace DC.AnimalChipization.WebApi.ViewModels
{
    public abstract class PagedRequest
    {
        public int From { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
