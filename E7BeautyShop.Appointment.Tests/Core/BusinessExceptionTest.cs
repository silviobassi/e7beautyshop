﻿using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class BusinessExceptionTest
{
    [Fact]
    public void Should_ThrowException_When_ConditionIsTrue()
    {
        var exception = Assert.Throws<BusinessException>(
            () => BusinessException.When(true, "Test"));
        Assert.Equal("Test", exception.Message);
    }
}