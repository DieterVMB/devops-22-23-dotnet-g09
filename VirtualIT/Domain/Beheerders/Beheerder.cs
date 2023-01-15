using Ardalis.GuardClauses;
using VirtualIT.Domain.Common;

namespace VirtualIT.Domain.Beheerders;

public class Beheerder : Entity
{
    private string voornaam = default!;
    public string Voornaam
    {
        get => voornaam;
        set => voornaam =Guard.Against.NullOrWhiteSpace(value,nameof(Voornaam));
    }
    private string naam = default!;
    public string Naam
    {
        get => naam;
        set => naam =Guard.Against.NullOrWhiteSpace(value, nameof(Naam));
    }
    private string email = default!;
    public string Email
    {
        get => email;
        set => email = Guard.Against.NullOrWhiteSpace(value, nameof(Email));
    }
    private string departement = default!;
    public string Departement
    {
        get => departement;
        set => departement = Guard.Against.NullOrWhiteSpace(value, nameof(Departement));
    }
    private Rol rol = default!;
    public Rol Rol { 
        get => rol;
        set => rol= Guard.Against.Null(value,nameof(Rol)); 
    }
    private bool isActief = default!;
    public bool IsActief {
        get => isActief;
        set => isActief= Guard.Against.Null(value,nameof(IsActief));
    }
    private string auth0Id = default!;
    public string Auth0Id
    {
        get => auth0Id;
        set => auth0Id = Guard.Against.NullOrWhiteSpace(value, nameof(Auth0Id));
    }

    private Beheerder() { }

    public Beheerder(string voornaam, string naam, string email, string departement, Rol rol, bool isActief, string auth0Id)
    {
        Voornaam = voornaam;
        Naam = naam; 
        Email = email;
        Departement = departement;
        Rol = rol;
        IsActief = isActief;
        Auth0Id = auth0Id;
    }
}
