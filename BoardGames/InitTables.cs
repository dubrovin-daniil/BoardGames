using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames
{
    public static class InitTables
    {
        public static void GamesInitialize(AppDbContext db)
        {

            db.Games.AddRange(
                new Entities.Game { Title = "Chess", Genre = "Strategy", MinPlayers = 2, MaxPlayers = 2 },
                new Entities.Game { Title = "Monopoly", Genre = "Economic", MinPlayers = 2, MaxPlayers = 6 },
                new Entities.Game { Title = "Catan", Genre = "Strategy", MinPlayers = 3, MaxPlayers = 4 },
                new Entities.Game { Title = "Pandemic", Genre = "Cooperative", MinPlayers = 2, MaxPlayers = 4 },
                new Entities.Game { Title = "Ticket to Ride", Genre = "Family", MinPlayers = 2, MaxPlayers = 5 }
            );
            db.SaveChanges();
        }
        public static void MembersInitialize(AppDbContext db)
        {
            db.Members.AddRange(
                new Entities.Member { FullName = "Alice Grace"},
                new Entities.Member { FullName = "Bob Smith" },
                new Entities.Member { FullName = "Charlie Johnson" },
                new Entities.Member { FullName = "Diana Lee" },
                new Entities.Member { FullName = "Ethan Brown" }
            );
            db.SaveChanges();
        }
        public static void RandomSessionsInitialize(AppDbContext db)
        {
            var random = new Random();
            var gameIds = db.Games.Select(g => g.Id).ToList();
            var memberIds = db.Members.Select(m => m.Id).ToList();

            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;

            for (int i = 0; i < 20; i++)
            {
                var session = new Entities.Session
                {
                    GameId = gameIds[random.Next(gameIds.Count)],
                    MemberId = memberIds[random.Next(memberIds.Count)],
                    DurationMinutes = random.Next(30, 180),
                    Date = start.AddDays(random.Next(range))
                };
                db.Sessions.Add(session);
            }
            db.SaveChanges();
        }
    }
}
