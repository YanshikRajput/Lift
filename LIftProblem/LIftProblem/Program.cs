using LiftProblem.Entities;
using System;

namespace LiftProblem
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[][] rawInput = new int[][]
            {
                new int[] {},
                new int[] {6,5,2},
                new int[] {4},
                new int[] {},
                new int[] {0,0,0},
                new int[] {},
                new int[] {},
                new int[] {3,6,4,5,6},
                new int[] {},
                new int[] {1,10,2},
                new int[] {1,4,3,2},

            };
            int liftCapacity = 4;
            var building = new Building(liftCapacity, rawInput);
        }
    }
}
