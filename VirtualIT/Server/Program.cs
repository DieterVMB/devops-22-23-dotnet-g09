using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using VirtualIT.Persistence;
using VirtualIT.Server.Middleware;
using VirtualIT.Services;
using VirtualIT.Shared.Beheerder;
using VirtualIT.Shared.Klanten;
using VirtualIT.Shared.Server;
using VirtualIT.Shared.Templates;
using VirtualIT.Shared.VirtualMachines;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Authority"];
    options.Audience = builder.Configuration["Auth0:ApiIdentifier"];
});

builder.Services.AddAuth0AuthenticationClient(config =>
{
    config.Domain = builder.Configuration["Auth0:Authority"];
    config.ClientId = builder.Configuration["Auth0:ClientId"];
    config.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

builder.Services.AddAuth0ManagementClient().AddManagementAccessToken();


// Add services to the container.

builder.Services.AddVirtualITServices();

builder.Services.AddValidatorsFromAssemblyContaining<KlantDto.Mutate.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<BeheerderDto.Create.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<BeheerderDto.Edit.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<AanspreekpuntDto.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<ServerDto.Mutate.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<TemplateDto.Mutate.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<VirtualMachineDto.Mutate.Validator>();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Since we subclass our dto's we need a more unique id.
    options.CustomSchemaIds(type => type.DeclaringType is null ? $"{type.Name}" : $"{type.DeclaringType?.Name}.{type.Name}");
    options.EnableAnnotations();
}).AddFluentValidationRulesToSwagger();

builder.Services.AddDbContext<VirtualITDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySQLServer"));
});

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using(var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<VirtualITDbContext>();
    FakeSeeder seeder = new(dbContext);
    seeder.Seed();
}

app.Run();
