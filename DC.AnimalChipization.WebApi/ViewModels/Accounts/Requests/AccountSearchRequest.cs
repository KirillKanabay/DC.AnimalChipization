namespace DC.AnimalChipization.WebApi.ViewModels.Accounts.Requests;

public class AccountSearchRequest : PagedRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}