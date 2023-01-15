using System.ComponentModel;

namespace VirtualIT.Domain.VirtualMachines;

public enum Status
{
    [Description("Aangevraagd")]
    Aangevraagd,

    [Description("In behandeling")]
    InBehandeling,

    [Description("Afgehandeld")]
    Afgehandeld
}
