namespace Logic
{
    public class Controller
    {
        public Elevator elevator { get; set; }
        public Controller()
        {            
            elevator = new Elevator(8);
        }

        public void StartElevatorSystem()
        {
            elevator.StartElevator();
        }

        public void GoToFloor(int destinationFloor, ElevatorDirection direction = ElevatorDirection.Stopped)
        {
            elevator.GoToFloor(destinationFloor, direction);
        }
    }
}