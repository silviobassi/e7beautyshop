﻿namespace E7BeautyShop.Schedule;

public class Appointment: Entity
{

    protected Appointment() => IsAvailable = true;
    
    public bool IsAvailable { get; protected set; }
    public void Cancel() => IsAvailable = false;
    public void Attend() => IsAvailable = true;
}