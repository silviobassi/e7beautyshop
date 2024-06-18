using E7BeautyShop.Appointment.Core.ObjectsValue;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core;

public class ProfessionalIdTest
{
    [Fact]
    public void Should_CreateProfessionalId()
    {
        var id = Guid.NewGuid();
        ProfessionalId professionalId = id;
        Assert.Equal(id, professionalId.Value);
        Assert.NotNull(professionalId);
    }
}