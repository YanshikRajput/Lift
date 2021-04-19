using LiftProblem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftProblem.Entities
{
    public class Building
    {
        public Floor[] Floors { get; set; }
        public Lift Lift { get; set; }
        public Building(int liftCapacity, int[][] FloorAndPeopleComposition)
        {
            Floors = FloorAndPeopleComposition.Select((floorComposition, floorNumber) =>
                {
                    var floor = new Floor(floorNumber, floorComposition);
                    floor.ButtonPressedForCallingTheLift += this.LiftRequested;
                    return floor;
                }).ToArray();
            Lift = new Lift(liftCapacity);
            Lift.LiftArriverAtAFloor += LiftArrivedAtAFloor;

        }
        public void LiftRequested(Direction direction, int floorNumberRequestedOn)
        {

        }
        public void LiftArrivedAtAFloor(int floorNumber)
        {
            var floor = this.Floors.Single(floor => floor.FloorNumber == floorNumber);
            floor.LiftHasArrived(this.Lift);
        }
    }

}
