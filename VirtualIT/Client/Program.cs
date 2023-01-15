using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VirtualIT.Client;
using VirtualIT.Shared.Klanten;
using MudBlazor.Services;
using VirtualIT.Client.Pages.Klanten;
using VirtualIT.Shared.VirtualMachines;
using VirtualIT.Client.Pages.VirtualMachines;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using VirtualIT.Client.Shared;
using VirtualIT.Shared.Beheerder;
using VirtualIT.Client.Pages.Beheerders;
using VirtualIT.Shared.Server;
using VirtualIT.Client.Pages.Servers;
using VirtualIT.Shared.Templates;
using VirtualIT.Client.Pages.Templates;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("VirtualITCompanyAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("VirtualITCompanyAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);

}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

//builder.Services.AddAuthorizationCore; (geeft error)
//builder.Services.AddSingleton<AuthenticationProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthenticationProvider>());

builder.Services.AddScoped<IKlantService, KlantService>();
builder.Services.AddScoped<IVirtualMachineService, VirtualMachineService>();
builder.Services.AddScoped<IBeheerderService, BeheerderService>();
builder.Services.AddScoped<IServerService, ServerService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();

await builder.Build().RunAsync();
