/// <summary>
/// Central model class that manages the collections of podcasts, users,
/// listening records, and feedback. Provides business logic for registration,
/// authentication, subscriptions, listening, and statistics.
/// </summary>
public class PodcastWorld : IUserService, IPodcastService, IListeningService
{
    // this is the main class of the application and it will be used to manage the podcasts and users
    // privaate fields to store the podcasts and users
    private  List<Podcast> Podcasts { get; set; }
    private List<User> Users { get; set; }

    public List<Episode> Episodes { get; set; }

    public List<Podcaster> podcasters { get; set; }

    private List<ListeningRecord> ListeningRecords { get; set; }
    private List<Feedback> Feedbacks { get; set; }


    public PodcastWorld()
    {
        Podcasts = new List<Podcast>();
        Users = new List<User>();
        Episodes = new List<Episode>();
        podcasters = new List<Podcaster>();
        ListeningRecords = new List<ListeningRecord>();
        Feedbacks = new List<Feedback>();
    }
    // Users

    public User RegisterUser(string name, int age, string email, string mobileNumber, string password, int userType)
    {
        User newUser  = new Listener(name, age, email, mobileNumber, password);
        Users.Add(newUser);
        return newUser;

    }

    public User RegisterPremiumUser(string name, int age, string email, string mobileNumber, string password, DateTime registrationDate, int payId)
    {
        // this method will be used to register a premium user and return the user object
        PremiumListener newPremiumUser = new PremiumListener(name, age, email, mobileNumber, password);
        newPremiumUser.RegistrationDate = registrationDate;
        newPremiumUser.PayId = payId;
        Users.Add(newPremiumUser);
        return newPremiumUser;
    }

    public User RegisterPodcaster(string name, int age, string email, string mobileNumber, string password, string podcastName, string podcastDescription, Genre podcastGenre)
    {
        Podcaster newPodcaster = new Podcaster(name, age, email, mobileNumber, password);
        Podcast podcast = new Podcast();
        podcast.Title = podcastName;
        podcast.Description = podcastDescription;
        podcast.Genre = podcastGenre;
        podcast.Host = newPodcaster;
        newPodcaster.AddPodcast(podcast);
        Podcasts.Add(podcast);
        Users.Add(newPodcaster);
        return newPodcaster;
    }
    public Episode RegisterEpisode(Podcast podcast, string episodeName, string podcastDescription, DateTime? episodeRecorded, int episodeLength, bool premiumEpisode)
    {
        
        Episode newEpisode = new Episode(episodeName,podcastDescription, episodeRecorded, episodeLength, premiumEpisode  );
        podcast.AddEpisode(newEpisode);
        return newEpisode;
        
    }



    public  User AuthenticateUser(string email, string password)
    {
        // this method will be used to authenticate the user and return the user object if the user is authenticated
        // otherwise it will return null
        User user = Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            return user;
        }
        else
        {
            return null;
        }
    }
    public User ChangePassword(string email, string oldPassword, string newUserPassword)
    {
        // this method will be used to change the password of the user and return a message if the password is changed successfully
        // otherwise it will return a message if the old password is incorrect
        User user = Users.FirstOrDefault(u => u.Email == email && u.Password == oldPassword);
        if (user != null)
        {
            user.Password = newUserPassword;
            return  user;
        }
        else
        {
            return null;
        }
    }

    // 
    public bool CheckUserExistence()
    {
        return Users?.Any() == true;
    }
    public bool CheckEmailExistence(string email)
    {
        return Users?.Any(u => u.Email == email) == true;
    }


    public List<Podcast> GetPodcastInAlphabecticOrder()
    {
        // we get the OrderBy mehtod and then we look for title ad then we order the list of podcasts by title and then we return the list of podcasts in alphabetical order
        return Podcasts.OrderBy(p => p.Title).ToList();
    
    }

    public User GetUserByEmail(string email)
    {
        return Users.FirstOrDefault(u => u.Email == email);
    }

    public bool CheckPodcastExistence()
    {
        return Podcasts?.Any() == true;
    }

    public void SubscribeToPodcast(PremiumListener user, Podcast podcast)
    {
        user.Subscribe(podcast);
        podcast.Subscribers++;
    }

    public List<Podcast> GetAvailablePodcasts(PremiumListener user)
    {
        return GetPodcastInAlphabecticOrder().Where(p => !user.SubscribedPodcasts.Contains(p)).ToList();
    }

    public void UnsubscribeFromPodcast(PremiumListener user, Podcast podcast)
    {
        user.Unsubscribe(podcast);
        podcast.Subscribers--;
    }

    public ListeningRecord GetListeningRecord(User user, Episode episode)
    {
        return ListeningRecords.FirstOrDefault(r => r.Listener == user && r.Episode == episode);
    }

    public ListeningRecord ListenToEpisode(User user, Podcast podcast, Episode episode, int minutesListened, DateTime listenDate)
    {
        ListeningRecord record = GetListeningRecord(user, episode);
        if (record == null)
        {
            record = new ListeningRecord(user, podcast, episode);
            ListeningRecords.Add(record);
            user.EpisodesStarted++;
        }
        record.MinutesListened += minutesListened;
        record.LastListenDate = listenDate;
        user.TotalMinutesListenedTo += minutesListened;
        user.AdvertisementsListenedTo = user.TotalMinutesListenedTo / 30;
        if (record.MinutesListened >= episode.EpisodeLength)
        {
            record.Completed = true;
            user.EpisodesCompleted++;
        }
        return record;
    }

    public void AddFeedback(User user, Episode episode, bool thumbsUp, string textFeedback)
    {
        Feedbacks.Add(new Feedback(user, episode, thumbsUp, textFeedback));
    }

    public List<Feedback> GetFeedbackForEpisode(Episode episode)
    {
        return Feedbacks.Where(f => f.Episode == episode).ToList();
    }

    public int GetThumbsUpCount(Episode episode)
    {
        return Feedbacks.Count(f => f.Episode == episode && f.ThumbsUp);
    }

    public int GetListenersStarted(Episode episode)
    {
        return ListeningRecords.Count(r => r.Episode == episode);
    }

    public int GetListenersCompleted(Episode episode)
    {
        return ListeningRecords.Count(r => r.Episode == episode && r.Completed);
    }
}