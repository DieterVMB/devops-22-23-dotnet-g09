namespace VirtualIT.Fakers.Klanten {
    public class AanspreekpuntFaker : Faker<Aanspreekpunt> {
        public AanspreekpuntFaker(string locale = "nl"):base(locale) {
            //CustomInstantiator(f => new Aanspreekpunt(f.Name.FirstName(), f.Name.LastName(), f.Person.Email, "047166784679"));
        }
    }
}
