using FluentValidation;
using VirtualIT.Shared.VirtualMachines;

namespace VirtualIT.Shared.Server;

public class ServerDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
    }

    public class Detail : Index 
    {
        public int Storage { get; set; }
        public int Memory { get; set; }
        public int Vcpu { get; set; }
        public IEnumerable<VirtualMachineDto.Detail> Vms { get; set; }
    }

    public class VmEdit {
        public int Id { get; set; }
        public IEnumerable<int> VmsIdList { get; set; }
    }

    public class Mutate
    {
        public string? Naam { get; set; }
        public int Storage { get; set; }
        public int Memory { get; set; }
        public int Vcpu { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Naam).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Storage).NotEmpty().GreaterThan(0);
                RuleFor(x => x.Memory).NotEmpty().GreaterThan(0);
                RuleFor(x => x.Vcpu).NotEmpty().GreaterThan(0);
            }
        }
    }
}
