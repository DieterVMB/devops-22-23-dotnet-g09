using Ardalis.GuardClauses;
using System.Net;
using VirtualIT.Domain.Beheerders;
using VirtualIT.Domain.Common;
using VirtualIT.Domain.Klanten;
using VirtualIT.Domain.Templates;

namespace VirtualIT.Domain.VirtualMachines;

public class VirtualMachine : Entity
{
    //1-6 (basis)
    private string naam = default!;
    public string Naam
    {
        get => naam;
        set => naam = Guard.Against.NullOrWhiteSpace(value, nameof(Naam));
    }
    private string hostname= default!;
    public string Hostname
    {
        get => hostname;
        set => hostname = Guard.Against.NullOrWhiteSpace(value, nameof(Hostname));
    }
    private string fqdn = default!;
    public string FQDN
    {
        get => fqdn;
        set => fqdn = Guard.Against.NullOrWhiteSpace(value, nameof(FQDN));
    }
    private int vcpu = default!;
    public int Vcpu
    {
        get => vcpu;
        set => vcpu = Guard.Against.NegativeOrZero(value, nameof(Vcpu));
    }
    private int memory = default!;
    public int Memory
    {
        get => memory;
        set => memory = Guard.Against.NegativeOrZero(value,nameof(Memory));
    }
    private int storage = default!;
    public int Storage
    {
        get => storage;
        set => storage = Guard.Against.NegativeOrZero(value, nameof(Storage));
    }

    //7-13 (detail)
    private Mode mode = default!;
    public Mode Mode
    {
        get => mode;
        set => mode = Guard.Against.Null(value, nameof(Mode));
    }
    public Template? Template { get; set; }
    private bool backup = default!;
    public bool Backup
    {
        get => backup;
        set => backup = Guard.Against.Null(value, nameof(Backup));
    }
    public string? BackupFrequentie { get; set; }
    private Beschikbaarheid beschikbaarheid = default!;
    public Beschikbaarheid Beschikbaarheid
    {
        get => beschikbaarheid;
        set => beschikbaarheid = Guard.Against.Null(value, nameof(Beschikbaarheid));
    }
    private bool highAvailable = default!;
    public bool HighAvailable
    {
        get => highAvailable;
        set => highAvailable = Guard.Against.Null(value, nameof(HighAvailable));
    }

    //14-21 (aanvrager)
    private Klant klant = default!;
    public Klant Klant
    {
        get => klant;
        set => klant = Guard.Against.Null(value, nameof(Klant));
    }
    private string emailAanvrager = default!;
    public string EmailAanvrager
    {
        get => emailAanvrager;
        set => emailAanvrager = Guard.Against.NullOrWhiteSpace(value, nameof(EmailAanvrager));
    }
    private string telefoonnummerAanvrager = default!;
    public string TelefoonnummerAanvrager
    {
        get => telefoonnummerAanvrager;
        set => telefoonnummerAanvrager = Guard.Against.NullOrWhiteSpace(value, nameof(TelefoonnummerAanvrager));
    }
    private DateTime datumAanvraag = default!;
    public DateTime DatumAanvraag { 
        get => datumAanvraag;
        set => datumAanvraag = Guard.Against.Null(value, nameof(DatumAanvraag));
    }
    private string naamGebruiker = default!;
    public string NaamGebruiker
    {
        get => naamGebruiker;
        set => naamGebruiker = Guard.Against.NullOrWhiteSpace(value, nameof(NaamGebruiker));
    }
    private string relatieGebruiker = default!;
    public string RelatieGebruiker
    {
        get => relatieGebruiker;
        set => relatieGebruiker = Guard.Against.NullOrWhiteSpace(value, nameof(RelatieGebruiker));
    }
    private string reden = default!;
    public string Reden
    {
        get => reden;
        set => reden = Guard.Against.NullOrWhiteSpace(value, nameof(Reden));
    }
    private DateTime startDatum = default!;
    public DateTime Startdatum
    {
        get => startDatum;
        set => startDatum = Guard.Against.Null(value, nameof(Startdatum));
    }
    private DateTime eindDatum = default!;
    public DateTime Einddatum
    {
        get => eindDatum;
        set => eindDatum = Guard.Against.Null(value, nameof(Einddatum));
    }

    //22-25 (detail)
    private Status status = default!;
    public Status Status
    {
        get => status;
        set => status = Guard.Against.Null(value, nameof(Status));
    }
    private Beheerder toegewezenAan = default!;
    public Beheerder ToegewezenAan
    {
        get => toegewezenAan;
        set => toegewezenAan = Guard.Against.Null(value, nameof(ToegewezenAan));
    }
    private string externeToegangspoorten = default!;
    public string ExterneToegangspoorten
    {
        get => externeToegangspoorten;
        set => externeToegangspoorten = Guard.Against.NullOrWhiteSpace(value, nameof(ExterneToegangspoorten));
    }

    private VirtualMachine() { }

    public VirtualMachine(string naam, string hostname, string fqdn, int vcpu, int memory, int storage,
                            Mode mode, Template? template, bool backup, string? backupFrequentie, Beschikbaarheid beschikbaarheid, bool highAvailable, Klant klant,
                            string emailAanvrager, string telefoonnummerAanvrager, DateTime datumAanvraag, string naamGebruiker, string relatieGebruiker, string reden, DateTime startdatum, DateTime einddatum,
                            Status status, Beheerder toegewezenAan, string externeToegangspoorten)
    {
        Naam = naam;
        Hostname = hostname;
        FQDN = fqdn;
        Vcpu = vcpu;
        Memory = memory;
        Storage = storage;

        Mode = mode;
        Template = template;
        Backup = backup;
        BackupFrequentie = backupFrequentie;
        Beschikbaarheid = beschikbaarheid;
        HighAvailable = highAvailable;

        Klant = klant;
        EmailAanvrager = emailAanvrager;
        TelefoonnummerAanvrager = telefoonnummerAanvrager;
        DatumAanvraag = datumAanvraag;
        NaamGebruiker = naamGebruiker;
        RelatieGebruiker = relatieGebruiker;
        Reden = reden;
        Startdatum = startdatum;
        Einddatum = einddatum;

        Status = status;
        ToegewezenAan = toegewezenAan;
        ExterneToegangspoorten = externeToegangspoorten;
    }
}
