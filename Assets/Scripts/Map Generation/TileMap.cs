using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public TileType[] tileType;

    int[,] tiles;
    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start()
    {
        GenerateMapData();
        GenerateMapVisual();

    }


    void GenerateMapData()
    {
        tiles = new int[mapSizeX, mapSizeY];

        int x, y;

        for ( x = 0; x < mapSizeX; x++)
        {
            for ( y = 0; y < mapSizeX; y++)
            {


                tiles[x, y] = 0;
            }

        }

        for ( x = 3; x <= 5; x++)
        {
            for ( y = 0; y < 4; y++)
            {
                tiles[x, y] = 1;
            }

        }

    tiles[4, 4] = 2;
        tiles[5, 4] = 2;
        tiles[6, 4] = 2;
        tiles[7, 4] = 2;
        tiles[8, 4] = 2;

        tiles[4, 5] = 2;
        tiles[4, 6] = 2;
        tiles[8, 5] = 2;
        tiles[8, 6] = 2;

        
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX;y++)
            {
                TileType tt = tileType[tiles[x, y]];
                Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }
}
