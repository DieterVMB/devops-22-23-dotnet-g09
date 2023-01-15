using Auth0.ManagementApi.Models;
using Auth0.ManagementApi;
using Microsoft.EntityFrameworkCore;
using VirtualIT.Domain.Beheerders;
using VirtualIT.Domain.Exceptions;
using VirtualIT.Persistence;
using VirtualIT.Shared.Beheerder;

namespace VirtualIT.Services.Beheerders;

public class BeheerderService : IBeheerderService {
    private readonly VirtualITDbContext dbContext;
    private readonly IManagementApiClient managementApiClient;

    public BeheerderService(VirtualITDbContext dbContext, IManagementApiClient managementApiClient) {
        this.dbContext = dbContext;
        this.managementApiClient = managementApiClient;
    }

    //GET action
    public async Task<BeheerderResponse.GetIndex> GetAllIndexAsync() {
        var query = dbContext.Beheerders.AsQueryable();

        var items = await query.OrderBy(x => x.Id)
                                .Select(x => new BeheerderDto.Index {
                                    Id = x.Id,
                                    Voornaam = x.Voornaam,
                                    Naam = x.Naam,
                                    Email = x.Email,
                                    Rol = x.Rol
                                }).ToListAsync();

        var result = new BeheerderResponse.GetIndex {
            Beheerders = items
        };

        return result;
    }

    //POST action
    public async Task<int> CreateAsync(BeheerderDto.Create model) {
        if (await dbContext.Beheerders.AnyAsync(x => (x.Voornaam == model.Voornaam) && (x.Naam == model.Naam))) {
            throw new EntityAlreadyExistsException(nameof(Beheerder), nameof(Beheerder.Naam), model.Naam);
        }

        var auth0Id = MaakBeheerderInAuth0(model.Voornaam, model.Naam, model.Email, model.Wachtwoord, model.Rol, model.IsActief).Result;

        Beheerder beheerder = new Beheerder(model.Voornaam, model.Naam, model.Email,
                                            model.Departement, model.Rol, model.IsActief, auth0Id);

        dbContext.Beheerders.Add(beheerder);

        await dbContext.SaveChangesAsync();

        return beheerder.Id;
    }
    public async Task<BeheerderDto.Detail> GetDetailAsync(int beheerderId) {
        BeheerderDto.Detail? beheerder = await dbContext.Beheerders.Select(x => new BeheerderDto.Detail {
            Id = x.Id,
            Voornaam = x.Voornaam,
            Naam = x.Naam,
            Email = x.Email,
            Departement = x.Departement,
            Rol = x.Rol,
            IsActief = x.IsActief
        }).SingleOrDefaultAsync(x => x.Id == beheerderId);

        if(beheerder is null) {
            throw new EntityNotFoundException(nameof(Beheerder), beheerderId);
        }

        return beheerder;
    }

    public async Task EditAsync(int beheerderId, BeheerderDto.Edit model) {
        Beheerder? beheerder = await dbContext.Beheerders.SingleOrDefaultAsync(x => x.Id == beheerderId);
        if(beheerder is null) {
            throw new EntityNotFoundException(nameof(Beheerder), beheerderId);
        }

        await PasBeheerderAanInAuth0(model.Voornaam, model.Naam, model.Email, model.Wachtwoord, model.Rol, model.IsActief, beheerder.Auth0Id);

        beheerder.Voornaam = model.Voornaam;
        beheerder.Naam = model.Naam;
        beheerder.Email = model.Email;
        beheerder.Departement = model.Departement;
        beheerder.Rol = model.Rol;
        beheerder.IsActief = model.IsActief;

        await dbContext.SaveChangesAsync();
    }

    private async Task PasBeheerderAanInAuth0(string voornaam, string naam, string email, string? wachtwoord, Rol rol, bool isActief, string auth0Id) {
        UserUpdateRequest auth0Request;

        if (!(string.IsNullOrEmpty(wachtwoord))) {
            auth0Request = new UserUpdateRequest {
                Email = email,
                FirstName = voornaam,
                LastName = naam,
                Password = wachtwoord,
                Blocked = !isActief,
                Connection = "Username-Password-Authentication"
            };
        } else {
            auth0Request = new UserUpdateRequest {
                Email = email,
                FirstName = voornaam,
                LastName = naam,
                Blocked = !isActief,
                Connection = "Username-Password-Authentication"
            };
        }

        var allRoles = await managementApiClient.Roles.GetAllAsync(new GetRolesRequest());
        var rolesUser = await managementApiClient.Users.GetRolesAsync(auth0Id);

        try {
            var roleUser = rolesUser.First(x => x.Name == rol.ToString());
        } catch(Exception e) {
            AssignRolesRequest roleRequest;
            if (rol.ToString() == "Gebruiker") {
                var role = allRoles.First(x => x.Name == "Beheerder");
                roleRequest = new AssignRolesRequest {
                    Roles = new string[] { role!.Id }
                };
            } else {
                var role = allRoles.First(x => x.Name == "Gebruiker");
                roleRequest = new AssignRolesRequest {
                    Roles = new string[] { role!.Id }
                };
            }
            await managementApiClient.Users.RemoveRolesAsync(auth0Id, roleRequest);

            var selectedRole = allRoles.First(x => x.Name == rol.ToString());

            var assignRoleRequest = new AssignRolesRequest {
                Roles = new string[] { selectedRole.Id }
            };
            await managementApiClient.Users.AssignRolesAsync(auth0Id, assignRoleRequest);
        }

        await managementApiClient.Users.UpdateAsync(auth0Id, auth0Request);

    }

    private async Task<string> MaakBeheerderInAuth0(string voornaam, string naam, string email, string wachtwoord, Rol rol, bool isActief) {
        var auth0Request = new UserCreateRequest {
            Email = email,
            FirstName = voornaam,
            LastName = naam,
            Password = wachtwoord,
            Blocked = !isActief,
            Connection = "Username-Password-Authentication"
        };

        var createdBeheerder = await managementApiClient.Users.CreateAsync(auth0Request);

        var allRoles = await managementApiClient.Roles.GetAllAsync(new GetRolesRequest());
        var role = allRoles.First(x => x.Name == rol.ToString());

        var assignRoleRequest = new AssignRolesRequest {
            Roles = new string[] { role.Id }
        };
        await managementApiClient.Users.AssignRolesAsync(createdBeheerder?.UserId, assignRoleRequest);

        return createdBeheerder!.UserId;
    }

    public async Task<BeheerderResponse.GetIndex> GetAllBeheerderRoleIndexAsync() {
        var query = dbContext.Beheerders.AsQueryable();

        var items = await query.OrderBy(x => x.Id)
                                .Where(x => x.Rol == Rol.Beheerder)
                                .Select(x => new BeheerderDto.Index {
                                    Id = x.Id,
                                    Voornaam = x.Voornaam,
                                    Naam = x.Naam,
                                }).ToListAsync();

        var result = new BeheerderResponse.GetIndex {
            Beheerders = items
        };

        return result;
    }

    public async Task<BeheerderResponse.Detail> GetAllDetailASync() {
        var items = await dbContext.Beheerders.Select(x => new BeheerderDto.Detail {
            Id = x.Id,
            Voornaam = x.Voornaam,
            Naam = x.Naam,
            Email = x.Email,
            Rol = x.Rol,
            Departement = x.Departement,
            IsActief = x.IsActief
        }).ToListAsync();

        var result = new BeheerderResponse.Detail {
            beheerders = items
        };

        return result;
    }
}

