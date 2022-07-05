namespace ProgrammingWithPalermo.ChurchBulletin.Core.Queries;

public class ChurchBulletinItemByDateAndTimeQuery
{
    public DateTime TargetDate { get; }

    public ChurchBulletinItemByDateAndTimeQuery(DateTime targetDate)
    {
        TargetDate = targetDate;
    }
}