using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Elevator
    {
        private readonly ElevatorDirection _direction;
        private int currentFloor = 0;
        private int minFloor = 0;
        private int maxFloor = 8;
    }
}
