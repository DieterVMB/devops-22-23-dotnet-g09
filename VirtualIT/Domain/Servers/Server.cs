using Ardalis.GuardClauses;
using System.Collections.Generic;
using VirtualIT.Domain.Common;
using VirtualIT.Domain.Exceptions;
using VirtualIT.Domain.VirtualMachines;

namespace VirtualIT.Domain.Servers;

public class Server : Entity 
{
    public string Naam { get; set; }
    public int Storage { get; set; } = 1000;
    public int Memory { get; set; }
    public int Vcpu { get; set; }

    private readonly List<VirtualMachine> vms = new();
    public IReadOnlyCollection<VirtualMachine> Vms => vms.AsReadOnly();

    private Server() { }

    public Server(string naam, int storage, int memory, int vcpu) {
        Naam = naam;
        Storage = storage;
        Memory = memory;
        Vcpu = vcpu;
    }

    public void VoegVMToeAanServer(VirtualMachine vm) {
        int usedStorage = vm.Storage;
        int usedMemory = vm.Memory;
        int usedVcpu = vm.Vcpu;

        List<VirtualMachine> filteredVms = vms.Where(v => v.Startdatum <= vm.Startdatum && v.Einddatum >= vm.Startdatum).ToList();

        foreach(var item in filteredVms) {
            usedStorage += item.Storage;
            usedMemory += item.Memory;
            usedVcpu += item.Vcpu;
        }

        if(usedStorage <= Storage && usedMemory <= Memory && usedVcpu <= Vcpu) {
            vms.Add(vm);
        } else {
            throw new ServerDoesNotHaveEnoughResourcesException(Naam, vm.Naam);
        }
    }
    public void VerwijderVMVanServer(VirtualMachine vm) {
        vms.Remove(vm);
    }
}