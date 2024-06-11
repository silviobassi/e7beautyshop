using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Infra.Context;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class OfficeHourPersistence(ApplicationDbContext context)
    : Persistence<OfficeHour>(context), IOfficeHourPersistencePort;