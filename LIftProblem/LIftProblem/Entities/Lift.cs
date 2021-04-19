using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiftProblem.Enums;

namespace LiftProblem.Entities
{
    public class Lift
    {
        #region "Events"

        public event LiftArrivedAtAFloor LiftArriverAtAFloor;

        #endregion

        #region "Data"
        public int Capacity { get; }
        public List<Person> People { get; set; }
        public int CurrentFloor { get; set; }
        public Direction LiftDirection { get; set; }

        #endregion

        public Lift(int capacity)
        {
            this.CurrentFloor = 0;
            this.Capacity = capacity;
            this.LiftDirection = Direction.Stationary;
        }

        public void Start()
        {
            this.LiftDirection = Direction.GoingUp;
            this.LiftArriverAtAFloor(this.CurrentFloor);
        }

        public void OnboardPeople(List<Person> people)
        {
            this.People.AddRange(people);
        }

        public List<Person> OffboardPeople(int floorNumber)
        {
            var peopleToOffboard = this.People.Where(p => p.DestinationFloor == floorNumber).ToList();
            this.People = this.People.Where(p => p.DestinationFloor != floorNumber).ToList();
            return peopleToOffboard;
        }

        public int GetAvailableCapacity()
        {
            return this.Capacity - this.People.Count;
        }

        private void MoveUp()
        {
            if (this.LiftDirection == Direction.GoingUp && this.CurrentFloor != topFloor)
            {
                var NextFloor = CurrentFloor;
                this.People.ForEach(person =>
                {
                    if (person.DirectionToGoIn == this.LiftDirection)
                    {
                        NextFloor = person.DestinationFloor;
                        LiftOperations.LiftArrivedAtAFloor(CurrentFloor);

                        this.CurrentFloor = NextFloor;
                    }
                });


            }
            else if (this.CurrentFloor == topFloor)
            {
                if (this.People.Count > 1)
                    MoveDown();
                else
                    CurrentFloor = GroundFloor;
            }
        }

        private void MoveDown()
        {
            if (this.LiftDirection == Direction.GoingDown && this.CurrentFloor != GroundFloor)
            {
                var NextFloor = CurrentFloor;
                this.People.ForEach(person =>
                {
                    if (person.DirectionToGoIn == this.LiftDirection)
                    {
                        NextFloor = person.DestinationFloor;
                        LiftOperations.LiftArrivedAtAFloor(CurrentFloor);
                        this.CurrentFloor = NextFloor;
                    }
                });

                this.CurrentFloor = NextFloor;

            }
            else if (this.CurrentFloor == GroundFloor)
            {
                if (this.People.Count > 1)
                    MoveUp();
            }
        }
    }
}