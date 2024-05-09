namespace E7BeautyShop.Domain.Tests;

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