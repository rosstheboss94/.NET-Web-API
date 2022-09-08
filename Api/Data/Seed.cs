using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public static class Seed 
{
    public static async void SeedData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManger = services.GetRequiredService<UserManager<AppUser>>();
            var tokenService = services.GetRequiredService<ITokenService>();
            string userId = "";
            int journalId = 0;

            await context.Database.EnsureCreatedAsync();

            if(!userManger.Users.Any())
            {
                var appUserDto = new AppUserDto{ UserName = "user", Email = "user@user.com"};
                var token = tokenService.CreateToken(appUserDto);

                var user = new AppUser
                {
                    UserName = appUserDto.UserName,
                    Email = appUserDto.Email
                };
                await userManger.CreateAsync(user, "Pa$$w0rd");
                userId = await userManger.GetUserIdAsync(user);
            }

            if(!context.Journals.Any())
            {
                context.Journals.AddRange(new List<Journal>()
                {
                  new Journal
                  {
                    Name = "Journal 1",
                    Description = "Journal 1 Description",
                    AppUserId = userId,
                  }                     
                });

                await context.SaveChangesAsync();

                var journal = await context.Journals.FirstOrDefaultAsync(j => j.Name == "Journal 1");
                journalId = journal.Id;
            }

            if(!context.Trades.Any())
            {
                context.Trades.AddRange(new List<Trade>()
                {
                    new Trade() 
                    {
                        JournalId = journalId,
                        Type = "Forex",
                        Result = "WIN",
                        Ticker = "EUR/USD",
                        Entry = 1.02590,
                        TakeProfit = 1.05000,
                        StopLoss = 1.01450,
                        RiskReward = "1:3",
                        Notes = "Test Notes"
                    },
                    new Trade() 
                    {
                        JournalId = journalId,
                        Type = "Forex",
                        Result = "LOSS",
                        Ticker = "AUD/USD",
                        Entry = .71213,
                        TakeProfit = .73000,
                        StopLoss = .68000,
                        RiskReward = "1:5",
                        Notes = "Test Notes"
                    },
                    new Trade() 
                    {
                        JournalId = journalId,
                        Type = "Forex",
                        Result = "WIN",
                        Ticker = "USD/CAD",
                        Entry = 1.27728,
                        TakeProfit = 1.32000,
                        StopLoss = 1.25500,
                        RiskReward = "1:3",
                        Notes = "Test Notes"
                    }
                });

                await context.SaveChangesAsync();
            }
        }
        catch(Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occured during migration");
        }
    }
}