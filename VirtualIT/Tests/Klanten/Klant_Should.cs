using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualIT.Tests.Klanten
{
    using Bogus.DataSets;
    using Castle.Core.Resource;
    using global::VirtualIT.Domain.Beheerders;
    using global::VirtualIT.Domain.Klanten;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;

    namespace VirtualIT.Tests.Beheerders
    {
        public class Klant_Should
        {

            [Fact]
            public void Be_created_when_valid()
            {
                bool externe = true;
                string voornaam = "Tonya";
                string naam = "Dewulf";
                string email = "Tony.Dewulf@gmail.com";
                string telefoonnummer = "04675383378";
                Aanspreekpunt aanspreekpunt = new Aanspreekpunt(voornaam, naam, email, telefoonnummer);
                Aanspreekpunt backupaanspreekpunt = new Aanspreekpunt(voornaam, naam, email, telefoonnummer);


                var klant = new Klant(externe, null, null, null, null, aanspreekpunt, backupaanspreekpunt);

                klant.Extern.ShouldBe(externe);
                klant.Aanspreekpunt.ShouldBe(aanspreekpunt);
                klant.BackupAanspreekpunt.ShouldBe(backupaanspreekpunt);

            }
            [Fact]
            public void Throw_when_invalid_backupaanspreekpunt()
            {
                bool externe = true;
                string voornaam = "Tonya";
                string naam = "Dewulf";
                string email = "Tonya.Dewulf@gmail.com";
                string telefoonnummer = "04675383378";
                Aanspreekpunt aanspreekpunt = new Aanspreekpunt(voornaam, naam, email, telefoonnummer);
                Aanspreekpunt backupaanspreekpunt = null;//new Aanspreekpunt(voornaam, naam, email, telefoonnummer);


                Action act = () =>
                {
                    var klant = new Klant(externe, null, null, null, null, aanspreekpunt, backupaanspreekpunt);
                };

                act.ShouldThrow<Exception>();
            }


        }
    }
}
