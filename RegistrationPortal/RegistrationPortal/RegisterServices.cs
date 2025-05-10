using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RegistrationPortal.Identity;
using RegistrationPortal.Identity.Client.Services;
using RegistrationPortal.Shared;
using Syncfusion.Blazor;

namespace RegistrationPortal;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        var scopes = builder.Configuration.GetValue<string>("DownstreamApi:Scopes");
        string[] initialScopes = scopes!.Split(' ');

        // Sign-in users with the Microsoft identity platform
        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration)
            .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            .AddInMemoryTokenCaches();
        //.AddInMemoryTokenCaches(); what is this?

        // Without, the client component using the API as an error in server mode
        builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();

        builder.Services.AddScoped<HostingEnvironmentService>();
        builder.Services.AddSingleton<BaseUrlProvider>();
        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddScoped(sp => sp
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("API"))
            .AddHttpClient("API", (provider, client) =>
            {
                // Get base address
                var uri = provider.GetRequiredService<BaseUrlProvider>().BaseUrl;
                client.BaseAddress = new Uri(uri);
            });

        builder.Services.AddAuthorization(options =>
        {
            // The FallbackPolicy is set to 'null' to disable global authorization.
            // This means by default, pages and APIs are accessible without authorization
            // unless explicitly protected using [Authorize] or similar attributes.

            // Set FallbackPolicy to options.DefaultPolicy for default authorization, requiring authentication for all requests.
            // Uncomment below to apply:

            options.FallbackPolicy = options.DefaultPolicy;

            //options.FallbackPolicy = null;
        });


        builder.Services.AddRazorPages().AddMvcOptions(options =>
        {
            //var policy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();
            //options.Filters.Add(new AuthorizeFilter(policy));
        }).AddMicrosoftIdentityUI();

        builder.Services.AddSwaggerGen();

        // Add SignalR
        builder.Services.AddSignalR();

        builder.Services.AddSingleton<IDbConnection, DbConnection>();
        builder.Services.AddSingleton<IArchivedDbConnection, ArchivedDbConnection>();

        builder.Services.AddAutoMapper(typeof(FormProfile), typeof(UserProfile));

        builder.Services.AddScoped<IFormData, FormData>();
        builder.Services.AddScoped<IUserData, UserData>();
        builder.Services.AddScoped<ISettingsData, SettingsData>();
        builder.Services.AddScoped<IDatabaseArchivedFormData, DatabaseArchivedFormData>();
        builder.Services.AddScoped<IEmailTemplateData, EmailTemplateData>();

        builder.Services.AddScoped<IFormService, FormService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IArchivedFormData, ArchivedFormData>();


        builder.Services.AddScoped<IFaceDetectionService, FaceDetectionService>();
        builder.Services.AddScoped<ITextDetectionService, TextDetectionService>();
        builder.Services.AddScoped<IEmailTemplateProcessor, EmailTemplateProcessor>();

        builder.Services.AddScoped<IScheduleSlotData, ScheduleSlotData>();
        builder.Services.AddScoped<IScheduleSettingsData, ScheduleSettingsData>();
        builder.Services.AddScoped<ISchedulingService, SchedulingService>();

        builder.Services.AddScoped<FormSelectionService>(); // each client
        builder.Services.AddScoped<DefaultListService>(); // each client

        builder.Services.AddSingleton<MockData>();

        builder.Services.AddSyncfusionBlazor();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
        "Ngo9BigBOggjHTQxAR8/V1NAaF1cXmhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFjW31acXxVQWJcV0R+Xg==");

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
        "Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxcdHRXQmleUU1+WkI=");


        // Increases the maximum allowed multipart body length for file uploads to 500 MB,
        // enabling large file uploads in the application.
        builder.Services.Configure<FormOptions>(x => x.MultipartBodyLengthLimit = 524288000);

        builder.Services.AddScoped<FetchSasUri>();

        builder.Services.AddSingleton<CertificateImageService>();

        builder.Services.AddSingleton<IEmailService, EmailService>();
        builder.Services.AddSingleton<IEmailService, EmailService>();

        builder.Services.AddMemoryCache();

        builder.Services.AddSignalR(hubOptions =>
        {
            hubOptions.MaximumReceiveMessageSize = 64 * 1024; // 64KB
                                                              // You can set it higher if needed, like 1MB:
                                                              // hubOptions.MaximumReceiveMessageSize = 1024 * 1024; // 1MB

            hubOptions.EnableDetailedErrors = true; // Keep detailed errors enabled
        });
    }
}
