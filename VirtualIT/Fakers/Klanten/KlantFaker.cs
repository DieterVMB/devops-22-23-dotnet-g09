namespace VirtualIT.Fakers.Klanten {
    public class KlantFaker : EntityFaker<Klant> {
        public KlantFaker(string locale = "nl") : base(locale) {
            CustomInstantiator(f => new Klant(true, "DIT", "Toegepaste Informatica",
                f.Company.CompanyName(), "Vennootschap", new AanspreekpuntFaker().Generate(), new AanspreekpuntFaker().Generate()));
        }
    }
}
