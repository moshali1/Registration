namespace RegistrationPortal;

public static class SecurityHeadersDefinitions
{
    public static HeaderPolicyCollection GetHeaderPolicyCollection(bool isDev, string idpHost)
    {
        ArgumentNullException.ThrowIfNull(idpHost);

        var policy = new HeaderPolicyCollection()
            .AddFrameOptionsDeny()
            .AddContentTypeOptionsNoSniff()
            .AddReferrerPolicyStrictOriginWhenCrossOrigin()
            .AddCrossOriginOpenerPolicy(builder => builder.SameOrigin())
            .AddCrossOriginResourcePolicy(builder => builder.CrossOrigin()) // Originally SameOrigin - Relaxing due to error
            /*.AddCrossOriginEmbedderPolicy(builder => builder.RequireCorp()) */// Orginially RequireCorp - Relaxing due to error
            .AddContentSecurityPolicy(builder =>
            {
                builder.AddObjectSrc().None();
                builder.AddBlockAllMixedContent();
                builder.AddImgSrc().Self().From("data:").From("*"); // UNSAVE FOR SECURITY REASONS
                builder.AddFormAction().Self().From(idpHost);
                builder.AddFontSrc().Self().From("https://fonts.gstatic.com").From("data:");
                builder.AddStyleSrc().Self().From("https://fonts.googleapis.com").UnsafeInline(); // UNSAFE INLINE ONLY DURING DEVELOPMENT
                builder.AddBaseUri().Self();
                builder.AddFrameAncestors().None();

                // due to Blazor Web, nonces cannot be used :(
                // weak script CSP....
                builder.AddScriptSrc()
                    .Self() // self required
                    .UnsafeEval() // due to Blazor WASM
                    .UnsafeInline(); // only a fallback for older browsers when the nonce is used 

            })
            .RemoveServerHeader()
            .AddPermissionsPolicy(builder =>
            {
                builder.AddAccelerometer().None();
                builder.AddAutoplay().None();
                builder.AddCamera().None();
                builder.AddEncryptedMedia().None();
                builder.AddFullscreen().All();
                builder.AddGeolocation().None();
                builder.AddGyroscope().None();
                builder.AddMagnetometer().None();
                builder.AddMicrophone().None();
                builder.AddMidi().None();
                builder.AddPayment().None();
                builder.AddPictureInPicture().None();
                builder.AddSyncXHR().None();
                builder.AddUsb().None();
            });

        if (!isDev)
        {
            // maxage = one year in seconds
            policy.AddStrictTransportSecurityMaxAgeIncludeSubDomains();
        }

        policy.ApplyDocumentHeadersToAllResponses();

        return policy;
    }
}
