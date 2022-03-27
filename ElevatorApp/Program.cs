

using Logic;

var controller = new Controller();
controller.StartElevator();


while(controller.IsOperating)
{
    Thread.Sleep(3000);
    Console.WriteLine("-- User pressed button - Go to floor 3");
    controller.GoToFloor(3);
    Console.WriteLine("-- User pressed button - Go to floor 5");
    controller.GoToFloor(5, ElevatorDirection.Up);

    Thread.Sleep(9000);
    Console.WriteLine("-- User pressed button - Go to floor 2");
    controller.GoToFloor(2, ElevatorDirection.Down);

    Thread.Sleep(10000);
    Console.WriteLine("-- User pressed button - Go to floor 5");
    controller.GoToFloor(5, ElevatorDirection.Down);

    Thread.Sleep(60000); // run 1 minute before termination
}



