﻿using AutoDrivingCarSimulation.Core.Common.Enums;

namespace AutoDrivingCarSimulation.Core.Entities
{
    public class Car
    {
        public string Name { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction Facing { get; private set; }
        public string Command { get; }
        public bool CanMove { get; set; } = true;
        public string CollideName {  get; set; }
        public int CollideStep { get; set; }

        public Car(string name, int x, int y, Direction facing, string command)
        {
            Name = name;
            X = x;
            Y = y;
            Facing = facing;
            Command = command;
        }

        public virtual void RotateLeft(Field field)
        {
            Direction current = Facing;
            Facing = (Direction)(((int)Facing + 3) % 4);
        }

        public virtual void RotateRight(Field field)
        {
            Direction current = Facing;
            Facing = (Direction)(((int)Facing + 1) % 4);
        }

        public virtual void MoveForward(Field field)
        {
            int newX = X, newY = Y;

            switch (Facing)
            {
                case Direction.N: newY++; break;
                case Direction.E: newX++; break;
                case Direction.S: newY--; break;
                case Direction.W: newX--; break;
            }

            // Ensure the car stays within the field boundaries
            if (field.IsWithinBounds(newX, newY))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                CanMove = false;
            }
        }

        public string GetInfo()
        {
            return $"- {Name}, ({X},{Y}) {Facing}, {Command}";
        }

        public override string ToString()
        {
            if (CanMove || string.IsNullOrEmpty(CollideName))
                return $"- {Name}, ({X},{Y}) {Facing}";
            
            return $"- {Name} collides with {CollideName} at ({X},{Y}) at step {CollideStep}"; 
        }
    }
}
