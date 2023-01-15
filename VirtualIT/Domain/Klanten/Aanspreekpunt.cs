using VirtualIT.Domain.Common;
using Ardalis.GuardClauses;

namespace VirtualIT.Domain.Klanten;

public class Aanspreekpunt : ValueObject
{
    public string Voornaam { get; set; } = default!;

    public string Naam { get; set; }= default!;

    public string Email { get; set; } = default!;

    public string Telefoonnummer { get; set; } = default!;

    private Aanspreekpunt() { }

    public Aanspreekpunt(string voornaam, string naam, string email, string telefoonnummer) 
    {
        Voornaam =  Guard.Against.NullOrWhiteSpace(voornaam, nameof(Voornaam));
        Naam = Guard.Against.NullOrWhiteSpace(naam, nameof(Naam));
        Email = Guard.Against.NullOrWhiteSpace(email, nameof(Email));
        Telefoonnummer = Guard.Against.NullOrWhiteSpace(telefoonnummer, nameof(Telefoonnummer));
    }

    protected override IEnumerable<object?> GetEqualityComponents() 
    {
        yield return Voornaam;
        yield return Naam;
        yield return Email;
        yield return Telefoonnummer;
    }
}
