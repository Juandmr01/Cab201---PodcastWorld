/// <summary>
/// Represents a podcaster who can create and host podcasts with episodes.
/// </summary>
public class Podcaster : User
{
    public string SubscriptionType { get; set; }
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime SubscriptionEndDate { get; set; }

    private List<Podcast> _podcasts = new List<Podcast>();

    /// <summary>
    /// Read-only view of the podcasts hosted by this podcaster.
    /// </summary>
    public IReadOnlyList<Podcast> Podcasts => _podcasts;

    public Podcaster(string name, int age, string email, string mobileNumber, string password)
        : base(name, age, email, mobileNumber, password)
    {
    }

    /// <summary>
    /// Adds a podcast to this podcaster's collection.
    /// </summary>
    public void AddPodcast(Podcast podcast)
    {
        _podcasts.Add(podcast);
    }
}
