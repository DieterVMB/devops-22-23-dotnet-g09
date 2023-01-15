using System.ComponentModel;

namespace VirtualIT.Domain.VirtualMachines;

public enum Beschikbaarheid
{
    [Description("Business uren")]
    BusinessUren,

    [Description("Hele dag")]
    HeleDag
}
