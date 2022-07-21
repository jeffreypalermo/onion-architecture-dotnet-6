namespace ProgrammingWithPalermo.ChurchBulletin.Core;

public class ChurchBulletinException : ApplicationException
{
    public ChurchBulletinException(string? message, Exception? innerException = null)
        : base(message, innerException){}
}