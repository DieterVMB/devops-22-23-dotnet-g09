using Bogus;

namespace VirtualIT.Shared.Klanten {
    public class AanspreekpuntFaker : Faker<AanspreekpuntDto> {
        public AanspreekpuntFaker(string locale = "nl") {
            base.RuleFor(x => x.Voornaam, f => f.Name.FirstName())
                .RuleFor(x => x.Naam, f => f.Name.LastName())
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Telefoonnummer, f => "047166784679");
        }

        public AanspreekpuntDto Generate() {
            return base.Generate();
        }
    }
}
