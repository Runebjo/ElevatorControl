

using Logic;

// Simple app for testing the flow

var controller = new Controller();
controller.StartElevatorSystem();


Thread.Sleep(3000);
Console.WriteLine("-- User pressed inside button - Go to floor 3");
controller.GoToFloor(3);
Console.WriteLine("-- User pressed outside button - Go to floor 5 - up");
controller.GoToFloor(5, ElevatorDirection.Up);

Thread.Sleep(1000);
Console.WriteLine($"Time to floor 5 - {controller.GetTimeToFloor(5)}");

Thread.Sleep(4000);
Console.WriteLine("-- User pressed outside button - Go to floor 2 - up");
controller.GoToFloor(2, ElevatorDirection.Up);

Thread.Sleep(2000);
Console.WriteLine("-- User pressed outside button - Go to floor 0 - up");
controller.GoToFloor(0, ElevatorDirection.Up);

Thread.Sleep(5000);
Console.WriteLine("-- User pressed button - Go to floor 5 - up");
controller.GoToFloor(5, ElevatorDirection.Up);

Thread.Sleep(2000);
Console.WriteLine("-- User pressed button - Go to floor 0 - down");
controller.GoToFloor(0, ElevatorDirection.Down);

Thread.Sleep(60000); // run 1 minute before termination




