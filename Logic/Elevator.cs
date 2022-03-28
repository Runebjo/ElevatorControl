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

        private List<bool> upQueue;
        private List<bool> downQueue;

        private ElevatorDirection currentDirection = ElevatorDirection.Stopped;

        private int timeForEachStop;

        public Elevator(int maxFloor)
        {
            upQueue = Enumerable.Repeat(false, maxFloor).ToList();
            downQueue = Enumerable.Repeat(false, maxFloor).ToList();
        }

        public void StartElevator()
        {
            MoveElevator();
        }

        public void GoToFloor(int destinationFloor, ElevatorDirection direction = ElevatorDirection.Stopped)
        {
            // If stopped, check current floor and compare it to next floor
            if (direction == ElevatorDirection.Stopped)
            {
                if (destinationFloor > currentFloor)
                {
                    upQueue[destinationFloor] = true;
                }
                else
                {
                    downQueue[destinationFloor] = true;
                }
            }

            else if (direction == ElevatorDirection.Up)
            {
                upQueue[destinationFloor] = true;
            }

            else if (direction == ElevatorDirection.Down)
            {
                downQueue[destinationFloor] = true;
            }
        }

        private async void MoveElevator()
        {
            Console.WriteLine($"Init current direction: {currentDirection}");
            while (true)
            {
                currentDirection = GetDirection();
                Console.WriteLine("");
                Console.WriteLine($"current direction: {currentDirection}");

                if (currentDirection == ElevatorDirection.Stopped)
                {
                    Console.WriteLine($"Elevator is idle. Current floor: {currentFloor}");
                }

                else if (currentDirection == ElevatorDirection.Up)
                {
                    currentFloor++;
                    await ProcessFloor();
                }

                else if (currentDirection == ElevatorDirection.Down)
                {
                    currentFloor--;
                    await ProcessFloor();
                }

                await Task.Delay(1000);
                Console.WriteLine($"Current Floor: {currentFloor}");
            }
        }

        private async Task ProcessFloor()
        {
            if (currentDirection == ElevatorDirection.Down)
            {
                if (downQueue[currentFloor] == true)
                {
                    Console.WriteLine($"reached destination. CurrentFloor: {currentFloor}");
                    downQueue[currentFloor] = false;
                    await LetPassengersOff();

                }

                // Person on the lowest floor. Pick up
                else if (upQueue[currentFloor] == true && !HasPendingBelow())
                {
                    Console.WriteLine($"reached destination. CurrentFloor: {currentFloor}");
                    upQueue[currentFloor] = false;
                    await LetPassengersOff();
                    currentDirection = ElevatorDirection.Up; // no more passengers below. Going up.
                }
            }

            if (currentDirection == ElevatorDirection.Up)
            {
                if (upQueue[currentFloor] == true)
                {
                    Console.WriteLine($"reached destination. CurrentFloor: {currentFloor}");
                    upQueue[currentFloor] = false;
                    await LetPassengersOff();
                }

                else if (downQueue[currentFloor] == true && !HasPendingAbove())
                {
                    Console.WriteLine($"reached destination. CurrentFloor: {currentFloor}");
                    downQueue[currentFloor] = false;
                    await LetPassengersOff();
                    currentDirection = ElevatorDirection.Up; // no more passengers above. Going down.
                    currentDirection = ElevatorDirection.Down;
                }
            }
        }

        private async Task LetPassengersOff()
        {
            for (int i = 0; i < timeForEachStop; i++)
            {
                await Task.Delay(1000);
                Console.WriteLine("Letting passengers on/off");
            }
        }

        private ElevatorDirection GetDirection()
        {
            if (HasNoMoreQueue())
            {
                return ElevatorDirection.Stopped;
            }

            if (currentDirection == ElevatorDirection.Up)
            {
                // Check if there is floors queued up above current floor
                if (HasPendingAbove())
                {
                    return ElevatorDirection.Up;
                }

                return ElevatorDirection.Down;
            }

            else if (currentDirection == ElevatorDirection.Down)
            {
                // Check if there is
                if (HasPendingBelow())
                {
                    return ElevatorDirection.Down;
                }

                return ElevatorDirection.Up;
            }

            if (currentDirection == ElevatorDirection.Stopped)
            {
                if (HasUpQueue() && !HasDownQueue())
                {
                    return ElevatorDirection.Up;
                }

                else if (HasDownQueue() && !HasUpQueue())
                {
                    return ElevatorDirection.Down;
                }

                else if (HasDownQueue() && HasUpQueue())
                {
                    // Maybe go to the closest?
                    return ElevatorDirection.Up;
                }
            }

            return ElevatorDirection.Invalid;
        }

        private bool HasNoMoreQueue()
        {
            if (!upQueue.Any(u => u == true) && !downQueue.Any(d => d == true))
            {
                return true;
            }

            return false;
        }

        private bool HasDownQueue()
        {
            return downQueue.Any(d => d == true);
        }

        private bool HasUpQueue()
        {
            return upQueue.Any(d => d == true);
        }

        private bool HasPendingBelow()
        {
            return HasPendingDownBelow() || HasPendingUpBelow();
        }

        private bool HasPendingAbove()
        {
            return HasPendingDownAbove() || HasPendingUpAbove();
        }

        private bool HasPendingDownBelow()
        {
            return downQueue.Select((value, index) => new { value, index }).Any(x => x.value == true && x.index < currentFloor);
        }

        private bool HasPendingUpBelow()
        {
            return upQueue.Select((value, index) => new { value, index }).Any(x => x.value == true && x.index < currentFloor);
        }

        private bool HasPendingDownAbove()
        {
            return downQueue.Select((value, index) => new { value, index }).Any(x => x.value == true && x.index > currentFloor);
        }

        private bool HasPendingUpAbove()
        {
            return upQueue.Select((value, index) => new { value, index }).Any(x => x.value == true && x.index > currentFloor);
        }

        public void PullEmergencyBreak()
        {
            currentDirection = ElevatorDirection.Stopped;
        }

        public string GetEstimatedTimeToFloor(int floorNum)
        {
            var timeToFloor = TimeCalculator.GetTimeToFloor(currentFloor, floorNum, upQueue, downQueue, currentDirection);
            return $"{timeToFloor} seconds";
        }
    }
}
