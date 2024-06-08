using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

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