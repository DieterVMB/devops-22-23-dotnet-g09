namespace VirtualIT.Shared.VirtualMachines;

public abstract class VirtualMachineRequest
{
    public class Index
    {
        public string? Zoekterm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }

    public class Create {
        public VirtualMachineDto.Mutate Vm { get; set; }
        public int ServerId { get; set; }
        public int KlantId { get; set; }
        public int BeheerderId { get; set; }
        public int TemplateId { get; set; }
    }

    public class Edit : Create {
        public int OldServerID { get; set; }
    }
}

