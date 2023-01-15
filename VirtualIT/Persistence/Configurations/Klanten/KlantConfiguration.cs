using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualIT.Domain.Klanten;

namespace VirtualIT.Persistence.Configurations.Klanten;

internal class KlantConfiguration : IEntityTypeConfiguration<Klant>
{
    public void Configure(EntityTypeBuilder<Klant> builder)
    {
        builder.OwnsOne(x => x.Aanspreekpunt, aanspreekpunt =>
        {
            aanspreekpunt.Property(x => x.Voornaam);
            aanspreekpunt.Property(x => x.Naam);
            aanspreekpunt.Property(x => x.Email);
            aanspreekpunt.Property(x => x.Telefoonnummer);
        });

        builder.OwnsOne(x => x.BackupAanspreekpunt, backupAanspreekpunt =>
        {
            backupAanspreekpunt.Property(x => x.Voornaam);
            backupAanspreekpunt.Property(x => x.Naam);
            backupAanspreekpunt.Property(x => x.Email);
            backupAanspreekpunt.Property(x => x.Telefoonnummer);
        });
    }
}
