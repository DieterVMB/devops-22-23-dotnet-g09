using FluentValidation;

namespace VirtualIT.Shared.Klanten;

public class AanspreekpuntDto 
{
    public string Voornaam { get; set; }
    public string Naam { get; set;}
    public string Email { get; set; }
    public string Telefoonnummer { get; set; }

    public class Validator : AbstractValidator<AanspreekpuntDto> 
    {
        public Validator() {
            RuleFor(x => x.Voornaam).NotEmpty().WithMessage("Voornaam mag niet leeg zijn!").Length(1, 250);
            RuleFor(x => x.Naam).NotEmpty().WithMessage("Naam mag niet leeg zijn!").Length(1, 250);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email mag niet leeg zijn!").Length(1, 250).EmailAddress().WithMessage("Geef een geldig email adres!");
            RuleFor(x => x.Telefoonnummer).NotEmpty().WithMessage("Telefoonnummer mag niet leeg zijn!").Length(1, 25);
        }
    }
}
