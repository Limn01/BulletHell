using UnityEngine;
using System.Collections;

public enum Direction
{
    North, East, South, West,
}

public class Corridor 
{
    public int startXPos;
    public int startYPos;
    public int corridoorLength;
    public Direction direction;
	
    public int EndPositionX
    {
        get
        {
            if (direction == Direction.North || direction == Direction.South)
            {
                return startXPos;
            }

            if (direction == Direction.East)
            {
                return startXPos + corridoorLength - 1;
            }

            return startXPos - corridoorLength + 1;
        }
    }

    public int EndPositionY
    {
        get
        {
            if (direction == Direction.East || direction == Direction.West)
            {
                return startYPos;
            }

            if (direction == Direction.North)
            {
                return startYPos + corridoorLength - 1;
            }

            return startYPos - corridoorLength + 1;
        }
    }

    public void SetUpCorridoor(Rooms room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridoor)
    {
        direction = (Direction)Random.Range(0, 4);

        Direction oppositeDirection = (Direction)(((int)room.enteringCorridoor + 2) % 4);

        if (!firstCorridoor && direction == oppositeDirection)
        {
            //Rotate the direction 90 degrees clockwise (North becomes East, east becomes south etc)
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }

        corridoorLength = length.Random;
        //Create a cap for how long the length can be
        int maxLength = length.m_Max;

        switch (direction)
        {
            // if the chosen direction is north (up)
            case Direction.North:
                // The Starting position in the x axis can be at random but within width of the room
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);

                //The Starting position in the y axis must be the top of the room
                startYPos = room.yPos + room.roomHeight;

                // The maximum length the corridoor can be is the height of the board (rows) but from the top of the room(y pos + height)
                maxLength = rows - startYPos - roomHeight.m_Min;
                break;
            case Direction.East:
                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;
            case Direction.South:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.m_Min;
                break;
            case Direction.West:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }

        //Clamp the length of the corridoor to make sure it doesnt go off the board
        corridoorLength = Mathf.Clamp(corridoorLength, 1, maxLength);
    }
}
