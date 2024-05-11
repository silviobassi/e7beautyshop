namespace E7BeautyShop.Domain;

public class OfficeHourFactory: IOfficeHourFactory
{
    public IOfficeHour Create(TimeSpan? hour)
    {
        return new OfficeHour(hour);
    }
}