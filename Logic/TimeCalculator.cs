namespace Logic
{
    public static class TimeCalculator
    {
        public static int GetTimeToFloor(int currentFloor, int destinationFloor, List<bool> upQueue, List<bool> downQueue, ElevatorDirection direction)
        {
            var seconds = 0;
            if (direction == ElevatorDirection.Up)
            {
                var stopsOnTheWayUp = upQueue.Select((value, index) => new { value, index }).Where(x => x.value == true && x.index > currentFloor).Select(x => x.index).ToList();
                foreach(var stop in stopsOnTheWayUp)
                {
                    seconds += stop - currentFloor;
                    if (stop == destinationFloor) break;
                    currentFloor = stop;

                    seconds += 5; // add 5 seconds for stopping
                }

                var stopsOnTheWayDown = downQueue.Select((value, index) => new { value, index }).Where(x => x.value == true && x.index < currentFloor).Select(x => x.index).ToList();
                foreach (var stop in stopsOnTheWayDown)
                {
                    seconds += currentFloor - stop;
                    if (stop == destinationFloor) break;
                    currentFloor = stop;

                    seconds += 5; // add 5 seconds for stopping
                }
            }

            if (direction == ElevatorDirection.Down)
            {
                var stopsOnTheWayDown = downQueue.Select((value, index) => new { value, index }).Where(x => x.value == true && x.index < currentFloor).Select(x => x.index).ToList();
                foreach (var stop in stopsOnTheWayDown)
                {
                    seconds += currentFloor - stop;
                    if (stop == destinationFloor) break;
                    currentFloor = stop;

                    seconds += 5; // add 5 seconds for stopping
                }

                var stopsOnTheWayUp = upQueue.Select((value, index) => new { value, index }).Where(x => x.value == true && x.index > currentFloor).Select(x => x.index).ToList();
                foreach (var stop in stopsOnTheWayUp)
                {
                    seconds += stop - currentFloor;
                    if (stop == destinationFloor) break;
                    currentFloor = stop;

                    seconds += 5; // add 5 seconds for stopping
                }
            }

            return seconds;
        }
    }
}
