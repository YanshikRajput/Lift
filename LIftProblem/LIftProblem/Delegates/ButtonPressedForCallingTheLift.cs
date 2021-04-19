using LiftProblem.Enums;

public delegate void ButtonPressedForCallingTheLift(Direction direction);
public delegate void ButtonPressedForCallingTheLiftOnAGivenFloor(Direction direction, int callingFloor);