namespace InboxMeMvc.Configuration
{
    public interface IGMailServiceConfiguration
    {
        string GmailAccount { get; }
        string GmailAccountPassword { get;  }
    }
}