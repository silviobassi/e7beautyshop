namespace E7BeautyShop.Domain;

public interface IOfficeHour
{
    public TimeSpan? Hour { get; protected set; }
    public void ToMakeAvailable();
    public void ToMakeUnavailable();
}