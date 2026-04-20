using AspNetCoreGeneratedDocument;

namespace TodayJokesApp.Models
{
    public class Jokes
    {
        public Jokes() { }

        public  int Id { get; set; } 
        public string JokesQuestion { get; set; } 
        public string JokesAnswer { get; set; }
       
        public string? Profile { get; set; }
        public ICollection<Like> Likes { get; set; }
        public List<Comments> Comments { get; set; } =new List<Comments>();

    }
}
