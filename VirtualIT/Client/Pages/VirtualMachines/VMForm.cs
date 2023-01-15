using VirtualIT.Shared.VirtualMachines;
using Microsoft.AspNetCore.Components;
using VirtualIT.Shared.Beheerder;
using VirtualIT.Shared.Klanten;
using VirtualIT.Shared.Server;
using VirtualIT.Shared.Templates;
using VirtualIT.Domain.Exceptions;

namespace VirtualIT.Client.Pages.VirtualMachines;

public partial class VMForm {
    private VirtualMachineDto.Mutate virtualMachine;
    private List<TemplateDto.Index> templates = new();
    private List<BeheerderDto.Index> beheerders = new();
    private List<KlantDto.Index> klanten = new();
    private List<KlantDto.Index> interneKlanten = new();
    private List<KlantDto.Index> externeKlanten = new();
    private List<ServerDto.Index> servers = new();
    private int klantId { get; set; }
    private int beheerderId { get; set; }
    private int serverId { get; set; }
    private int templateId { get; set; }
    private int oudServerId { get; set; } = 0;
    private bool vmBestaatAl { get; set; } = false;
    private bool serverNietGenoegRuimte { get; set; } = false;
    private bool geenServerGeselecteerd { get; set; } = false;
    private bool geenKlantGeselecteerd { get; set; } = false;
    private bool geenBeheerderGeselecteerd { get; set; } = false;
    [Parameter] public int Id { get; set; }
    [Inject] public IVirtualMachineService VirtualMachineService { get; set; }
    [Inject] public IBeheerderService BeheerderService { get; set; }
    [Inject] public IKlantService KlantenService { get; set; }
    [Inject] public IServerService ServerService { get; set; }
    [Inject] public ITemplateService TemplateService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync() {
        var beheerderResponse = await BeheerderService.GetAllBeheerderRoleIndexAsync();
        var klantenResponse = await KlantenService.GetAllIndexAsync();
        var serverResponse = await ServerService.GetAllIndexAsync();
        var templateResponse = await TemplateService.GetAllIndexAsync();

        beheerders = beheerderResponse.Beheerders;
        klanten = klantenResponse.Klanten!.ToList();
        servers = serverResponse.Servers!.ToList();
        templates = templateResponse.Templates!.ToList();


        interneKlanten = klanten.Select(x => new KlantDto.Index {
            Id = x.Id,
            Departement = x.Departement,
            Opleiding = x.Opleiding
        }).Where(x => x.KlantNaam == null && x.Departement != null).ToList();

        externeKlanten = klanten.Select(x => new KlantDto.Index {
            Id = x.Id,
            KlantNaam = x.KlantNaam,
        }).Where(x => x.KlantNaam != null && x.Departement == null).ToList();
        if (Id == 0) {
            virtualMachine = new();
            virtualMachine.DatumAanvraag = DateTime.Today;
            virtualMachine.Startdatum = DateTime.Today;
            virtualMachine.Einddatum = DateTime.Today;
        } else {
            var response = await VirtualMachineService.GetDetailAsync(Id);
            virtualMachine = new VirtualMachineDto.Mutate {
                Naam = response.Vm.Naam,
                Hostname = response.Vm.Hostname,
                FQDN = response.Vm.FQDN,
                Vcpu = response.Vm.Vcpu,
                Memory = response.Vm.Memory,
                Storage = response.Vm.Storage,
                Mode = response.Vm.Mode,
                Backup = response.Vm.Backup,
                BackupFrequentie = response.Vm.BackupFrequentie,
                Beschikbaarheid = response.Vm.Beschikbaarheid,
                HighAvailable = response.Vm.HighAvailable,
                EmailAanvrager = response.Vm.EmailAanvrager,
                TelefoonnummerAanvrager = response.Vm.TelefoonnummerAanvrager,
                DatumAanvraag = response.Vm.DatumAanvraag,
                NaamGebruiker = response.Vm.NaamGebruiker,
                RelatieGebruiker = response.Vm.RelatieGebruiker,
                Reden = response.Vm.Reden,
                Startdatum = response.Vm.Startdatum,
                Einddatum = response.Vm.Einddatum,
                Status = response.Vm.Status,
                ExterneToegangspoorten = response.Vm.ExterneToegangspoorten
            };
            klantId = response.Vm.Klant.Id;
            beheerderId = response.Vm.ToegewezenAan.Id;
            if(response.Vm.Template.Id == 0) {
                templateId = 0;
            } else {
                templateId = response.Vm.Template.Id;
            }
            serverId = response.HostingServerId;
            oudServerId = response.HostingServerId;
        }
    }

    private async Task HandleForm() {
        serverNietGenoegRuimte = false;
        if(Id == 0) {
            vmBestaatAl = false;
            geenServerGeselecteerd = false;
            geenKlantGeselecteerd = false;
            geenBeheerderGeselecteerd = false;
            VirtualMachineRequest.Create request = new VirtualMachineRequest.Create {
                Vm = virtualMachine,
                ServerId = serverId,
                KlantId = klantId,
                BeheerderId = beheerderId,
                TemplateId = templateId
            };

            try {
                if (serverId == 0) {
                    geenServerGeselecteerd = true;
                    throw new ArgumentException("");
                }  
                if (klantId == 0) {
                    geenKlantGeselecteerd = true;
                    throw new ArgumentException("");
                }
                if (beheerderId == 0) {
                    geenBeheerderGeselecteerd = true;
                    throw new ArgumentException("");
                }

                var response = await VirtualMachineService.CreateAsync(request);
                NavigationManager.NavigateTo($"vm/{response}");
            } catch (EntityAlreadyExistsException) {
                vmBestaatAl = true;
            } catch (ArgumentException) {
                //
            } catch (ServerDoesNotHaveEnoughResourcesException) {
                serverNietGenoegRuimte = true;
            }
        } else {
            try {
                VirtualMachineRequest.Edit request = new VirtualMachineRequest.Edit {
                    Vm = virtualMachine,
                    ServerId = serverId,
                    KlantId = klantId,
                    BeheerderId = beheerderId,
                    TemplateId = templateId,
                    OldServerID = oudServerId
                };
                await VirtualMachineService.EditAsync(Id, request);

                NavigationManager.NavigateTo($"vm/{Id}");
            } catch (ServerDoesNotHaveEnoughResourcesException) {
                serverNietGenoegRuimte = true;
            }
        }
    }
}
