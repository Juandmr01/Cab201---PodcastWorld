/// <summary>
/// Represents a premium listener who can subscribe to podcasts
/// and access premium-only episodes.
/// </summary>
public class PremiumListener : User, ISubscribable
{
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public int PayId { get; set; } = 0;
    public string SubscriptionType { get; set; }
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime SubscriptionEndDate { get; set; }

    private List<Podcast> _subscribedPodcasts = new List<Podcast>();

    /// <summary>
    /// Read-only view of the podcasts this premium listener is subscribed to.
    /// </summary>
    public IReadOnlyList<Podcast> SubscribedPodcasts => _subscribedPodcasts;

    public PremiumListener(string name, int age, string email, string mobileNumber, string password)
        : base(name, age, email, mobileNumber, password)
    {
    }

    /// <summary>
    /// Subscribes this listener to a podcast.
    /// </summary>
    public void Subscribe(Podcast podcast)
    {
        _subscribedPodcasts.Add(podcast);
    }

    /// <summary>
    /// Unsubscribes this listener from a podcast.
    /// </summary>
    public void Unsubscribe(Podcast podcast)
    {
        _subscribedPodcasts.Remove(podcast);
    }

    /// <summary>
    /// Validates that the registration date is a valid date.
    /// </summary>
    public static bool IsValidRegistrationDate(DateTime registrationDate)
    {
        return true;
    }

    /// <summary>
    /// Validates that the pay ID is a 6-digit number.
    /// </summary>
    public static bool IsValidPayId(int payId)
    {
        return payId >= 100000 && payId <= 999999;
    }
}
