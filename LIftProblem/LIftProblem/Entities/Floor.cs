using LiftProblem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftProblem.Entities
{
    public class Floor
    {
        
        public event ButtonPressedForCallingTheLiftOnAGivenFloor ButtonPressedForCallingTheLift;
        public int FloorNumber { get; set; }

        public List<Person> PeopleWaitingForLift { get; set; }

        public List<Person> PeopleBelongToTheFloor { get; set; }

        public Floor(int floorNumber, int[] floorNumbersPeopleWantToGoTo)
        {
            this.FloorNumber = floorNumber;
            this.PeopleWaitingForLift = floorNumbersPeopleWantToGoTo.Select(floorNumberPersonWantToGoTo =>
            {
                var person = new Person(floorNumber, floorNumberPersonWantToGoTo);
                person.ButtonPressed += this.ButtonPress;
                return person;
            }).ToList();
            this.PeopleBelongToTheFloor = new List<Person>();
        }

        public void ButtonPress(Direction direction)
        {
            this.ButtonPressedForCallingTheLift(direction, this.FloorNumber);
        }

        public void LiftHasArrived(Lift lift)
        {
            // Getting the people out of Lift who wanted to get to the current floor
            MovePeopleOutOfTheLift(lift);
            // Getting the people in Lift who want to go in the same direction
            MovePeopleInTheLift(lift);
        }

        private void MovePeopleInTheLift(Lift lift)
        {
            if (this.PeopleWaitingForLift.Count < 1)
            {
                return;
            }

            
            var peopleGoingInSameDirection = this.PeopleWaitingForLift.Where(p => p.DirectionToGoIn == lift.LiftDirection).ToList();
            var availableCapacityOfTheLift = lift.GetAvailableCapacity();

            if (availableCapacityOfTheLift < 1)
            {
                return;
            }

            var numberOfPeopleWhoCanGetIn = availableCapacityOfTheLift;
            if (availableCapacityOfTheLift > peopleGoingInSameDirection.Count)
            {
                numberOfPeopleWhoCanGetIn = peopleGoingInSameDirection.Count;
            }
            var peopleWhoCanGetIn = peopleGoingInSameDirection.GetRange(0, numberOfPeopleWhoCanGetIn);

            lift.People.AddRange(peopleWhoCanGetIn);
            peopleWhoCanGetIn.ForEach(person =>
            {
                person.SetOnboard();
                this.PeopleWaitingForLift.Remove(person);
            });
        }

        private void MovePeopleOutOfTheLift(Lift lift)
        {
            var peopleForCurrentFloor = lift.OffboardPeople(this.FloorNumber);
            if (peopleForCurrentFloor.Count > 0)
            {
                this.PeopleBelongToTheFloor.AddRange(peopleForCurrentFloor);
                peopleForCurrentFloor.ForEach(person =>
                {
                    person.SetReached();
                });
            }
        }
    }
}
