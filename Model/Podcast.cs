/// <summary>
/// Represents a podcast with a title, description, genre, host, and episodes.
/// </summary>
public class Podcast
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Genre Genre { get; set; }
    public Podcaster Host { get; set; }
    public int Subscribers { get; set; } = 0;

    private List<Episode> _episodes = new List<Episode>();

    /// <summary>
    /// Read-only view of the episodes belonging to this podcast.
    /// </summary>
    public IReadOnlyList<Episode> Episodes => _episodes;

    /// <summary>
    /// Adds an episode to this podcast.
    /// </summary>
    public void AddEpisode(Episode episode)
    {
        _episodes.Add(episode);
    }

    public static bool IsValidGenreChoice(int choice)
    {
        return choice >= 1 && choice <= Enum.GetValues<Genre>().Length;
    }
}
