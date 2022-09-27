using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class AppExtensions
{
    public static void AddAppExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options => 
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 
    }
}
