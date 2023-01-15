using Bogus;

namespace VirtualIT.Shared.Klanten {
    /*public class FakeKlantService : IKlantService{
        private readonly List<KlantDto.Detail> _klanten = new();

        public FakeKlantService() {
            var klantIds = 0;
            var klantFaker = new Faker<KlantDto.Detail>("nl")
                .UseSeed(1337)
                .RuleFor(x => x.Id, _ => ++klantIds)
                .RuleFor(x => x.Extern, _ => true)
                .RuleFor(x => x.Departement, _ => new DepartementFaker().Generate())
                .RuleFor(x => x.Opleiding, _ => new OpleidingFaker().Generate())
                .RuleFor(x => x.KlantNaam, f => f.Company.CompanyName())
                .RuleFor(x => x.ExternType, _ => "Vennootschap")
                .RuleFor(x => x.Aanspreekpunt, _ => new AanspreekpuntFaker().Generate())
                .RuleFor(x => x.BackupAanspreekpunt, _ => new AanspreekpuntFaker().Generate());
            _klanten = klantFaker.Generate(10);
        }

        public Task<KlantDto.Detail> GetDetailAsync(int klantId) {
            return Task.FromResult(_klanten.Single(x => x.Id == klantId));
        }

        public Task<IEnumerable<KlantDto.Index>> GetIndexAsync(KlantRequest.Index request) {
            return Task.FromResult(_klanten.Select(x => new KlantDto.Index {
                Id = x.Id,
                KlantNaam = x.KlantNaam
            }));
        }
    }*/
}
