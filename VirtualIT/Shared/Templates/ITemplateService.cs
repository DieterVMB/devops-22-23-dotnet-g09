namespace VirtualIT.Shared.Templates {
    public interface ITemplateService {
        Task<TemplateResponse.Index> GetAllIndexAsync();
    }
}
