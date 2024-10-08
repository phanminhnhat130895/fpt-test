﻿using AutoDrivingCarSimulation.Core.Entities;

namespace AutoDrivingCarSimulation.Infrastructure
{
    public class DataContext : IDataContext
    {
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
