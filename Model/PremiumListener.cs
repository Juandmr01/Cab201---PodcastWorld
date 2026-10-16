/// <summary>
/// Represents a premium listener who can subscribe to podcasts
/// and access premium-only episodes.
/// </summary>
public class PremiumListener : User, ISubscribable
{
    private int _payId;

    public DateTime RegistrationDate { get; private set; } = DateTime.Now;

    public int PayId
    {
        get => _payId;
        private set
        {
            if (!Validation.IsValidPayId(value))
                throw new ArgumentException(MessageBox.InvalidPayId);
            _payId = value;
        }
    }

    public string SubscriptionType { get; set; }
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime SubscriptionEndDate { get; set; }

    private List<Podcast> _subscribedPodcasts = new List<Podcast>();

    /// <summary>
    /// Read-only view of the podcasts this premium listener is subscribed to.
    /// </summary>
    public IReadOnlyList<Podcast> SubscribedPodcasts => _subscribedPodcasts;

    public PremiumListener(string name, int age, string email, string mobileNumber, string password, DateTime registrationDate, int payId)
        : base(name, age, email, mobileNumber, password)
    {
        RegistrationDate = registrationDate;
        PayId = payId;
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


}
