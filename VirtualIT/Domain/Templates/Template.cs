using VirtualIT.Domain.Common;

namespace VirtualIT.Domain.Templates;

public class Template : Entity
{
    public string Naam { get; set; }
    public string? GebruikteSoftware { get; set; }

    private Template() { }

    public Template(string naam, string? gebruikteSoftware)
    {
        Naam = naam;
        GebruikteSoftware = gebruikteSoftware;
    }
}

