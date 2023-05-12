namespace DC.AnimalChipization.WebApi.ViewModels.Accounts.Requests;

public record UpdateAccountRequest(string FirstName, string LastName, string Email, string Password, string Role);