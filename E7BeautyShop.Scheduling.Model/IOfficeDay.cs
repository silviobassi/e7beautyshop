namespace E7BeautyShop.Domain;

public interface IOfficeDay
{
    public DateTime? Date { get; protected set; }
    public List<IOfficeHour> OfficeHours { get; protected set; }
    public bool IsAttending { get; protected set; }
    public void Generate();
    public void InformDate(DateTime? date);
    void IncrementDate(int daysNumber);
    public void Attend();
    public void Cancel();
}