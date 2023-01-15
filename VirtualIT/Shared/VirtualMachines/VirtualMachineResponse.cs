using VirtualIT.Shared.Server;

namespace VirtualIT.Shared.VirtualMachines;

public abstract class VirtualMachineResponse
{
    public class Index
    {
        public IEnumerable<VirtualMachineDto.Index>? VirtualMachines { get; set; }
        public int TotaalAantal { get; set; }
    }

    public class Detail
    {
        public IEnumerable<VirtualMachineDto.Detail>? VirtualMachineDetails { get; set; }
        public int TotaalAantal { get; set; }
    }

    public class SingleDetail {
        public VirtualMachineDto.Detail Vm { get; set; }
        public int HostingServerId { get; set; }
    }
}

