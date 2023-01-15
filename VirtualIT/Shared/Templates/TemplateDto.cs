using FluentValidation;

namespace VirtualIT.Shared.Templates;

public class TemplateDto
{
    public class Index {
        public int Id { get; set; }
        public string Naam { get; set; }
    }

    public class Detail : Index {
        public string GebruikteSoftware { get; set; }
    }

    public class Mutate {
        public string Naam { get; set; }
        public string? GebruikteSoftware { get; set; }

        public class Validator : AbstractValidator<TemplateDto.Mutate> {
            public Validator() {
                RuleFor(x => x.Naam).NotEmpty().Length(1, 250);
            }
        }
    }
}
