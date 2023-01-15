using Microsoft.Extensions.DependencyInjection;
using VirtualIT.Services.Beheerders;
using VirtualIT.Services.Klanten;
using VirtualIT.Services.Servers;
using VirtualIT.Services.Templates;
using VirtualIT.Services.VirtualMachines;
using VirtualIT.Shared.Beheerder;
using VirtualIT.Shared.Klanten;
using VirtualIT.Shared.Server;
using VirtualIT.Shared.Templates;
using VirtualIT.Shared.VirtualMachines;

namespace VirtualIT.Services {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddVirtualITServices(this IServiceCollection services) {
            services.AddScoped<IKlantService, KlantService>();
            services.AddScoped<IVirtualMachineService, VirtualMachineService>();
            services.AddScoped<IBeheerderService, BeheerderService>();
            services.AddScoped<IServerService, ServerService>();
            services.AddScoped<ITemplateService, TemplateService>();

            return services;
        }
    }
}
