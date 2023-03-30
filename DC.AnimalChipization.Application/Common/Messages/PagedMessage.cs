namespace DC.AnimalChipization.Application.Common.Messages
{
    public abstract class PagedMessage 
    {
        public int From { get; set; }
        public int Size { get; set; }
    }
}
