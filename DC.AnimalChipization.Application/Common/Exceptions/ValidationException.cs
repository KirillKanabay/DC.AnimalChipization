namespace DC.AnimalChipization.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }
        
        public ValidationException()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> failures)
        {
            Errors = failures;
        }
    }
}
