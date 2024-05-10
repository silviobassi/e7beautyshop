namespace E7BeautyShop.Domain;

public class Schedule
{
    public Guid Id { get; private set; }
    public IEnumerator<OfficeDay> OfficeDays { get; private set; }
    
    public Schedule(IEnumerator<OfficeDay> officeDays)
    {
        OfficeDays = officeDays;
    }
}
