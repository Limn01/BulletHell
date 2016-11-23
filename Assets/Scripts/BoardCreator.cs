using UnityEngine;
using System.Collections;

public class BoardCreator : MonoBehaviour
{
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 50;
    public int rows = 50;
    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridoorlength = new IntRange(6, 10);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject player;

    TileType[][] tiles;
    Rooms[] rooms;
    Corridor[] corridors;
    GameObject boardHolder;

	// Use this for initialization
	void Start ()
    {
        // creat the board holder
        boardHolder = new GameObject("BoardHolder");

        SetUpTileArray();

        CreateRoomsAndCorridors();

        SetTileValuesForRooms();
        SetTileValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();
	}
	
	void SetUpTileArray()
    {
        // Set the tiles jagged array to the correct width
        tiles = new TileType[columns][];

        // Go through all the tile array
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors()
    {
        // Creat rooms array with a random size
        rooms = new Rooms[numRooms.Random];

        // Ther Should be one less corridor than rooms
        corridors = new Corridor[rooms.Length - 1];

        // Create the first room and corrodor
        rooms[0] = new Rooms();
        corridors[0] = new Corridor();

        // Setup the first room
        rooms[0].SetUpRoom(roomWidth, roomHeight, columns, rows);

        // Set up the first corridor using the first room
        corridors[0].SetUpCorridoor(rooms[0], corridoorlength, roomWidth, roomHeight, columns, rows, true);

        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i] = new Rooms();

            // Set upm the room based on the first corridor
            rooms[i].SetUpRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            // if we havnt reached the end of the corridors array
            if (i < corridors.Length)
            {
                // create a corridor
                corridors[i] = new Corridor();

                // Set up the coridor based on the room just created 
                corridors[i].SetUpCorridoor(rooms[i], corridoorlength, roomWidth, roomHeight, columns, rows, false);
            }

            if (i == rooms.Length * .5f)
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
                Instantiate(player, playerPos, Quaternion.identity);
            }
        }
    }

    void SetTileValuesForRooms()
    {
        // go Through all the rooms
        for (int i = 0; i < rooms.Length; i++)
        {
            Rooms currentRoom = rooms[i];

            // For Ech room go through its width
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                // for each horizontal tile go up vertically rooms height
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCorrd = currentRoom.yPos + k;

                    // The corrdinates in the jagged array are based on the rooms position and width and height
                    tiles[xCoord][yCorrd] = TileType.Floor;
                }
            }
        }
    }

    void SetTileValuesForCorridors()
    {
        // go through every corridor
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            // go through its length
            for (int j = 0; j < currentCorridor.corridoorLength; j++)
            {
                // start the coordinates at the start of the corridor
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction add or subtract from the appropriate 
                // coordinates  based on how far through the length the loop is
                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;

                    case Direction.East:
                        xCoord += j;
                        break;

                    case Direction.South:
                        yCoord -= j;
                        break;

                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                // Set these tiles the coordinates of the floor
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }

    void InstantiateTiles()
    {
        // Go throught all the tiles in the jagged array
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                InstantiateFromArray(floorTiles,i,j);

                if (tiles[i][j] ==  TileType.Wall)
                {
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }

    void InstantiateOuterWalls()
    {
        // The Outer walls are one unit left, right, up and down from the board
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both verticlas walls
        InstantiateVerticleOuterWalls(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticleOuterWalls(rightEdgeX, bottomEdgeY, topEdgeY);

        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }

    void InstantiateVerticleOuterWalls(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting loop for y
        float currentY = startingY;

        // While the value for y is less than the end value
        while (currentY <= endingY)
        {
            // Instantiate an outer wall tile at the x coordinate and the current y coordinate
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }

    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // start the loop starting value for x
        float currentX = startingX;

        while (currentX <= endingX)
        {
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }

    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Creat a random index for the array
        int randomIndex = Random.Range(0, prefabs.Length);

        // the position to be instantiated at is the base on the coordinates
        Vector3 pos = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array
        GameObject tileInstance = Instantiate(prefabs[randomIndex], pos, Quaternion.identity) as GameObject;

        // Set the tiles parent to the board holder
        tileInstance.transform.position = boardHolder.transform.position;
    }
}
