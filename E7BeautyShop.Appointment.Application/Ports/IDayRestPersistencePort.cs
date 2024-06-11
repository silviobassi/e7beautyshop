﻿using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface IDayRestPersistencePort
{
    Task<DayRest?> GetByIdAsync(Guid id);
}