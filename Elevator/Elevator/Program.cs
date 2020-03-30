using System;
using System.Threading;

namespace Elevator
{
    class Program
    {
        private const string QUIT = "q";
        static void Main(string[] args)
        {
        
        Start:
            
            Console.WriteLine("How tall is the building that this elevator will be in?");

            int floor; string floorInput; Elevator elevator;

            floorInput = Console.ReadLine();

            if (Int32.TryParse(floorInput, out floor))
                elevator = new Elevator(floor);
            else
            {
                Console.WriteLine("That' doesn't make sense...");
                Console.Beep();
                Thread.Sleep(2000);
                Console.Clear();
                goto Start;
            }
            string input = string.Empty;

            while (input != QUIT)
            {
                Console.WriteLine("Please press which floor you would like to go to");

                input = Console.ReadLine();
                if (Int32.TryParse(input, out floor))
                    elevator.FloorPress(floor);
                else if (input == QUIT)
                    Console.WriteLine("GoodBye!");
                else
                    Console.WriteLine("You have pressed an incorrect floor, Please try again");
            }
        }

        public class Elevator
        {
            // Declarations
            // Suppose building has n floors

            private bool[] floorReady;
            public int CurrentFloor = 0;
            private int topfloor;
            public ElevatorStatus Status = ElevatorStatus.STOPPED;

            public Elevator(int NumberOfFloors = 10)
            {
                floorReady = new bool[NumberOfFloors + 1];
                topfloor = NumberOfFloors;
            }

            private void Stop(int floor)
            {
                Status = ElevatorStatus.STOPPED;
                CurrentFloor = floor;
                floorReady[floor] = false;
                Console.WriteLine("Stopped at floor {0}", floor);
            }

            private void Descend(int floor)
            {
                for (int i = CurrentFloor; i >= 0; i--)
                {
                    if (floorReady[i])
                        Stop(floor);
                    else
                        continue;
                }

                Status = ElevatorStatus.STOPPED;
                Console.WriteLine("Waiting..");
            }

            private void Ascend(int floor)
            {
                for (int i = CurrentFloor; i <= topfloor; i++)
                {
                    if (floorReady[i])
                        Stop(floor);
                    else
                        continue;
                }

                Status = ElevatorStatus.STOPPED;
                Console.WriteLine("Waiting..");
            }

            void StayPut()
            {
                Console.WriteLine("That's our Ground floor");
            }

            public void FloorPress(int floor)
            {
                if (floor > topfloor)
                {
                    Console.WriteLine("We only have {0} floors", topfloor);
                    return;
                }

                floorReady[floor] = true;

                switch (Status)
                {

                    case ElevatorStatus.DOWN:
                        Descend(floor);
                        break;

                    case ElevatorStatus.STOPPED:
                        if (CurrentFloor < floor)
                            Ascend(floor);
                        else if (CurrentFloor == floor)
                            StayPut();
                        else
                            Descend(floor);
                        break;

                    case ElevatorStatus.UP:
                        Ascend(floor);
                        break;

                    default:
                        break;
                }


            }

            public enum ElevatorStatus
            {
                UP,
                STOPPED,
                DOWN
            }






        }

    }
}
    
