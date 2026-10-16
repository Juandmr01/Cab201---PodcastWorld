/// <summary>
/// Represents a podcast with a title, description, genre, host, and episodes.
/// </summary>
public class Podcast
{
    /// <summary>The podcast's title.</summary>
    public string Title { get; private set; }

    /// <summary>A short description of the podcast.</summary>
    public string Description { get; private set; }

    /// <summary>The genre category of the podcast.</summary>
    public Genre Genre { get; private set; }

    /// <summary>The podcaster who hosts this podcast.</summary>
    public Podcaster Host { get; private set; }

    /// <summary>The current number of subscribers.</summary>
    public int Subscribers { get; private set; } = 0;

    private List<Episode> _episodes = new List<Episode>();

    /// <summary>
    /// Read-only view of the episodes belonging to this podcast.
    /// </summary>
    public IReadOnlyList<Episode> Episodes => _episodes;

    /// <summary>
    /// Creates a new podcast with the specified details.
    /// </summary>
    public Podcast(string title, string description, Genre genre, Podcaster host)
    {
        Title = title;
        Description = description;
        Genre = genre;
        Host = host;
    }

    /// <summary>
    /// Adds an episode to this podcast.
    /// </summary>
    public void AddEpisode(Episode episode)
    {
        _episodes.Add(episode);
    }

    /// <summary>
    /// Increments the subscriber count by one.
    /// </summary>
    public void IncrementSubscribers()
    {
        Subscribers++;
    }

    /// <summary>
    /// Decrements the subscriber count by one.
    /// </summary>
    public void DecrementSubscribers()
    {
        Subscribers--;
    }

    /// <summary>
    /// Validates that the genre choice is within the valid range.
    /// </summary>
    public static bool IsValidGenreChoice(int choice)
    {
        return choice >= 1 && choice <= Enum.GetValues<Genre>().Length;
    }
}
