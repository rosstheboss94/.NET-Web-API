using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public static class Seed 
    {

        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = 1,
                    UserName = "User",
                    Password = "Pa$$w0rd",
                    Email = "user@user.com",
                }
            );

            // modelBuilder.Entity<Trade>().HasData(
            //     new Trade() 
            //     {
            //         Id = 1,
            //         Type = "Forex",
            //         Result = "WIN",
            //         Ticker = "EUR/USD",
            //         Entry = 1.02590,
            //         TakeProfit = 1.05000,
            //         StopLoss = 1.01450,
            //         RiskReward = "1:3",
            //         Notes = "Test Notes"
            //     },
            //     new Trade() 
            //     {
            //         Id = 2,
            //         Type = "Forex",
            //         Result = "LOSS",
            //         Ticker = "AUD/USD",
            //         Entry = .71213,
            //         TakeProfit = .73000,
            //         StopLoss = .68000,
            //         RiskReward = "1:5",
            //         Notes = "Test Notes"
            //     },
            //     new Trade() 
            //     {
            //         Id = 3,
            //         Type = "Forex",
            //         Result = "WIN",
            //         Ticker = "USD/CAD",
            //         Entry = 1.27728,
            //         TakeProfit = 1.32000,
            //         StopLoss = 1.25500,
            //         RiskReward = "1:3",
            //         Notes = "Test Notes"
            //     }
            // );
        }
    }
}