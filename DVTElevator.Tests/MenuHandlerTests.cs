﻿using DVTElevator.Application;
using DVTElevator.UI;
using Microsoft.Extensions.Logging;
using Moq;

namespace DVTElevator.Tests
{
    /// <summary>
    /// Method Tests within MenuHandler.cs
    /// </summary>
    public class MenuHandlerTests
    {
        [Fact]
        public async Task RunAsync_Option1_ElevatorRequest()
        {
            var mockConsole = new Mock<IConsoleService>();
            var mockController = new Mock<IElevatorController>();
            var mockSettings = new Mock<ISettings>();
            var mockLogger = new Mock<ILogger<MenuHandler>>();

            mockSettings.SetupGet(x => x.MinMenuChoice).Returns(1);
            mockSettings.SetupGet(x => x.MaxMenuChoice).Returns(3);
            mockSettings.SetupGet(x => x.MinFloor).Returns(0);
            mockSettings.SetupGet(x => x.MaxFloor).Returns(10);

            mockConsole.SetupSequence(x => x.ReadChoice(1, 3))
                       .Returns(1)  //Call elevator
                       .Returns(3); //Exit

            mockConsole.Setup(x => x.ReadPassengerCount()).Returns(2);
            mockConsole.Setup(x => x.ReadFloorNumber(0, 10, false)).Returns(1);
            mockConsole.Setup(x => x.ReadFloorNumber(0, 10, true)).Returns(5);

            mockController.Setup(x => x.RequestElevator(1, 5, 2)).Returns(ElevatorDispatchResult.Success(0));

            var handler = new MenuHandler(mockController.Object, mockConsole.Object, mockSettings.Object, mockLogger.Object);
            await handler.RunAsync();

            mockController.Verify(x => x.RequestElevator(1, 5, 2), Times.Once);
        }

        [Fact]
        public async Task RunAsync_Option2_ShowElevatorStatuses()
        {
            var mockConsole = new Mock<IConsoleService>();
            var mockController = new Mock<IElevatorController>();
            var mockSettings = new Mock<ISettings>();
            var mockLogger = new Mock<ILogger<MenuHandler>>();

            mockSettings.SetupGet(x => x.MinMenuChoice).Returns(1);
            mockSettings.SetupGet(x => x.MaxMenuChoice).Returns(3);

            mockConsole.SetupSequence(x => x.ReadChoice(1, 3))
                       .Returns(2)  //Show Status
                       .Returns(3); //Exit

            var handler = new MenuHandler(mockController.Object, mockConsole.Object, mockSettings.Object, mockLogger.Object);
            await handler.RunAsync();

            mockConsole.Verify(x => x.ShowElevatorStatuses(null), Times.Once);
        }

        [Fact]
        public async Task RunAsync_Option3_Exit()
        {
            var mockConsole = new Mock<IConsoleService>();
            var mockController = new Mock<IElevatorController>();
            var mockSettings = new Mock<ISettings>();
            var mockLogger = new Mock<ILogger<MenuHandler>>();

            mockSettings.SetupGet(x => x.MinMenuChoice).Returns(1);
            mockSettings.SetupGet(x => x.MaxMenuChoice).Returns(3);

            mockConsole.SetupSequence(x => x.ReadChoice(1, 3)).Returns(3); //Exit

            var handler = new MenuHandler(mockController.Object, mockConsole.Object, mockSettings.Object, mockLogger.Object);
            await handler.RunAsync();

            mockConsole.Verify(x => x.RunMenu(), Times.Once);
        }
    }
}
