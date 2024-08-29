﻿using AutoDrivingCarSimulation.Domain.Entities;

namespace AutoDrivingCarSimulation.Infrastructure
{
    public interface IDataContext
    {
        List<Car> Cars { get; set; }
    }
}
