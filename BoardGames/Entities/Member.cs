namespace BoardGames.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime JoinDate { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
