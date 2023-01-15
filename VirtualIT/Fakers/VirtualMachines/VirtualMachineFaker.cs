using VirtualIT.Domain.Beheerders;
using VirtualIT.Domain.Servers;
using Bogus;

namespace VirtualIT.Fakers.VirtualMachines
{
    public class VirtualMachineFaker : EntityFaker<VirtualMachine>
    {

        public VirtualMachineFaker(string locale = "nl") : base(locale)
        {

            CustomInstantiator(f =>
            {
                
                return new VirtualMachine("TestMachine" + f.UniqueIndex, "96.8.6.1", "fQDN", f.Random.Number(0, 5), f.Random.Number(1, 5) * 512, f.Random.Number(3, 10) * 512, Mode.PaaS, null, true, "wekelijks", (Beschikbaarheid)Enum.GetValues(typeof(Beschikbaarheid)).GetValue(f.Random.Number(1)), false, new Klant(false, "DIT", "Toegepaste Informatica", null, null, new Aanspreekpunt("Dieter", "Van Meerbeeck", "dieterjeweetwel@hotmail.com", "0000000000"), new Aanspreekpunt("Dieter", "Van Meerbeeck", "dieterjeweetwel@hotmail.com", "0000000000")), "marc@gmail.com", "+32 4708517660",
                      new DateTime(2022, 1, 1), f.Name.LastName(), f.Name.FirstName(), "onderzoek", new DateTime(2022, 12, 5), new DateTime(2022, 12, 19), Status.Afgehandeld, new Beheerder(f.Name.FirstName() , f.Name.LastName(), f.Person.Email, "DIT", Rol.Beheerder, true, "500"), "25,80");
            });
        }
    }
}