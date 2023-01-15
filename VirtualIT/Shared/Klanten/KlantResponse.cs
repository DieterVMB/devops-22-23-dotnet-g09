namespace VirtualIT.Shared.Klanten;

public abstract class KlantResponse 
{
    public class Index 
    {
        public IEnumerable<KlantDto.Index>? Klanten { get; set; }
    }

    public class Detail
    {
        public IEnumerable<KlantDto.Detail>? Klanten { get; set; }
        public int TotaalAantal { get; set; }
    }
}
