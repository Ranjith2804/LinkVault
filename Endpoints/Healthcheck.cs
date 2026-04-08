using System.Runtime.CompilerServices;

namespace LinkVault.Endpoints
{
    public static  class Healthcheck
    {
        public static void MapHealthcheck(this WebApplication app) {
            app.MapGet("/health", () => Results.Ok(new
            {
                status="Healthy",
                Timestamp=DateTime.Now,
                version="v.1.0"
            }));
       }
    }
}
