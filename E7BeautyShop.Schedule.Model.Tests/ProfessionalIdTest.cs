namespace E7BeautyShop.Schedule.Tests;

public class ProfessionalIdTest
{
    [Fact]
    public void Should_CreateProfessionalId()
    {
        var id = Guid.NewGuid();
        var professional = new ProfessionalId(id);
        Assert.Equal(id, professional.Id);
        Assert.NotNull(professional);
    }
}