using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace VirtualIT.Client.Shared;


public class AuthenticationProvider : AuthenticationStateProvider
{
    public ClaimsPrincipal Current { get; private set; } = Onbekend;

    //declaratie rollen
    public static ClaimsPrincipal Onbekend => new(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.Name, "Onbekend")
    }));

    public static ClaimsPrincipal Beheerder => new(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim(ClaimTypes.Name, "Fake beheerder"),
        new Claim(ClaimTypes.Email, "fake-beheerder@gmail.com"),
        new Claim(ClaimTypes.Role, "Beheerder")
    }, "Beheerder"));

    public static ClaimsPrincipal Gebruiker => new(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, "2"),
        new Claim(ClaimTypes.Name, "Fake gebruiker"),
        new Claim(ClaimTypes.Email, "fake-gebruiker@gmail.com"),
        new Claim(ClaimTypes.Role, "Gebruiker")
    }, "Gebruiker"));

    //instellen en wijzigen rol
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(Current));
    }

    public void ChangeAuthenticationState(ClaimsPrincipal claimsPrincipal)
    {
        Current = claimsPrincipal;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}

