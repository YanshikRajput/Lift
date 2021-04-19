using LiftProblem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftProblem.Entities
{
    public class Person
    {

        public event ButtonPressedForCallingTheLift ButtonPressed;

        public int CurrentFloor { get; set; }
        public int DestinationFloor { get; set; }

        public WaitingStatus WaitingStatus { get; set; }

        public Direction DirectionToGoIn
        {
            get
            {
                return CurrentFloor > DestinationFloor ? Direction.GoingDown : Direction.GoingUp;
            }
        }

        public Person(int currentFloor, int destinationFloor)
        {
            this.CurrentFloor = currentFloor;
            this.DestinationFloor = destinationFloor;
            this.WaitingStatus = WaitingStatus.Waiting;
        }

        public void PressButton()
        {
            this.ButtonPressed(this.DirectionToGoIn);
        }

        public void SetReached()
        {
            this.WaitingStatus = WaitingStatus.Reached;
        }

        public void SetOnboard()
        {
            this.WaitingStatus = WaitingStatus.BoardedLift;
        }
    }
}
