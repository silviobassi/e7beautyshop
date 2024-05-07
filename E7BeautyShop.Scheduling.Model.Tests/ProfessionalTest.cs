namespace E7BeautyShop.Domain.Tests;

public class ProfessionalTest
{
    [Fact]
    public void Should_CreateProfessionalId()
    {
        var id = Guid.NewGuid();
        var professional = new Professional(id);
        Assert.Equal(id, professional.Id);
        Assert.NotNull(professional);
    }
}