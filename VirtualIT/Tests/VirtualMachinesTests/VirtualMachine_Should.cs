using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualIT.Domain.Beheerders;
using VirtualIT.Domain.Klanten;
using VirtualIT.Domain.VirtualMachines;
using Xunit;

namespace VirtualIT.Tests.VirtualMachinesTests
{
    public class VirtualMachine_Should
    {

        [Fact]
        public void Be_created_when_valid()
        {
            string naam = "Virtual";
            string hostname = "machine";
            string fqdn = "1";
            int vcpu = 1;
            int memory = 500;
            int storage = 12;
            Mode mode = Mode.PaaS;
            bool backup = true;
            Beschikbaarheid beschikbaarheid = Beschikbaarheid.HeleDag;
            bool highAvailable = true;
            Klant klant = new Klant(true, "IT", "informatica", "jan", null, new Aanspreekpunt("lars", "Dehandschutter", "larsdh18@gmail.com", "04"), new Aanspreekpunt("egon", "Dehandschutter", "egondh18@gmail.com", "0468"));
            string emailAanvrager = "jan.jansens@gmail.com";
            string telefoonnummerAanvrager = "jan.jansens@gmail.com";
            DateTime datumAanvraag = DateTime.Now;
            string naamGebruiker = "Lars Dehandschutter";
            string relatieGebruiker = "bedrijf";
            string reden = "BP";
            DateTime startdatum = DateTime.Now;
            DateTime einddatum = DateTime.MaxValue;
            Status status = Status.Aangevraagd;
            Beheerder toegewezenAan = new Beheerder("jeff", "kool", "jeff.kool@gmail.com", "VIC", Rol.Beheerder, true, "1");
            string externeToegangspoorten = "usb";

            var vm = new VirtualMachine(naam, hostname, fqdn, vcpu, memory, storage, mode, null, backup, null, beschikbaarheid, highAvailable, klant, emailAanvrager, telefoonnummerAanvrager, datumAanvraag, naamGebruiker, relatieGebruiker, reden, startdatum, einddatum, status, toegewezenAan, externeToegangspoorten);

            
            vm.Naam.ShouldBe(naam);
            vm.Hostname.ShouldBe(hostname);
            vm.FQDN.ShouldBe(fqdn);
            vm.Vcpu.ShouldBe(vcpu);
            vm.Memory.ShouldBe(memory);
            vm.Storage.ShouldBe(storage);
            vm.Mode.ShouldBe(mode);
            vm.Backup.ShouldBe(backup);
            vm.Beschikbaarheid.ShouldBe(beschikbaarheid);
            vm.HighAvailable.ShouldBe(highAvailable);
            vm.Klant.ShouldBe(klant);
            vm.EmailAanvrager.ShouldBe(emailAanvrager);
            vm.TelefoonnummerAanvrager.ShouldBe(telefoonnummerAanvrager);
            vm.DatumAanvraag.ShouldBe(datumAanvraag);
            vm.NaamGebruiker.ShouldBe(naamGebruiker);
            vm.RelatieGebruiker.ShouldBe(relatieGebruiker);
            vm.Reden.ShouldBe(reden);
            vm.Startdatum.ShouldBe(startdatum);
            vm.Einddatum.ShouldBe(einddatum);
            vm.Status.ShouldBe(status);
            vm.ToegewezenAan.ShouldBe(toegewezenAan);
            vm.ExterneToegangspoorten.ShouldBe(externeToegangspoorten);
        }
        [Fact]
        public void Throw_when_invalid_naam()
        {
            string naam = "";
            string hostname = "machine";
            string fqdn = "1";
            int vcpu = 1;
            int memory = 500;
            int storage = 12;
            Mode mode = Mode.PaaS;
            bool backup = true;
            Beschikbaarheid beschikbaarheid = Beschikbaarheid.HeleDag;
            bool highAvailable = true;
            Klant klant = new Klant(true, "IT", "informatica", "jan", null, new Aanspreekpunt("lars", "Dehandschutter", "larsdh18@gmail.com", "04"), new Aanspreekpunt("egon", "Dehandschutter", "egondh18@gmail.com", "0468"));
            string emailAanvrager = "jan.jansens@gmail.com";
            string telefoonnummerAanvrager = "jan.jansens@gmail.com";
            DateTime datumAanvraag = DateTime.Now;
            string naamGebruiker = "Lars Dehandschutter";
            string relatieGebruiker = "bedrijf";
            string reden = "BP";
            DateTime startdatum = DateTime.Now;
            DateTime einddatum = DateTime.MaxValue;
            Status status = Status.Aangevraagd;
            Beheerder toegewezenAan = new Beheerder("jeff", "kool", "jeff.kool@gmail.com", "VIC", Rol.Beheerder, true, "1");
            string externeToegangspoorten = "usb";


            Action act = () =>
            {
                var vm = new VirtualMachine(naam, hostname, fqdn, vcpu, memory, storage, mode, null, backup, null, beschikbaarheid, highAvailable, klant, emailAanvrager, telefoonnummerAanvrager, datumAanvraag, naamGebruiker, relatieGebruiker, reden, startdatum, einddatum, status, toegewezenAan, externeToegangspoorten);
            };

            act.ShouldThrow<Exception>();
        }
        [Fact]
        public void Throw_when_invalid_vcpu()
        {
            string naam = "virtual";
            string hostname = "machine";
            string fqdn = "1";
            int vcpu = 0;
            int memory = 500;
            int storage = 12;
            Mode mode = Mode.PaaS;
            bool backup = true;
            Beschikbaarheid beschikbaarheid = Beschikbaarheid.HeleDag;
            bool highAvailable = true;
            Klant klant = new Klant(true, "IT", "informatica", "jan", null, new Aanspreekpunt("lars", "Dehandschutter", "larsdh18@gmail.com", "04"), new Aanspreekpunt("egon", "Dehandschutter", "egondh18@gmail.com", "0468"));
            string emailAanvrager = "jan.jansens@gmail.com";
            string telefoonnummerAanvrager = "jan.jansens@gmail.com";
            DateTime datumAanvraag = DateTime.Now;
            string naamGebruiker = "Lars Dehandschutter";
            string relatieGebruiker = "bedrijf";
            string reden = "BP";
            DateTime startdatum = DateTime.Now;
            DateTime einddatum = DateTime.MaxValue;
            Status status = Status.Aangevraagd;
            Beheerder toegewezenAan = new Beheerder("jeff", "kool", "jeff.kool@gmail.com", "VIC", Rol.Beheerder, true, "1");
            string externeToegangspoorten = "usb";


            Action act = () =>
            {
                var vm = new VirtualMachine(naam, hostname, fqdn, vcpu, memory, storage, mode, null, backup, null, beschikbaarheid, highAvailable, klant, emailAanvrager, telefoonnummerAanvrager, datumAanvraag, naamGebruiker, relatieGebruiker, reden, startdatum, einddatum, status, toegewezenAan, externeToegangspoorten);
            };

            act.ShouldThrow<Exception>();
        }
        [Fact]
        public void Throw_when_invalid_Klant()
        {
            string naam = "";
            string hostname = "machine";
            string fqdn = "1";
            int vcpu = 1;
            int memory = 500;
            int storage = 12;
            Mode mode = Mode.PaaS;
            bool backup = true;
            Beschikbaarheid beschikbaarheid = Beschikbaarheid.HeleDag;
            bool highAvailable = true;
            Klant klant = null;
            string emailAanvrager = "jan.jansens@gmail.com";
            string telefoonnummerAanvrager = "jan.jansens@gmail.com";
            DateTime datumAanvraag = DateTime.Now;
            string naamGebruiker = "Lars Dehandschutter";
            string relatieGebruiker = "bedrijf";
            string reden = "BP";
            DateTime startdatum = DateTime.Now;
            DateTime einddatum = DateTime.MaxValue;
            Status status = Status.Aangevraagd;
            Beheerder toegewezenAan = new Beheerder("jeff", "kool", "jeff.kool@gmail.com", "VIC", Rol.Beheerder, true, "1");
            string externeToegangspoorten = "usb";


            Action act = () =>
            {
                var vm = new VirtualMachine(naam, hostname, fqdn, vcpu, memory, storage, mode, null, backup, null, beschikbaarheid, highAvailable, klant, emailAanvrager, telefoonnummerAanvrager, datumAanvraag, naamGebruiker, relatieGebruiker, reden, startdatum, einddatum, status, toegewezenAan, externeToegangspoorten);
            };

            act.ShouldThrow<Exception>();
        }
        [Fact]
        public void Throw_when_invalid_Date()
        {
            string naam = "";
            string hostname = "machine";
            string fqdn = "1";
            int vcpu = 1;
            int memory = 500;
            int storage = 12;
            Mode mode = Mode.PaaS;
            bool backup = true;
            Beschikbaarheid beschikbaarheid = Beschikbaarheid.HeleDag;
            bool highAvailable = true;
            Klant klant = null;
            string emailAanvrager = "jan.jansens@gmail.com";
            string telefoonnummerAanvrager = "jan.jansens@gmail.com";
            DateTime datumAanvraag = DateTime.Now;
            string naamGebruiker = "Lars Dehandschutter";
            string relatieGebruiker = "bedrijf";
            string reden = "BP";
            DateTime startdatum = DateTime.MinValue;
            DateTime einddatum = DateTime.MaxValue;
            Status status = Status.Aangevraagd;
            Beheerder toegewezenAan = new Beheerder("jeff", "kool", "jeff.kool@gmail.com", "VIC", Rol.Beheerder, true, "1");
            string externeToegangspoorten = "usb";


            Action act = () =>
            {
                var vm = new VirtualMachine(naam, hostname, fqdn, vcpu, memory, storage, mode, null, backup, null, beschikbaarheid, highAvailable, klant, emailAanvrager, telefoonnummerAanvrager, datumAanvraag, naamGebruiker, relatieGebruiker, reden, startdatum, einddatum, status, toegewezenAan, externeToegangspoorten);
            };

            act.ShouldThrow<Exception>();
        }
    }
}
