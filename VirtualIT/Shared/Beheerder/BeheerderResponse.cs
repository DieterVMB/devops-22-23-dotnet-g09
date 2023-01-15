namespace VirtualIT.Shared.Beheerder;

public abstract class BeheerderResponse
{
    public class GetIndex
    {
        public List<BeheerderDto.Index> Beheerders { get; set; } = new();
    }

    public class Detail {
        public IEnumerable<BeheerderDto.Detail>? beheerders { get; set; }
    }
}
