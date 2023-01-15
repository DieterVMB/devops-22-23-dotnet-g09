using FluentValidation;

namespace VirtualIT.Shared.Klanten;

public static class KlantDto 
{
    public class Index 
    {
        public int Id { get; set; }
        public bool Extern { get; set; }
        public string? KlantNaam { get; set; }
        public string? Departement { get; set; }
        public string? Opleiding { get; set; }
    }

    public class Detail: Index 
    {
        public string ExternType { get; set; }
        public AanspreekpuntDto Aanspreekpunt { get; set; }
        public AanspreekpuntDto BackupAanspreekpunt { get; set; }
    }

    public class Mutate 
    {
        public string? KlantNaam { get; set; }
        public bool Extern { get; set; }
        public string? Departement { get; set; }
        public string? Opleiding { get; set; } = default!;
        public string? ExternType { get; set; }
        public AanspreekpuntDto Aanspreekpunt { get; set; }
        public AanspreekpuntDto BackupAanspreekpunt { get; set; }

        public class Validator : AbstractValidator<Mutate> 
        {
            public Validator() 
            {
                When(x => !x.Extern, () =>
                {
                    RuleFor(x => x.Departement).NotEmpty().WithMessage("Departement mag niet leeg zijn!").Length(1, 250);
                    RuleFor(x => x.Opleiding).Length(0, 250);
                }).Otherwise(() =>
                {
                    RuleFor(x => x.KlantNaam).NotEmpty().WithMessage("Naam klant mag niet leeg zijn!").Length(1, 250);
                    RuleFor(x => x.ExternType).NotEmpty().WithMessage("Type klant mag niet leeg zijn!").Length(1, 250);
                });
                RuleFor(x => x.Aanspreekpunt).SetValidator(new AanspreekpuntDto.Validator());
                RuleFor(x => x.BackupAanspreekpunt).SetValidator(new AanspreekpuntDto.Validator());
            }
        }
    }
}
