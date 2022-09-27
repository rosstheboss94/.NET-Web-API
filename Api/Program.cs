using Api.Data;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddResponseCaching();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();

builder.AddAppExtensions();
builder.AddIdentityExtensions();
builder.Services.AddAuthorizationPolicyExtensions();
builder.Services.AddSwaggerConfigExtensions();
builder.Services.AddApiVersioningExtensions();
builder.Services.AddServiceExtensions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

Seed.SeedData(app);

app.Run();

