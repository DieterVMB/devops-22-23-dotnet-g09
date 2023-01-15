using VirtualIT.Domain.Beheerders;
using Ardalis.GuardClauses;
using VirtualIT.Domain.Common;

namespace VirtualIT.Domain.Klanten;

public class Klant : Entity 
{
    private bool externe = default!;
    public bool Extern
    {
        get => externe;
        set => externe = Guard.Against.Null(value, nameof(Extern));
    }
    public string? Departement { get; set; }
    public string? Opleiding { get; set; }
    public string? KlantNaam { get; set; }
    public string? ExternType { get; set; }
    private Aanspreekpunt aanspreekpunt = default!;
    public Aanspreekpunt Aanspreekpunt
    {
    get => aanspreekpunt;
    set => aanspreekpunt = Guard.Against.Null(value, nameof(Aanspreekpunt));
    }
    private Aanspreekpunt backupaanspreekpunt = default!;
    public Aanspreekpunt BackupAanspreekpunt
    {
        get => backupaanspreekpunt;
        set => backupaanspreekpunt = Guard.Against.Null(value, nameof(BackupAanspreekpunt));
    }

    private Klant() { }

    public Klant(bool externe, string? departement, string? opleiding, string? klantnaam, string? externtype, Aanspreekpunt aanspreekpunt, Aanspreekpunt backupaanspreekpunt)
    {
        Extern = externe;
        Departement = departement;
        Opleiding = opleiding;
        KlantNaam = klantnaam;
        ExternType = externtype;
        Aanspreekpunt = aanspreekpunt;
        BackupAanspreekpunt = backupaanspreekpunt;
    }
}
