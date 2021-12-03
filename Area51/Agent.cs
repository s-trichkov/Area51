using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Area51
{
    public class Agent
    {
        Random rand = new Random();
        public AccessLevel accessLevel;
        Elevator elevator;
        public Floor currentFloor = Floor.G;
        public string Name { get; set; }
        public volatile bool isInElevator = false;


        public Agent(string name, Elevator elevator, Floor floor)
        {
            int x = rand.Next(1, 10);
            if (x <= 9) this.accessLevel = AccessLevel.TopSecret;
            if (x <= 6) this.accessLevel = AccessLevel.Secret;
            if (x <= 3) this.accessLevel = AccessLevel.Confidential;
            this.Name = name;
            this.elevator = elevator;
            this.currentFloor = floor;
            Console.WriteLine($"{this.Name} with access level {this.accessLevel} is here to use the elevator");
        }

        public void AgentCycle()
        {
            while (true)
            {
                if (isInElevator)
                {
                    continue;
                }
                int action = rand.Next(0, 3);
                switch (action)
                {
                    case 0:
                        elevator.CallElevator(currentFloor);
                        Console.WriteLine($"{this.Name} called the elevator to floor {this.currentFloor}");
                        Thread.Sleep(1000);
                        break;
                    case 1:
                        Thread.Sleep(1000);
                        break;
                    case 2:
                        if (elevator.currentFloor == currentFloor)
                        {
                            Floor destination = currentFloor;
                            while (destination == currentFloor)
                            {
                                destination = (Floor)rand.Next(0, 4);
                            }
                            elevator.EnterElevator(this, destination);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void ExitElevator(Floor floor)
        {
            currentFloor = floor;
            isInElevator = false;
            Console.WriteLine($"{Name} exits the elevator on floor {floor}");
        }

        public Floor SelectFloor()
        {
            Floor destination = currentFloor;
            while (destination == currentFloor)
            {
                destination = (Floor)rand.Next(0, 4);
            }
            return destination;
        }
    }
}
