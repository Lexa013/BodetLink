namespace BodetLink.Domain.SeedWork;

public abstract class Sport
{
    public abstract Dictionary<int, int> MessagesLength { get; set; }

    public abstract void Parse(int messageId, string data);
}