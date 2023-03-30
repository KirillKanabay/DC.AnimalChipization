using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DC.AnimalChipization.Application.Identity.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DC.AnimalChipization.WebApi.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IIdentityManager _identityManager;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock, IIdentityManager identityManager) : base(options, logger, encoder, clock)
    {
        _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers["Authorization"].ToString();

        if (authHeader != null && authHeader.StartsWith(Scheme.Name, StringComparison.InvariantCultureIgnoreCase))
        {
            var token = authHeader.Substring($"{Scheme.Name} ".Length).Trim();
            var encodedCreds = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var creds = encodedCreds.Split(':');

            if (creds.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var email = creds[0];
            var password = creds[1];

            var user = await _identityManager.AuthenticateAsync(email, password);

            if (user != null)
            {
                var claims = new[] { new Claim("Email", user.Email) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var claimsPrincipal = new ClaimsPrincipal(identity);

                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
            }
        }

        return AuthenticateResult.Fail("Invalid Authorization Header");
    }
}