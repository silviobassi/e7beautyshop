namespace E7BeautyShop.Domain;

public interface IOfficeHourFactory
{
    public IOfficeHour Create(TimeSpan? hour);
}