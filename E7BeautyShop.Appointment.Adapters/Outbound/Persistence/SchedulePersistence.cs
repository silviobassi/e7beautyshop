using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Infra.Context;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class SchedulePersistence(ApplicationDbContext context)
    : Persistence<Schedule>(context), ISchedulePersistencePort;