namespace InboxMeMvc
{
    public interface IGMailServiceConfiguration
    {
        string GmailAccount { get; }
        string GmailAccountPassword { get;  }
    }
}