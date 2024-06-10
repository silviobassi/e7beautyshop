using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class ProfessionalIdTest
{
    [Fact]
    public void Should_CreateProfessionalId()
    {
        var id = Guid.NewGuid();
        var professional = new ProfessionalId(id);
        Assert.Equal(id, professional.Value);
        Assert.NotNull(professional);
    }
}