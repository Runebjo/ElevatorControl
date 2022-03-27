namespace Logic
{
    public class Controller
    {
        private int currentFloor = 0;
        private int minFloor = 0;
        private int maxFloor = 8;

        private int timeForEachFloor = 1;
        private int timeForEachStop = 5;

        private List<int> upQueue = new List<int>();
        private List<int> downQueue = new List<int>();

        private ElevatorDirection currentDirection = ElevatorDirection.Stopped;

        public bool IsOperating = false;


        public void StartElevator()
        {
            IsOperating = true;
            MoveElevator();
        }

        /// <summary>
        /// User is pressing the button and it is added to the queue
        /// </summary>
        /// <param name="destinationFloor"></param>
        /// <param name="direction"></param>
        public void GoToFloor(int destinationFloor, ElevatorDirection direction = ElevatorDirection.Stopped)
        {
            // If stopped, check current floor and compare it to next floor
            if (direction == ElevatorDirection.Stopped)
            {
                if (destinationFloor > currentFloor) { 
                    upQueue.Add(destinationFloor);
                    currentDirection = ElevatorDirection.Up;
                }
                else
                {
                    downQueue.Add(destinationFloor);
                    currentDirection = ElevatorDirection.Down;
                }
            }

            else if (direction == ElevatorDirection.Up)
            {
                upQueue.Add(destinationFloor);
            }
            
            else if (direction == ElevatorDirection.Down)
            {
                downQueue.Add(destinationFloor);
            }
        }

        private async void MoveElevator()
        {
            int? nextFloor = null;
            while(IsOperating)
            {
                SetNewDirection();
                Console.WriteLine("");
                Console.WriteLine($"UpQueue: {String.Join(",", upQueue)}");
                Console.WriteLine($"DownQueue: {String.Join(",", downQueue)}");

                if (currentDirection == ElevatorDirection.Stopped)
                {                  
                    Console.WriteLine($"Elevator is idle. Current floor: {currentFloor}");                    
                }
                else if (currentDirection == ElevatorDirection.Up)
                {
                    var destinationFloor = GetNextFloor(ElevatorDirection.Up);
                    if (!destinationFloor.HasValue)
                    {
                        // if no next floor, then go to top
                        destinationFloor = maxFloor;
                    }

                    // only up
                    for (int i = currentFloor; i < destinationFloor; i++)
                    {
                        await Task.Delay(1000);
                        currentFloor++;
                        Console.WriteLine($"Current floor: {currentFloor}, Destination floor: {destinationFloor}");
                    }
                    await Task.Delay(1000);
                    Console.WriteLine("Reached destination floor");

                    for (int i = 0; i < timeForEachStop; i++)
                    {
                        await Task.Delay(1000);
                        Console.WriteLine("Letting off/on passengers");
                    }

                    if (upQueue.Count > 0) upQueue.RemoveAt(0);
                }

                else if (currentDirection == ElevatorDirection.Down)
                {
                    var destinationFloor = GetNextFloor(ElevatorDirection.Down);
                    if (!destinationFloor.HasValue)
                    {
                        // if no next floor, then go to bottom
                        destinationFloor = minFloor;
                    }

                    // only down
                    for (int i = currentFloor; i > destinationFloor; i--)
                    {
                        await Task.Delay(1000);
                        currentFloor--;
                        Console.WriteLine($"Current floor: {currentFloor}, Destination floor: {destinationFloor}");
                    }
                    await Task.Delay(1000);
                    Console.WriteLine("Reached destination floor");

                    for (int i = 0; i < timeForEachStop; i++)
                    {
                        await Task.Delay(1000);
                        Console.WriteLine("Letting off/on passengers");
                    }

                    if (downQueue.Count > 0) downQueue.RemoveAt(0);
                }
                await Task.Delay(1000);
            }
        }

        private void SetNewDirection()
        {
            if (currentDirection == ElevatorDirection.Up)
            {
                if (!upQueue.Any() && downQueue.Any())
                {
                    currentDirection = ElevatorDirection.Down;
                }

                if (!upQueue.Any() && !downQueue.Any())
                {
                    currentDirection = ElevatorDirection.Stopped;
                }
            }

            else if (currentDirection == ElevatorDirection.Down)
            {
                if (upQueue.Any() && !downQueue.Any())
                {
                    currentDirection = ElevatorDirection.Up;
                }

                if (!upQueue.Any() && !downQueue.Any())
                {
                    currentDirection = ElevatorDirection.Stopped;
                }
            }

            else if (currentDirection == ElevatorDirection.Stopped)
            {
                if (upQueue.Any() && !downQueue.Any())
                {
                    currentDirection = ElevatorDirection.Up;
                }

                else if (downQueue.Any() && !upQueue.Any())
                {
                    currentDirection = ElevatorDirection.Down;
                }

                else if (downQueue.Any() && upQueue.Any())
                {
                    // Maybe go to the closest?
                    currentDirection = ElevatorDirection.Up;
                }
            }

            Console.WriteLine($"Set new current direction: ${currentDirection}");
        }

        public ElevatorDirection GetDirection()
        {
            return currentDirection;
        }

        private int? GetNextFloor(ElevatorDirection elevatorDirection)
        {
            int? floor = null;
            if (elevatorDirection == ElevatorDirection.Up)
            {
                if (upQueue.Count > 0) floor = upQueue.FirstOrDefault();
            }

            else if (elevatorDirection == ElevatorDirection.Down)
            {
                if (downQueue.Count > 0) floor = downQueue.FirstOrDefault();
            }

            return floor;
        }

        public void PullEmergencyBreak()
        {
            currentDirection = ElevatorDirection.Stopped;
        }

        public string GetEstimatedTimeToFloor(int floorNum)
        {
            return "0";
        }

    }
}