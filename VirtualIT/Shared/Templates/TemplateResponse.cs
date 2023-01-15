namespace VirtualIT.Shared.Templates {
    public abstract class TemplateResponse {
        public class Index {
            public IEnumerable<TemplateDto.Index>? Templates { get; set; }
        }
    }
}
