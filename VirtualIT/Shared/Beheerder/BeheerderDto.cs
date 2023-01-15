using FluentValidation;
using VirtualIT.Domain.Beheerders;

namespace VirtualIT.Shared.Beheerder;

public abstract class BeheerderDto
{
    public class Index
    {
        public int Id { get; set; }

        public string Voornaam { get; set; }

        public string Naam { get; set; }

        public string Email { get; set; }
        public Rol Rol { get; set; }
    }

    public class Detail : Index
    {
        public string Departement { get; set; }

        public bool IsActief { get; set; }
    }
    public class Create
    {
        public string Voornaam { get; set; }

        public string Naam { get; set; }

        public string Email { get; set; }

        public string Departement { get; set; }

        public Rol Rol { get; set; }

        public string Wachtwoord { get; set; }
        public string BevestigWachtwoord { get; set; }

        public bool IsActief { get; set; } = true;

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                RuleFor(x => x.Voornaam).NotEmpty().WithMessage("Voornaam mag niet leeg zijn!").Length(1, 100).WithMessage("Voornaam mag maar 100 karakters lang zijn");
                RuleFor(x => x.Naam).NotEmpty().WithMessage("Naam mag niet leeg zijn!").Length(1, 100).WithMessage("Naam mag maar 100 karakters lang zijn");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email mag niet leeg zijn!").Length(1, 100).EmailAddress().WithMessage("Geef een correcte email!");
                RuleFor(x => x.Departement).NotEmpty().WithMessage("Departement mag niet leeg zijn!").Length(1, 100);
                RuleFor(x => x.Wachtwoord).NotEmpty().WithMessage("Wachtwoord mag niet leeg zijn!").MinimumLength(8).WithMessage("Wachtwoord moet minstens 8 karakters bevatten!").Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{0,}$").WithMessage("Wachtwoord moet bestaan uit minstens 1 hoofdletter, 1 cijfer en 1 van de volgende speciale tekens: !@#$&*");
                RuleFor(x => x.BevestigWachtwoord).NotEmpty().WithMessage("Bevestig wachtwoord mag niet leeg zijn!").Equal(x => x.Wachtwoord).WithMessage("Wachtwoord en bevestig wachtwoord moeten hetzelfde zijn!");
            }
        }
    }

    public class Edit {
        public string Voornaam { get; set; }

        public string Naam { get; set; }

        public string Email { get; set; }

        public string Departement { get; set; }

        public Rol Rol { get; set; }

        public string? Wachtwoord { get; set; }
        public string? BevestigWachtwoord { get; set; }

        public bool IsActief { get; set; } = true;

        public class Validator : AbstractValidator<Edit> {
            public Validator() {
                RuleFor(x => x.Voornaam).NotEmpty().WithMessage("Voornaam mag niet leeg zijn!").Length(1, 100).WithMessage("Voornaam mag maar 100 karakters lang zijn");
                RuleFor(x => x.Naam).NotEmpty().WithMessage("Naam mag niet leeg zijn!").Length(1, 100).WithMessage("Naam mag maar 100 karakters lang zijn");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email mag niet leeg zijn!").Length(1, 100).EmailAddress().WithMessage("Geef een correcte email!");
                RuleFor(x => x.Departement).NotEmpty().WithMessage("Departement mag niet leeg zijn!").Length(1, 100);
                RuleFor(x => x.Wachtwoord).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{0,}$").WithMessage("Wachtwoord moet bestaan uit minstens 1 hoofdletter, 1 cijfer en 1 van de volgende speciale tekens: !@#$&*");
                RuleFor(x => x.BevestigWachtwoord).Equal(x => x.Wachtwoord).WithMessage("Wachtwoord en bevestig wachtwoord moeten hetzelfde zijn!");
            }
        }
    }
}

