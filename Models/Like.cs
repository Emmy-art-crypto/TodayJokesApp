using TodayJokesApp.Models;

public class Like
{
    public int Id { get; set; }

    public int JokeId { get; set; }
    public Jokes Joke { get; set; }

    public string UserId { get; set; }
}