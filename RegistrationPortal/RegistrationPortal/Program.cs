using Microsoft.Extensions.Logging.AzureAppServices;
using RegistrationPortal.Components;
using RegistrationPortal.Extensions;
using RegistrationPortal.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

// Logging in Azure
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "diagnostics-";
    options.FileSizeLimit = 50 * 1024; // 50 MB
    options.RetainedFileCountLimit = 5;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseSecurityHeaders(
    SecurityHeadersDefinitions.GetHeaderPolicyCollection(app.Environment.IsDevelopment(),
        app.Configuration["AzureAd:Instance"]));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

AuthenticationExtensions.SetupEndpoints(app);

app.MapControllers();

app.MapFormApi();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(RegistrationPortal.Client._Imports).Assembly);

app.Run();
