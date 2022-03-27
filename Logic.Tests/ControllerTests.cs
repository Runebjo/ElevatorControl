using Xunit;

namespace Logic.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void Test1()
        {
            var controller = new Controller();
            controller.GoToFloor(5);
            controller.GoToFloor(3, ElevatorDirection.Up);
            controller.GoToFloor(4, ElevatorDirection.Up);
        }
    }
}