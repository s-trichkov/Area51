using System;
using System.Threading;

namespace Area51
{
    class Program
    {
        static void Main(string[] args)
        {
            Elevator elevator = new Elevator();
            Agent a1 = new Agent("Agenet001", elevator, Floor.G);
            Agent a2 = new Agent("Agenet002", elevator, Floor.G);
            Agent a3 = new Agent("Agenet003", elevator, Floor.G);
            Agent a4 = new Agent("Agenet004", elevator, Floor.G);

            Thread t1 = new Thread(elevator.ElevatorCycle);
            Thread t2 = new Thread(a1.AgentCycle);
            Thread t3 = new Thread(a2.AgentCycle);
            Thread t4 = new Thread(a3.AgentCycle);
            Thread t5 = new Thread(a4.AgentCycle);

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

            t1.Join();

        }
    }
}
