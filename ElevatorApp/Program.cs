

using Logic;

var controller = new Controller();
controller.StartElevatorSystem();


Thread.Sleep(3000);
Console.WriteLine("-- User pressed inside button - Go to floor 3");
controller.GoToFloor(3);
Console.WriteLine("-- User pressed outside button - Go to floor 5 - up");
controller.GoToFloor(5, ElevatorDirection.Up);

Thread.Sleep(9000);
Console.WriteLine("-- User pressed outside button - Go to floor 2 - up");
controller.GoToFloor(2, ElevatorDirection.Up);

Thread.Sleep(2000);
Console.WriteLine("-- User pressed outside button - Go to floor 0 - up");
controller.GoToFloor(0, ElevatorDirection.Up);

//Thread.Sleep(10000);
//Console.WriteLine("-- User pressed button - Go to floor 5");
//controller.GoToFloor(5, ElevatorDirection.Down);

Thread.Sleep(60000); // run 1 minute before termination




