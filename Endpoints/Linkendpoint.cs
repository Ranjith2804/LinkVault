using LinkVault.Services;
using System.Reflection.Metadata.Ecma335;
namespace LinkVault.Endpoints
{
    public static class Linkendpoint
    {
        public static void MapLinkendpoints(this WebApplication app)
        {
            app.MapPost("/api/links", createlink);
            app.MapGet("/{code}",Redirect);
            app.MapGet("/api/links", getall);

        }
        public record createLinkreq(string url);
        private static IResult createlink(createLinkreq req,Linkservice service)
        {
            if (string.IsNullOrWhiteSpace(req.url))
            {
                return Results.BadRequest("Url is required");
            }
            if(!Uri.TryCreate(req.url,UriKind.Absolute,out _))
            {
                return Results.BadRequest("Url must be a valid one");
            }
            var entry = service.create(req.url);
            return Results.Created($"/{entry.code}", new
            {
                entry.code,
                entry.orginalurl,
                ShortUrl=$"http://localhost:5000/{entry.code}",
                entry.CreatedAt
            });

        } 
        private static IResult Redirect(string code, Linkservice service)
        {
            var entry = service.getbycode(code);
            if(entry == null)
            {
                return Results.NotFound($"No link found for code {code}");
            }
            return Results.Redirect(entry.orginalurl, permanent: false);
        }

        private static IResult getall(Linkservice service)
        {
            return Results.Ok(service.getall());
        }
        

    }
}
