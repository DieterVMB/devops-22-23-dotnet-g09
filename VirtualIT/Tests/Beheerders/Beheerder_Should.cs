using Bogus.DataSets;
using Castle.Core.Resource;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VirtualIT.Domain.Beheerders;
using Xunit;

namespace VirtualIT.Tests.Beheerders
{
    public class Beheerder_Should
    {
        
        [Fact]
        public void Be_created_when_valid()
        {
            string voornaam = "Lars";
            string naam = "Dehandschutter";
            string email = "larsdh18@gmail.com" ;
            string departement = "Hogent";
            Rol rol = Rol.Beheerder;
            bool isActief = true;
            string auth0Id = "1";

            var beheerder = new Beheerder(voornaam, naam, email, departement, rol, isActief, auth0Id);

            beheerder.Voornaam.ShouldBe(voornaam);
            beheerder.Naam.ShouldBe(naam);
            beheerder.Email.ShouldBe(email);
            beheerder.Departement.ShouldBe(departement);
            beheerder.Rol.ShouldBe(rol);
            beheerder.IsActief.ShouldBe(isActief);
            beheerder.Auth0Id.ShouldBe(auth0Id);
        }
        [Fact]
        public void Throw_when_invalid_email()
        {
            string voornaam = "Lars";
            string naam = "Dehandschutter";
            string email = "";
            string departement = "Hogent";
            Rol rol = Rol.Beheerder;
            bool isActief = true;
            string auth0Id = "1";


            Action act = () =>
            {
                var beheerder = new Beheerder(voornaam, naam, email, departement, rol, isActief, auth0Id);
            };

            act.ShouldThrow<Exception>();
        }

        
    }
}

