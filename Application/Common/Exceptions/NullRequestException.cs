namespace Application.Common.Exceptions;

public class NullRequestException : Exception
{
    public NullRequestException(string message) : base(message)
    {
        
    }
}