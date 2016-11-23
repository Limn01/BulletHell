using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public Transform parent;

    public GameObject[] tiles;
    public GameObject wall;
    public GameObject player;

    public List<Vector3> createdTiles;

    public int tileAmount;
    public int tileSize;

    public float waitTime;
    public float chanceUp;
    public float chanceRight;
    public float chanceDown;

    // Wall Generate
    public float minY = 9999999;
    public float maxY = 0f;
    public float minX = 9999999;
    public float maxX = 0f;
    public float xAmount;
    public float yAmount;
    public float extraWallX;
    public float extraWallY;
    

	// Use this for initialization
	void Start ()
    {
        parent = new GameObject().transform;
        parent.name = "LevelParent";
        StartCoroutine(GenerateLevel());
	}
	
    IEnumerator GenerateLevel()
    {
        for (int i = 0; i < tileAmount; i++)
        {
            float direction = Random.Range(0f, 1f);
            int tile = Random.Range(0, tiles.Length);

            CreateTile(tile);
            CallMoveGen(direction);

            yield return new WaitForSeconds(waitTime);

            if (i == tileAmount - 1)
            {
                Finish();

            }
        }

        yield return 0;
    }

    void CallMoveGen(float randomDir)
    {
        if (randomDir < chanceUp)
        {
            MoveGenerator(0);
        }

        else if (randomDir < chanceRight)
        {
            MoveGenerator(1);

        }

        else if (randomDir < chanceDown)
        {
            MoveGenerator(2);
        }

        else
        {
            MoveGenerator(3);
        }
    }

    void MoveGenerator(int dir)
    {
        switch (dir)
        {
            case 0:
                transform.position = new Vector3(transform.position.x, transform.position.y + tileSize, 0);
                break;

            case 1:
                transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, 0);
                break;

            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y - tileSize, 0);
                break;

            case 3:
                transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0);
                break;
        }
    }

    void CreateTile(int tileIndex)
    {
        if (!createdTiles.Contains(transform.position))
        {
            GameObject tileObject;

            tileObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

            createdTiles.Add(tileObject.transform.position);

            tileObject.transform.parent = parent;
        }

        else
        {
            tileAmount++;
        }

        
    }

    void Finish()
    {
        CreateWallValues();
        CreateWall();
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Instantiate(player, createdTiles[Random.Range(0, createdTiles.Count)], Quaternion.identity);

        //for (int i = 0; i < createdTiles.Count; i++)
        //{
        //
        //}
    }

    void CreateWallValues()
    {
        for (int i = 0; i < createdTiles.Count; i++)
        {
            if (createdTiles[i].y < minY)
            {
                minY = createdTiles[i].y;

            }

            if (createdTiles[i].y > maxY)
            {
                maxY = createdTiles[i].y;
            }

            if (createdTiles[i].x < minX)
            {
                minX = createdTiles[i].x;
            }

            if (createdTiles[i].x > maxX)
            {
                maxX = createdTiles[i].x;
            }

            xAmount = ((maxX - minX) / tileSize) + extraWallX;
            yAmount = ((maxY - minY) / tileSize) + extraWallY;
        }
    }

    void CreateWall()
    {
        for (int x = 0; x < xAmount; x++)
        {
            for (int y = 0; y < yAmount; y++)
            {
                if (!createdTiles.Contains(new Vector3((minX - (extraWallX * tileSize) / 2) + (x * tileSize), (minY - (extraWallY * tileSize) / 2) + (y * tileSize))))
                {
                    GameObject wallObj = (GameObject)Instantiate(wall, new Vector3((minX - (extraWallX * tileSize) / 2) + (x * tileSize), (minY - (extraWallY * tileSize) / 2) + (y * tileSize)), transform.rotation);
                    wallObj.transform.parent = parent;
                }
            }
        }
    }
}
