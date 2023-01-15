using FluentValidation;
using VirtualIT.Domain.VirtualMachines;
using VirtualIT.Shared.Beheerder;
using VirtualIT.Shared.Klanten;
using VirtualIT.Shared.Templates;

namespace VirtualIT.Shared.VirtualMachines;

public abstract class VirtualMachineDto
{
    public class Index
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Hostname { get; set; }
        public string FQDN { get; set; }
        public int Vcpu { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
    }

    public class Detail : Index
    {
        public Mode Mode { get; set; }
        public TemplateDto.Detail? Template { get; set; }
        public bool Backup { get; set; }
        public string? BackupFrequentie { get; set; }
        public Beschikbaarheid Beschikbaarheid { get; set; }
        public bool HighAvailable { get; set; }
        public KlantDto.Detail Klant { get; set; }
        public string EmailAanvrager { get; set; }
        public string TelefoonnummerAanvrager { get; set; }
        public DateTime DatumAanvraag { get; set; }
        public string NaamGebruiker { get; set; }
        public string RelatieGebruiker { get; set; }
        public string Reden { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Einddatum { get; set; }

        public Status Status { get; set; }
        public BeheerderDto.Detail ToegewezenAan { get; set; }
        public string ExterneToegangspoorten { get; set; }
    }

    public class Mutate {
        public string Naam { get; set; }
        public string Hostname { get; set; }
        public string FQDN { get; set; }
        public int Vcpu { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
        public Mode Mode { get; set; }
        public bool Backup { get; set; }
        public string? BackupFrequentie { get; set; }
        public Beschikbaarheid Beschikbaarheid { get; set; }
        public bool HighAvailable { get; set; }
        public string EmailAanvrager { get; set; }
        public string TelefoonnummerAanvrager { get; set; }
        public DateTime DatumAanvraag { get; set; }
        public string NaamGebruiker { get; set; }
        public string RelatieGebruiker { get; set; }
        public string Reden { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Einddatum { get; set; }
        public Status Status { get; set; }
        public string ExterneToegangspoorten { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Naam).NotEmpty().WithMessage("Naam mag niet leeg zijn!").MaximumLength(250);
                RuleFor(x => x.Hostname).NotEmpty().WithMessage("Hostname mag niet leeg zijn!").MaximumLength(250).Matches("^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$|^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\\-]*[a-zA-Z0-9])\\.)+([A-Za-z]|[A-Za-z][A-Za-z0-9\\-]*[A-Za-z0-9])$").WithMessage("Gelieve een juist geformateerde hostname te geven!");
                RuleFor(x => x.FQDN).NotEmpty().WithMessage("FQDN mag niet leeg zijn!").MaximumLength(253).Matches("^(?!:\\/\\/)(?=.{1,255}$)((.{1,63}\\.){1,127}(?![0-9]*$)[a-z0-9-]+\\.?)$").WithMessage("Gelieve een juist geformateerde FQDN te geven!");
                RuleFor(x => x.Vcpu).NotEmpty().WithMessage("Aantal virtuele CPU's mag niet leeg zijn").GreaterThan(0).WithMessage("Het aantal virtuele CPU's moet strikt positief zijn!");
                RuleFor(x => x.Memory).NotEmpty().WithMessage("Het aantal gigabyte RAM geheugen moet ingevuld zijn!").GreaterThan(0).WithMessage("Het aantal gigabyte RAM geheugen moet strikt positief zijn!");
                RuleFor(x => x.Storage).NotEmpty().WithMessage("Het aantal gigabyte opslaggeheugen moet ingevuld zijn!").GreaterThan(0).WithMessage("Het aantal gigabyte opslaggeheugen moet strikt positief zijn!");

                RuleFor(x => x.Mode).IsInEnum().WithMessage("Gelieve een mode te selecteren!");
                RuleFor(x => x.Backup).NotNull();
                RuleFor(x => x.Beschikbaarheid).IsInEnum().WithMessage("Gelieve een soort van de beschikbaarheden te geven in de lijst!");
                RuleFor(x => x.HighAvailable).NotNull();
                
                RuleFor(x => x.EmailAanvrager).NotEmpty().WithMessage("Het emailadres van de aanvrager mag niet leeg zijn!").EmailAddress().WithMessage("Gelieve een geldig emailadres in te vullen voor de aanvrager!");
                RuleFor(x => x.TelefoonnummerAanvrager).NotEmpty().WithMessage("Telefoonnummer van de aanvrager mag niet leeg zijn!").MaximumLength(25);
                RuleFor(x => x.DatumAanvraag).NotEmpty();
                RuleFor(x => x.NaamGebruiker).NotEmpty().WithMessage("Naam van de gebruiker mag niet leeg zijn!").MaximumLength(100);
                RuleFor(x => x.RelatieGebruiker).NotEmpty().WithMessage("Gelieve een relatie tussen de klant en aanvrager te geven!").MaximumLength(100);
                RuleFor(x => x.Reden).NotEmpty().WithMessage("Reden mag niet leeg zijn!").MaximumLength(100);
                RuleFor(x => x.Startdatum).NotEmpty().GreaterThan(DateTime.Today).WithMessage("Startdatum moet in de zijn toekomst!");
                RuleFor(x => x.Einddatum).NotEmpty().GreaterThanOrEqualTo(x => x.Startdatum).WithMessage("Einddatum moet op de zelfde dag of later in de toekomst liggen dan startdatum!");
                
                RuleFor(x => x.Status).IsInEnum().WithMessage("Gelieve een status te selecteren!");
                RuleFor(x => x.ExterneToegangspoorten).NotEmpty().WithMessage("Externe toegangspoorten mag niet leeg zijn!").Matches("^[0-9]+(,[0-9]+)*,?$").WithMessage("Gelieve de verschillende poort nummers te scheiden met komma's");
            }
        }
    }
}
