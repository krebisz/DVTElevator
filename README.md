# DVTElevator



ABOUT
* This is a Console Application written in .Net 9 and C# that is meant to simulate a multi-floor, multi-elevator system within a building.
* It's flow is simple, and provides the user with two choices, besides exiting the application.
* An elevator can be called to a floor with a number of passengers, or the status of the elevators can be viewed. 
* Every call to show the Elevator statuses, these are updated as if a tick or step-wise action in real time is being undertaken.

<BR>
<BR>

CONFIGURATION
* A Config Section from the appsettings.json file is read and mapped to ElevatorConfiguration. It containes the customizable elevator configuration details.
* The configuration allows for multiplle Elevators, with different starting floors and passenger capacities. 
* Coding things like elevator speeds, passenger types (to facilitate elevator restrictions and type of elevator perhaps) were not considered at this time.
* A Settings class within the DVTElevator.UI project has the static boundary values for Menu and Floor options.
* The hard-set boundaries apply to all users (no difference in user inputs), and the floor boundaries are the same for all elevators.
* The Dependency Injection is there to provide the flexibility to change or adapt many of these set ups though.

<BR>
<BR>

UI
* A looping Console Menu, until the user quits. Startup project is DVTElevator.UI.
* Option 2. in the Main Menu returns the current status of all the elevators after a status update has been made for each of them. Each call to this method moves along the elevator in motion, and displays their newly updated status, simulating floor to floor movement.
* An additonal option (No. 4) was to be added to simulate the movement of an elevator in real-time by creating a loop that repeatedly calls the ShowElevatorStatus method, until all elevators go idle.
* Due to time constraints, this was not implemented. Rather a call to either show the Statuses, or to move the elevators, or make a different menu option provides step-by-step movement of the elevators.
* Option 4: Would simulate the movement of an elevator in real-time, by creating a loop repeatedly calling the ShowElevatorStatus method, until all elevators go idle. A separate, event-driven thread could be used to receive user input from the console from the simulatede movement of the elevators in order to handle both simultaneously, in real-time.	

<BR>
<BR>

CONTROLLER
* An additional limitation occurs on the calling of Elevators if all of them are in operation. 
* A queue implementation could be added, and used by the RequestElevator method within the ElevatorController class when the elevator check fails for the ElevatorDispatchResult.
* (Not sure if any systems in real buildings do this but I'd like to find out.)

<BR>
<BR>

TESTS
* Located in DVTElevator.Tests project. Uses XUnit and Moq. 
* Support for Code Coverage via coverlet.collector is available. Chosen to not implement now due to time constraints.
* Tests are run when committed to GitHub as part of a Workflow

<BR>
<BR>

CONTINUOUS INTEGRATION 
* A ci.yml file exists to signal that both a build and a run of the unit tests is to be conducted. 
* This is against the development branch, which if successful, will allow for a pull request into the main/master branch to be made
* Due to GitHub licensing issues, restrictions are placed on push restrictions. The aim was to prevent access to main/master altogether
* Further work would be required to fully automate the merging of development into master after the checks are successful (even though auto-merge is enabled)
* Some fine tuning could also be done to control the branching strategy even further for different users, and feature sets.
* Since I don't have a public space within which to host the console app, no deployment aspects were considered here		
