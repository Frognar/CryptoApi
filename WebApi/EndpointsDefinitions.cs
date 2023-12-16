using Crypto;

namespace WebApi;

public static class EndpointsDefinitions
{
    public static void RegisterCryptoService(this IServiceCollection services)
    {
        services.AddSingleton<ICrypto, CryptoRSA>();
    }

    public static void RegisterCryptoEndpoints(this WebApplication app)
    {
        app.MapGet("api/encrypt", Encrypt);
        app.MapGet("api/decrypt", Decrypt);
    }

    private static async Task<IResult> Encrypt(string text, ICrypto crypto)
    {
        try
        {
            return Results.Ok(await crypto.Encrypt(text));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> Decrypt(string text, ICrypto crypto)
    {
        try
        {
            return Results.Ok(await crypto.Decrypt(text));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
