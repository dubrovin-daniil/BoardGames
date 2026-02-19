namespace BoardGames
{
    public class Program
    {
        static void Main(string[] args)  
        {
            AppDbContext db = new AppDbContext();

            if (!db.Games.Any() && !db.Members.Any() && !db.Sessions.Any())
            {  
                InitTables.GamesInitialize(db);
                InitTables.MembersInitialize(db);
                InitTables.RandomSessionsInitialize(db);
            }
        } 
    }
}
