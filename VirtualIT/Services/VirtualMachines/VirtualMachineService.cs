using Microsoft.EntityFrameworkCore;
using VirtualIT.Persistence;
using VirtualIT.Shared.VirtualMachines;
using VirtualIT.Domain.Exceptions;
using VirtualIT.Domain.VirtualMachines;
using VirtualIT.Shared.Beheerder;
using VirtualIT.Domain.Beheerders;
using VirtualIT.Shared.Templates;
using VirtualIT.Domain.Templates;
using VirtualIT.Domain.Klanten;
using VirtualIT.Domain.Servers;
using VirtualIT.Shared.Server;
using VirtualIT.Shared.Klanten;

namespace VirtualIT.Services.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly VirtualITDbContext dbContext;

    public VirtualMachineService(VirtualITDbContext dbContext)
    {
        this.dbContext = dbContext;
    }



    public async Task<VirtualMachineResponse.Index> GetAll()
    {
        var query = dbContext.VirtualMachines.AsQueryable();
        
        int totaalAantal = await query.CountAsync();

        var items = await query.OrderBy(x => x.Id)
                                .Select(x => new VirtualMachineDto.Index {
                                    Id = x.Id,
                                    Naam = x.Naam,
                                    Hostname = x.Hostname,
                                    FQDN = x.FQDN,
                                    Vcpu = x.Vcpu,
                                    Memory = x.Memory,
                                    Storage = x.Storage
                                }).ToListAsync();

        var result = new VirtualMachineResponse.Index {
            VirtualMachines = items,
            TotaalAantal = totaalAantal
        };

        return result;
    }
    public async Task<VirtualMachineResponse.Detail> GetAllDetails()
    {

        var query = dbContext.VirtualMachines.AsQueryable();

        int totaalAantal = await query.CountAsync();

        var items = await query.OrderBy(x => x.Id)
                                .Select(x => new VirtualMachineDto.Detail
                                {
                                    Id = x.Id,       
                                    NaamGebruiker = x.NaamGebruiker,
                                    Einddatum = x.Einddatum,
                                    Startdatum = x.Startdatum,
                                    Backup = x.Backup,
                                    BackupFrequentie = x.BackupFrequentie,
                                    Beschikbaarheid = x.Beschikbaarheid,   
                                    DatumAanvraag = x.DatumAanvraag,
                                    EmailAanvrager = x.EmailAanvrager,
                                    ExterneToegangspoorten = x.ExterneToegangspoorten,  
                                    HighAvailable = x.HighAvailable,
                                    TelefoonnummerAanvrager = x.TelefoonnummerAanvrager,
                                    Mode = x.Mode,
                                    Reden = x.Reden, 
                                    Status = x.Status,
                                    Naam = x.Naam,
                                    Hostname = x.Hostname,
                                    FQDN = x.FQDN,
                                    Vcpu = x.Vcpu,
                                    Memory = x.Memory,
                                    Storage = x.Storage
                                }).ToListAsync();

        var result = new VirtualMachineResponse.Detail
        {
            VirtualMachineDetails = items,
            TotaalAantal = totaalAantal
        };

        return result;
    }

    public async Task<VirtualMachineResponse.Detail> GetAllDetailsVerlopenVms() {

        var query = dbContext.VirtualMachines.AsQueryable();

        int totaalAantal = await query.CountAsync();

        var items = await query.Where(x => x.Einddatum < DateTime.Today).OrderBy(x => x.Id)
                                .Select(x => new VirtualMachineDto.Detail {
                                    Id = x.Id,
                                    NaamGebruiker = x.NaamGebruiker,
                                    Einddatum = x.Einddatum,
                                    Startdatum = x.Startdatum,
                                    Backup = x.Backup,
                                    BackupFrequentie = x.BackupFrequentie,
                                    Beschikbaarheid = x.Beschikbaarheid,
                                    DatumAanvraag = x.DatumAanvraag,
                                    EmailAanvrager = x.EmailAanvrager,
                                    ExterneToegangspoorten = x.ExterneToegangspoorten,
                                    HighAvailable = x.HighAvailable,
                                    TelefoonnummerAanvrager = x.TelefoonnummerAanvrager,
                                    Mode = x.Mode,
                                    Reden = x.Reden,
                                    Status = x.Status,
                                    Naam = x.Naam,
                                    Hostname = x.Hostname,
                                    FQDN = x.FQDN,
                                    Vcpu = x.Vcpu,
                                    Memory = x.Memory,
                                    Storage = x.Storage
                                }).ToListAsync();

        var result = new VirtualMachineResponse.Detail {
            VirtualMachineDetails = items,
            TotaalAantal = totaalAantal
        };

        return result;
    }

    //GET BY ID action
    public async Task<VirtualMachineResponse.SingleDetail> GetDetailAsync(int virtualMachineId)
    {
        VirtualMachineDto.Detail? virtualMachine = await dbContext.VirtualMachines.Select(x => new VirtualMachineDto.Detail {
            Id = x.Id,
            Naam = x.Naam,
            Hostname = x.Hostname,
            FQDN = x.FQDN,
            Vcpu = x.Vcpu,
            Memory = x.Memory,
            Storage = x.Storage,

            Mode = x.Mode,
            Template = new TemplateDto.Detail {
                Naam = x.Template.Naam,
                GebruikteSoftware = x.Template.GebruikteSoftware
            },
            Backup = x.Backup,
            BackupFrequentie = x.BackupFrequentie,
            Beschikbaarheid = x.Beschikbaarheid,
            HighAvailable = x.HighAvailable,
            Klant = new KlantDto.Detail {
                Id = x.Klant.Id,
                KlantNaam = x.Klant.KlantNaam,
                Departement = x.Klant.Departement,
                Opleiding = x.Klant.Opleiding,
                Extern = x.Klant.Extern,
                ExternType = x.Klant.ExternType,
                Aanspreekpunt = new AanspreekpuntDto {
                    Voornaam = x.Klant.Aanspreekpunt.Voornaam,
                    Naam = x.Klant.Aanspreekpunt.Naam,
                    Email = x.Klant.Aanspreekpunt.Email,
                    Telefoonnummer = x.Klant.Aanspreekpunt.Telefoonnummer
                },
                BackupAanspreekpunt = new AanspreekpuntDto {
                    Voornaam = x.Klant.BackupAanspreekpunt.Voornaam,
                    Naam = x.Klant.BackupAanspreekpunt.Naam,
                    Email = x.Klant.BackupAanspreekpunt.Email,
                    Telefoonnummer = x.Klant.BackupAanspreekpunt.Telefoonnummer
                }
            },
            EmailAanvrager = x.EmailAanvrager,
            TelefoonnummerAanvrager = x.TelefoonnummerAanvrager,
            DatumAanvraag = x.DatumAanvraag,
            NaamGebruiker = x.NaamGebruiker,
            RelatieGebruiker = x.RelatieGebruiker,
            Reden = x.Reden,
            Startdatum = x.Startdatum,
            Einddatum = x.Einddatum,
            
            Status = x.Status,
            ToegewezenAan = new BeheerderDto.Detail {
                Id = x.ToegewezenAan.Id,
                Voornaam = x.ToegewezenAan.Voornaam,
                Naam = x.ToegewezenAan.Naam,
                Email = x.ToegewezenAan.Email,
                Departement = x.ToegewezenAan.Departement,
                Rol = x.ToegewezenAan.Rol,
                IsActief = x.ToegewezenAan.IsActief
            },
            ExterneToegangspoorten = x.ExterneToegangspoorten
        }).SingleOrDefaultAsync(x => x.Id == virtualMachineId);

        if(virtualMachine is null) {
            throw new EntityNotFoundException(nameof(VirtualMachine), virtualMachineId);
        }

        var servers = dbContext.Servers.Select(x => new ServerDto.VmEdit {
            Id = x.Id,
            VmsIdList = x.Vms.Select(x => x.Id)
        }).ToList();

        var server = servers.FirstOrDefault(x => x.VmsIdList.Contains(virtualMachineId));

        return new VirtualMachineResponse.SingleDetail {
            Vm = virtualMachine,
            HostingServerId = server!.Id
        };
    }

    //POST action
    public async Task<int> CreateAsync(VirtualMachineRequest.Create request)
    {
        if(await dbContext.VirtualMachines.AnyAsync(x => x.Naam == request.Vm.Naam)) {
            throw new EntityAlreadyExistsException(nameof(VirtualMachine), nameof(VirtualMachine.Naam), request.Vm.Naam);
        }

        Server? server = await dbContext.Servers.SingleOrDefaultAsync(x => x.Id == request.ServerId);
        Template? template = await dbContext.Templates.SingleOrDefaultAsync(x => x.Id == request.TemplateId);
        Beheerder? beheerder = await dbContext.Beheerders.SingleOrDefaultAsync(x => x.Id == request.BeheerderId);
        Klant? klant = await dbContext.Klanten.SingleOrDefaultAsync(x => x.Id == request.KlantId);
        VirtualMachine vm = new VirtualMachine(request.Vm.Naam, request.Vm.Hostname, request.Vm.FQDN, request.Vm.Vcpu, request.Vm.Memory, request.Vm.Storage, request.Vm.Mode,
            template, request.Vm.Backup, request.Vm.BackupFrequentie, request.Vm.Beschikbaarheid, request.Vm.HighAvailable, klant!, request.Vm.EmailAanvrager, request.Vm.TelefoonnummerAanvrager,
            request.Vm.DatumAanvraag, request.Vm.NaamGebruiker, request.Vm.RelatieGebruiker, request.Vm.Reden, request.Vm.Startdatum, request.Vm.Einddatum, request.Vm.Status,
            beheerder!, request.Vm.ExterneToegangspoorten);
        server!.VoegVMToeAanServer(vm);

        dbContext.VirtualMachines.Add(vm);

        await dbContext.SaveChangesAsync();
        
        return vm.Id;
    }

    //PUT action
    public async Task EditAsync(int vmId, VirtualMachineRequest.Edit request)
    {
        VirtualMachine? vm = await dbContext.VirtualMachines.SingleOrDefaultAsync(x => x.Id == vmId);

        if(vm is null) {
            throw new EntityNotFoundException(nameof(VirtualMachine), vmId);
        }

        Server? server = await dbContext.Servers.SingleOrDefaultAsync(x => x.Id == request.ServerId);
        if (request.ServerId == request.OldServerID) {
            Server? OldServer = await dbContext.Servers.SingleOrDefaultAsync(x => x.Id == request.ServerId);
            OldServer.VerwijderVMVanServer(vm);
            server.VoegVMToeAanServer(vm);
        }
        Template? template = await dbContext.Templates.SingleOrDefaultAsync(x => x.Id == request.TemplateId);
        Beheerder? beheerder = await dbContext.Beheerders.SingleOrDefaultAsync(x => x.Id == request.BeheerderId);
        Klant? klant = await dbContext.Klanten.SingleOrDefaultAsync(x => x.Id == request.KlantId);

        vm.Naam = request.Vm.Naam;
        vm.Hostname = request.Vm.Hostname;
        vm.FQDN = request.Vm.FQDN;
        vm.Vcpu = request.Vm.Vcpu;
        vm.Memory = request.Vm.Memory;
        vm.Storage = request.Vm.Storage;
        vm.Mode = request.Vm.Mode;
        vm.Template = template;
        vm.Backup = request.Vm.Backup;
        vm.BackupFrequentie = request.Vm.BackupFrequentie;
        vm.Beschikbaarheid = request.Vm.Beschikbaarheid;
        vm.HighAvailable = request.Vm.HighAvailable;
        vm.Klant = klant!;
        vm.EmailAanvrager = request.Vm.EmailAanvrager;
        vm.TelefoonnummerAanvrager = request.Vm.TelefoonnummerAanvrager;
        vm.DatumAanvraag = request.Vm.DatumAanvraag;
        vm.NaamGebruiker = request.Vm.NaamGebruiker;
        vm.RelatieGebruiker = request.Vm.RelatieGebruiker;
        vm.Reden = request.Vm.Reden;
        vm.Startdatum = request.Vm.Startdatum;
        vm.Einddatum = request.Vm.Einddatum;
        vm.Status = request.Vm.Status;
        vm.ToegewezenAan = beheerder!;
        vm.ExterneToegangspoorten = request.Vm.ExterneToegangspoorten;

        await dbContext.SaveChangesAsync();
    }
}

