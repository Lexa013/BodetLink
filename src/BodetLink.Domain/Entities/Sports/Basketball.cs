using BodetLink.Domain.SeedWork;

namespace BodetLink.Domain.Entities.Sports;

public class Basketball : Sport
{
    public override Dictionary<int, int> MessagesLength { get; set; } = new()
    {
        { 18, 14 },
        { 36, 5 },
        { 50, 5 },
        { 30, 9 },
        { 31, 11 },
        { 19, 7 },
        { 32, 7 },
        { 33, 15 },
        { 34, 15 },
        { 56, 8 },
        { 37, 32 },
        { 38, 32 },
        { 98, 24 },
        { 99, 24 },
        { 20, 6 },
        { 60, 5 }
    };

    public override void Parse(int messageId, string data)
    {

    }
}