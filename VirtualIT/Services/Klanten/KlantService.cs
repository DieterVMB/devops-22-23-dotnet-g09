using Microsoft.EntityFrameworkCore;
using VirtualIT.Persistence;
using VirtualIT.Shared.Klanten;
using VirtualIT.Domain.Exceptions;
using VirtualIT.Domain.Klanten;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi;

namespace VirtualIT.Services.Klanten {
    public class KlantService: IKlantService {
        private readonly VirtualITDbContext dbContext;
        private readonly IManagementApiClient managementApiClient;

        public KlantService(VirtualITDbContext dbContext, IManagementApiClient managementApiClient) {
            this.dbContext = dbContext;
            this.managementApiClient = managementApiClient;
        }

        public async Task<int> CreateAsync(KlantDto.Mutate model) {

            if (model.Extern) {
                if (await dbContext.Klanten.AnyAsync(x => x.KlantNaam == model.KlantNaam))
                    throw new EntityAlreadyExistsException(nameof(Klant), nameof(Klant.KlantNaam), model.KlantNaam);
            } else {
                if (await dbContext.Klanten.AnyAsync(x => x.Departement == model.Departement && x.Opleiding == model.Opleiding))
                    throw new EntityAlreadyExistsException(nameof(Klant), nameof(Klant.Departement) + " + " + nameof(Klant.Opleiding), model.Departement + " + " + model.Opleiding);
            }

            string? opleiding = null;
            if(model.Opleiding != null) {
                opleiding = model.Opleiding;
            }
            Aanspreekpunt aanspreekpunt = new Aanspreekpunt(model.Aanspreekpunt.Voornaam, model.Aanspreekpunt.Naam, model.Aanspreekpunt.Email, model.Aanspreekpunt.Telefoonnummer);
            Aanspreekpunt backupAanspreekpunt = new Aanspreekpunt(model.BackupAanspreekpunt.Voornaam, model.BackupAanspreekpunt.Naam, model.BackupAanspreekpunt.Email, model.BackupAanspreekpunt.Telefoonnummer);

            Klant klant = new Klant(model.Extern, model.Departement, opleiding, model.KlantNaam, model.ExternType, aanspreekpunt, backupAanspreekpunt);

            dbContext.Klanten.Add(klant);
            await dbContext.SaveChangesAsync();

            return klant.Id;
        }

        public async Task EditAsync(int klantId, KlantDto.Mutate model) {
            Klant? klant = await dbContext.Klanten.SingleOrDefaultAsync(x => x.Id == klantId);
            if(klant is null) {
                throw new EntityNotFoundException(nameof(Klant), klantId);
            }

            klant.Extern = model.Extern!;
            klant.Departement = model.Departement!;
            if(model.Opleiding != null) {
                klant.Opleiding = model.Opleiding;
            }
            klant.KlantNaam = model.KlantNaam!;
            klant.ExternType = model.ExternType!;
            klant.Aanspreekpunt = new Aanspreekpunt(model.Aanspreekpunt.Voornaam, model.Aanspreekpunt.Naam, model.Aanspreekpunt.Email, model.Aanspreekpunt.Telefoonnummer);
            klant.BackupAanspreekpunt = new Aanspreekpunt(model.BackupAanspreekpunt.Voornaam, model.BackupAanspreekpunt.Naam, model.BackupAanspreekpunt.Email, model.BackupAanspreekpunt.Telefoonnummer);

            await dbContext.SaveChangesAsync();
        }

        public async Task<KlantResponse.Detail> GetAllDetailASync()
        {
            var items = await dbContext.Klanten.Select(x => new KlantDto.Detail
            {
                Id = x.Id,
                Extern = x.Extern,
                Departement = x.Departement,
                Opleiding = x.Opleiding,
                KlantNaam = x.KlantNaam,
                ExternType = x.ExternType,
                Aanspreekpunt = new AanspreekpuntDto
                {
                    Voornaam = x.Aanspreekpunt.Voornaam,
                    Naam = x.Aanspreekpunt.Naam,
                    Email = x.Aanspreekpunt.Email,
                    Telefoonnummer = x.Aanspreekpunt.Telefoonnummer
                },
                BackupAanspreekpunt = new AanspreekpuntDto
                {
                    Voornaam = x.BackupAanspreekpunt.Voornaam,
                    Naam = x.BackupAanspreekpunt.Naam,
                    Email = x.BackupAanspreekpunt.Email,
                    Telefoonnummer = x.BackupAanspreekpunt.Telefoonnummer
                }
            }).ToListAsync();

      


            var result = new KlantResponse.Detail
            {
                Klanten = items,
                TotaalAantal = 0
            };

            return result;
        }

        public async Task<KlantDto.Detail> GetDetailAsync(int klantId) {
            KlantDto.Detail? klant = await dbContext.Klanten.Select(x => new KlantDto.Detail {
                Id = x.Id,
                Extern = x.Extern,
                Departement = x.Departement,
                Opleiding = x.Opleiding,
                KlantNaam = x.KlantNaam,
                ExternType = x.ExternType,
                Aanspreekpunt = new AanspreekpuntDto {
                    Voornaam = x.Aanspreekpunt.Voornaam,
                    Naam = x.Aanspreekpunt.Naam,
                    Email = x.Aanspreekpunt.Email,
                    Telefoonnummer = x.Aanspreekpunt.Telefoonnummer
                },
                BackupAanspreekpunt = new AanspreekpuntDto {
                    Voornaam = x.BackupAanspreekpunt.Voornaam,
                    Naam = x.BackupAanspreekpunt.Naam,
                    Email = x.BackupAanspreekpunt.Email,
                    Telefoonnummer = x.BackupAanspreekpunt.Telefoonnummer
                }
            }).SingleOrDefaultAsync(x => x.Id == klantId);

            if (klant is null) {
                throw new EntityNotFoundException(nameof(Klant), klantId);
            }
            
            return klant;
        }

        public async Task<KlantResponse.Index> GetAllIndexAsync() {
            var query = dbContext.Klanten.AsQueryable();

            var items = await query.OrderBy(x => x.Id).Select(x => new KlantDto.Index { Id = x.Id, KlantNaam = x.KlantNaam, Departement = x.Departement, Opleiding = x.Opleiding }).ToListAsync();

            var result = new KlantResponse.Index {
                Klanten = items
            };

            return result;
        }
    }
}