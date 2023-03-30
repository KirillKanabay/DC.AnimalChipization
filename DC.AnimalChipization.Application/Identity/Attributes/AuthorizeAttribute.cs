namespace DC.AnimalChipization.Application.Identity.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute
    {
        public AuthorizeAttribute() { }
    }
}
