using Crypto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi;

public static class EndpointsDefinitions
{
    public static void RegisterCryptoService(this IServiceCollection services)
    {
        services.AddSingleton<ICrypto, CryptoRSA>();
    }

    public static void RegisterCryptoEndpoints(this WebApplication app)
    {
        app.MapPost("api/encrypt", Encrypt);
        app.MapPost("api/decrypt", Decrypt);
    }

    private static async Task<IResult> Encrypt([FromBody] string text, ICrypto crypto)
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

    private static async Task<IResult> Decrypt([FromBody] string text, ICrypto crypto)
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
