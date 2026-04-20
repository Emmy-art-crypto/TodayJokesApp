namespace TodayJokesApp.Models
{
    public class Comments
    {
        public int Id { get; set; } 
        public string Content { get; set; } 
        public  DateTime CreatedAt{ get; set; }   
        public int JokeId { get; set; }
        public Jokes Joke { get; set; }
        public string ? UserId { get; set; }    
    }
}
