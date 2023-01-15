using Microsoft.EntityFrameworkCore;
using VirtualIT.Domain.Beheerders;
using VirtualIT.Domain.Klanten;
using VirtualIT.Domain.Servers;
using VirtualIT.Domain.Templates;
using VirtualIT.Domain.VirtualMachines;
using VirtualIT.Fakers.Klanten;
using VirtualIT.Fakers.VirtualMachines;

namespace VirtualIT.Persistence;

public class FakeSeeder 
{
    private readonly VirtualITDbContext dbContext;

    public FakeSeeder(VirtualITDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public void Seed() {
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS VirtualMachines");
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS Templates");
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS Servers");
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS Klanten");
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS Beheerders");
        dbContext.Database.EnsureCreated();

        List<Server> servers = new();
        List<VirtualMachine> vms = new();
        List<Beheerder> beheerders = new();
        List<Klant> klanten = new();
        List<Template> templates = new();

        Server srv001 = new("srv001", 5000, 8, 8);
        Server srv002 = new("srv002", 5000, 8, 8);
        Server srv003 = new("srv003", 5000, 8, 8);
        Server srv004 = new("srv004", 5000, 8, 8);
        Server srv005 = new("srv005", 5000, 8, 8);

        Klant klant1 = new(false, "IT en Digitale Innovatie", "Toegepaste Informatica", null, null, new Aanspreekpunt("Brian", "Luo", "brianluo@gmail.com", "0475956735"), new Aanspreekpunt("Dieter", "Van Meerbeeck", "dietervanmeerbeeck@hotmail.com", "0471016969"));
        Klant klant2 = new(false, "Gezondheidszorg", "Verpleegkunde", null, null, new Aanspreekpunt("Brian", "Luo", "brianluo@gmail.com", "0475956735"), new Aanspreekpunt("Dieter", "Van Meerbeeck", "dietervanmeerbeeck@hotmail.com", "0471016969"));
        Klant klant3 = new(false, "Bedrijf en Organisatie", "Bedrijfsmanagement", null, null, new Aanspreekpunt("Xander", "Nuytinck", "xandernuytinck@gmail.com", "0471017595"), new Aanspreekpunt("Brian", "Luo", "brianluo@gmail.com", "0475956735"));
        Klant klant4 = new(true, null, null, "KBC", "UNIZO", new Aanspreekpunt("Egon", "Dehandschutter", "egondehandschutter@hotmail.com", "0471019543"), new Aanspreekpunt("Dieter", "Van Meerbeeck", "dietervanmeerbeeck@hotmail.com", "0471016969"));
        Klant klant5 = new(true, null, null, "Proximus", "VOKA", new Aanspreekpunt("Lars", "Dehandschutter", "larsdehandschutter@gmail.com", "0471028374"), new Aanspreekpunt("Dieter", "Van Meerbeeck", "dietervanmeerbeeck@hotmail.com", "0471016969"));

        Beheerder bd = new("Dieter", "Van Meerbeeck", "dieter.vanmeerbeeck@student.hogent.be", "IT en Digitale Innovatie", Rol.Beheerder, true, "auth0|63a84b54d53ea3596eb38cd9");
        Beheerder bx = new("Xander", "Nuytinck", "xander.nuytinck@student.hogent.be", "IT en Digitale Innovatie", Rol.Beheerder, true, "auth0|63a84b7ad53ea3596eb38cdb");
        Beheerder bbl = new("Brian", "Luo", "zhao.luo@student.hogent.be", "IT en Digitale Innovatie", Rol.Beheerder, true, "auth0|63a84b323a95d517efd87641");
        Beheerder bbv = new("Beheerder", "VIC", "beheerder@vic.hogent.be", "VIC", Rol.Beheerder, true, "auth0|63a84e32d53ea3596eb38ce1");
        Beheerder bl = new("Lars", "Dehandschutter", "lars.dehandschutter@student.hogent.be", "IT en Digitale Innovatie", Rol.Gebruiker, true, "auth0|63a32e66670077b1b9aa84fc");
        Beheerder bge = new("Egon", "Dehandschutter", "egon.dehandschutter@student.hogent.be", "IT en Digitale Innovatie", Rol.Gebruiker, true, "auth0|63a84afcf042eff5b09cbccb");
        Beheerder bg = new("Gebruiker", "VIC", "gebruiker@vic.hogent.be", "VIC", Rol.Gebruiker, true, "auth0|63a84e54d53ea3596eb38ce3");

        Template mSSQL = new("Microsoft SQL", "MSSQL Server, Microsoft SQL Server Management Studio");
        Template mySql = new("MySQL", "MySQL Server, MySQL Workbench");
        Template development = new("Development", "Python3, Visual Studio Code, Eclipse, NodeJS, Java, Scenebuilder");
        Template texteditor = new("Text editors", "MikTex, TexStudio, JabRef, Microsoft Word");

        VirtualMachine vm001 = new("vm001", "vm001.be", "vic.srv001-vm001.be", 2, 2, 500, Mode.SaaS, mySql, false, null, Beschikbaarheid.HeleDag, true, klant1, "dietervanmeerbeeck@hotmail.com", "0471019935", new DateTime(2022, 12, 24), "Dieter Van Meerbeeck", "student", "Bachelorproef", new DateTime(2023, 1, 25), new DateTime(2023, 1, 27), Status.InBehandeling, bd, "25,80");
        VirtualMachine vm002 = new("vm002", "vm002.be", "vic.srv005-vm002.be", 2, 2, 600, Mode.IAAS, null, true, "dagelijks", Beschikbaarheid.BusinessUren, false, klant2, "jorrit.campens@hogent.be", "0485648353", new DateTime(2022, 12, 25), "Jorrit Campens", "Lector verpleegkunde", "Verwerking testdata", new DateTime(2023, 1, 17), new DateTime(2023, 1, 27), Status.InBehandeling, bbl, "80");
        VirtualMachine vm003 = new("vm003", "vm003.be", "vic.srv002-vm003.be", 3, 3, 1000, Mode.PaaS, development, false, null, Beschikbaarheid.HeleDag, true, klant1, "jelle.delporte@student.hogent.be", "0478348976", new DateTime(2022, 12, 19), "Jelle Delporte", "Student Toegepaste Informatica software development", "Testomgeving development BP", new DateTime(2023, 2, 1), new DateTime(2023, 5, 25), Status.Aangevraagd, bx, "21,22, 80, 443,990");
        VirtualMachine vm004 = new("vm004", "vm004.be", "vic.src003-vm004.be", 4, 4, 2000, Mode.IAAS, null, true, "dagelijks", Beschikbaarheid.HeleDag, true, klant1, "victor.vanhooren@student.hogent.be", "0487648664", new DateTime(2023, 1, 2), "Victor Vanhooren", "Student Toegepaste Informatica operations", "Testomgeving OPS BP", new DateTime(2023, 2, 1), new DateTime(2023, 5, 25), Status.Aangevraagd, bd, "21,22,80,443,990");
        VirtualMachine vm005 = new("vm005", "vm005.be", "vic.srv004-vm005.be", 1, 1, 1000, Mode.PaaS, mSSQL, true, "dagelijks", Beschikbaarheid.HeleDag, true, klant4, "jan.janssens@kbc.be", "0471523648", new DateTime(2022, 3, 18), "Jan Janssens", "Data analist", "Tijdelijke database storage voor verhuis andere db systeem", new DateTime(2022, 4, 10), new DateTime(2022, 4, 24), Status.Afgehandeld, bbv, "21,22");
        VirtualMachine vm006 = new("vm006", "vm006.be", "vic.src002-vm006.be", 3, 3, 1000, Mode.IAAS, null, false, null, Beschikbaarheid.BusinessUren, false, klant3, "karendevos@hogent.com", "0471287365", new DateTime(2022, 2, 3), "Karen Devos", "Lector bedrijfsmanagement vak boekhouden", "Platform voor boekhoudprogramma", new DateTime(2022, 2, 15), new DateTime(2022, 5, 20), Status.Afgehandeld, bd, "21,22");
        VirtualMachine vm007 = new("vm007", "vm007.be", "vic.srv001-vm007.be", 4, 4, 2000, Mode.IAAS, null, true, "dagelijks", Beschikbaarheid.BusinessUren, true, klant5, "karelsmet@proximus.be", "0947534861", new DateTime(2022, 12, 1), "Karel Smet", "Lead Netwerk Departement", "DNS Server optimalisatie test", new DateTime(2022, 12, 14), new DateTime(2023, 2, 12), Status.Afgehandeld, bbl, "53");
        VirtualMachine vm008 = new("vm008", "vm008.be", "vic.srv004-vm008.be", 1, 1, 250, Mode.PaaS, texteditor, false, null, Beschikbaarheid.BusinessUren, false, klant3, "jonas.pieters@student.hogent.be", "0474857128", new DateTime(2022, 8, 20), "Jonas Pieters", "Laatste jaar student Bedrijfsmanagement", "Bachelorproef schrijven voor afstuderen semester 1", new DateTime(2022, 9, 22), new DateTime(2022, 12, 25), Status.Afgehandeld, bx, "21,22");
        VirtualMachine vm009 = new("vm009", "vm009.be", "vic.srv005-vm009.be", 3, 3, 500, Mode.IAAS, null, false, null, Beschikbaarheid.HeleDag, true, klant1, "koen.verstraten@hogent.be", "0471687567", new DateTime(2022, 12, 28), "Koen Verstraten", "Student Ops", "Stresstest webserver", new DateTime(2023, 1, 12), new DateTime(2023, 1, 30), Status.InBehandeling, bbv, "21, 22, 80, 443");
        VirtualMachine vm010 = new("vm010", "vm010.be", "vic.srv003-vm010.be", 2, 2, 750, Mode.SaaS, mySql, true, "wekelijks", Beschikbaarheid.BusinessUren, false, klant4, "lauravandevelde@accounting.kbc.be", "048716898", new DateTime(2022, 12, 25), "Laura Van de Velde", "Manager accounting departement", "Veilig platform samenwerking met extern accounting bedrijf", new DateTime(2023, 1, 25), new DateTime(2023, 2, 25), Status.Aangevraagd, bbl, "21,22");

        
        beheerders.Add(bd);
        beheerders.Add(bx);
        beheerders.Add(bbl);
        beheerders.Add(bbv);
        beheerders.Add(bl);
        beheerders.Add(bge);
        beheerders.Add(bg);

        klanten.Add(klant1);
        klanten.Add(klant2);
        klanten.Add(klant3);
        klanten.Add(klant4);
        klanten.Add(klant5);

        templates.Add(mSSQL);
        templates.Add(mySql);
        templates.Add(development);
        templates.Add(texteditor);

        vms.Add(vm001);
        vms.Add(vm002);
        vms.Add(vm003);
        vms.Add(vm004);
        vms.Add(vm005);
        vms.Add(vm006);
        vms.Add(vm007);
        vms.Add(vm008);
        vms.Add(vm009);
        vms.Add(vm010);

        srv001.VoegVMToeAanServer(vm001);
        srv001.VoegVMToeAanServer(vm007);
        srv002.VoegVMToeAanServer(vm003);
        srv002.VoegVMToeAanServer(vm006);
        srv003.VoegVMToeAanServer(vm004);
        srv003.VoegVMToeAanServer(vm010);
        srv004.VoegVMToeAanServer(vm005);
        srv004.VoegVMToeAanServer(vm008);
        srv005.VoegVMToeAanServer(vm002);
        srv005.VoegVMToeAanServer(vm009);

        servers.Add(srv001);
        servers.Add(srv002);
        servers.Add(srv003);
        servers.Add(srv004);
        servers.Add(srv005);

        dbContext.Templates.AddRange(templates);
        dbContext.Beheerders.AddRange(beheerders);
        dbContext.Klanten.AddRange(klanten);
        dbContext.VirtualMachines.AddRange(vms);
        dbContext.Servers.AddRange(servers);
        dbContext.SaveChanges();
    }
}
