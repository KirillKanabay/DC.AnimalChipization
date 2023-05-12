using DC.AnimalChipization.Application.Identity.Models;

namespace DC.AnimalChipization.Application.Identity.Contracts;

public interface IIdentityManager
{
    public Task<ApplicationUser> AuthenticateAsync(string email, string password);
    public bool HasRole(ApplicationUser user, string roleName);
    public ApplicationUser GetCurrentUser();
}