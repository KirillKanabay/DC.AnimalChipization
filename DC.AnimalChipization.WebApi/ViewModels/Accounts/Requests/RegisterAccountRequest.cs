namespace DC.AnimalChipization.WebApi.ViewModels.Accounts.Requests;

public record RegisterAccountRequest(string FirstName, string LastName, string Email, string Password);
