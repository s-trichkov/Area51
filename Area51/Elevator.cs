using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Area51
{
    public class Elevator
    {
        public Floor currentFloor = Floor.G;
        Agent currentAgent;
        Floor destinationFloor;
        volatile bool isDoorOpen = false;
        volatile bool hasDestination = false;
        object lockObject = new Object();

        public void CallElevator(Floor floor)
        {
            if (!hasDestination && currentAgent == null)
            {
                destinationFloor = floor;
                hasDestination = true;
            }

        }

        public void ElevatorCycle()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (hasDestination)
                {
                    currentFloor = destinationFloor;
                    hasDestination = false;
                    currentAgent?.ExitElevator(currentFloor);
                    currentAgent = null;
                    isDoorOpen = true;
                }
            }

        }

        public void EnterElevator(Agent agent, Floor floor)
        {
            if (isDoorOpen && agent.currentFloor == currentFloor)
            {
                lock (lockObject)
                {
                    currentAgent = agent;
                    agent.isInElevator = true;
                    isDoorOpen = false;
                    destinationFloor = floor;
                    hasDestination = true;

                    Console.WriteLine($"{agent.Name} enters elevator");

                }
            }
        }
    }
}
