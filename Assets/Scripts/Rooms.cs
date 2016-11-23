using UnityEngine;
using System.Collections;

public class Rooms
{
    public int xPos;
    public int yPos;
    public int roomWidth;
    public int roomHeight;
    public Direction enteringCorridoor;

	public void SetUpRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        // Set a Random Width and Height
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        // Set the x and y coordinates so the room is roughly in the middle of the board 
        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
        yPos = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);
    }

    public void SetUpRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridoor)
    {
        // Set the entering corridoor direction
        enteringCorridoor = corridoor.direction;

        // Set random values for width and height
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        switch (corridoor.direction)
        {
            case Direction.North:

                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridoor.EndPositionY);

                yPos = corridoor.EndPositionY;

                xPos = Random.Range(corridoor.EndPositionX - roomWidth + 1, corridoor.EndPositionX);

                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;

            case Direction.East:

                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridoor.EndPositionX);
                xPos = corridoor.EndPositionX;

                yPos = Random.Range(corridoor.EndPositionY - roomHeight + 1, corridoor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;

            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridoor.EndPositionY);
                yPos = corridoor.EndPositionY - roomHeight + 1;

                xPos = Random.Range(corridoor.EndPositionX - roomWidth + 1, corridoor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;

            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridoor.EndPositionX);
                xPos = corridoor.EndPositionX - roomWidth + 1;

                yPos = Random.Range(corridoor.EndPositionY - roomHeight + 1, corridoor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
        }
    }
}
