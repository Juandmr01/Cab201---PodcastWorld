/// <summary>
/// Tracks a user's listening progress for a specific episode,
/// including total minutes listened, last listen date, and completion status.
/// </summary>
public class ListeningRecord
{
    public User Listener { get; set; }
    public Podcast Podcast { get; set; }
    public Episode Episode { get; set; }
    public int MinutesListened { get; set; }
    public DateTime LastListenDate { get; set; }
    public bool Completed { get; set; }

    public ListeningRecord(User listener, Podcast podcast, Episode episode)
    {
        Listener = listener;
        Podcast = podcast;
        Episode = episode;
        MinutesListened = 0;
        Completed = false;
    }
}
