using Api.Data;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Test.Helpers;

public static class TestDb
{
    public static AppDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.Add(new AppUser
            {
                Id = "1",
                UserName = "user",
                PasswordHash = "Pa$$w0rd",
                Email = "user@trader.com"
            });

            context.SaveChanges();
        }

        if (!context.Journals.Any())
        {
            context.Journals.Add(new Journal
            {
                Id = 1,
                Name = "TestJournal",
                Description = "TestDescription",
                AppUserId = "1"
            });

            context.SaveChanges();
        }

        if (!context.Trades.Any())
        {
            context.Trades.Add(new Trade
            {
                JournalId = 1,
                Type = "Forex",
                Result = "WIN",
                Ticker = "EUR/USD",
                Entry = 1.02590,
                TakeProfit = 1.05000,
                StopLoss = 1.01450,
                RiskReward = "1:3",
                Notes = "Test Notes"
            });

            context.SaveChanges();
        }

        return context;
    }
}
