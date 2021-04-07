
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{

    [SerializeField]
    private Tile groundTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile topWallTile;
    [SerializeField]
    private Tile botWallTile;
    [SerializeField]
    private Tile treasureTile;
    [SerializeField]
    private Tile bossTile;
    [SerializeField]
    private Tile bossBackgroundTile;
    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap pitMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private Tilemap addMap;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;
    

    private int routeCount = 0;

    private void Start() {
        bossBackgroundTile.name = "bossBackground";
        groundTile.name = "ground";

        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        FillWalls();
        addFeatures();
    }

    private void addFeatures() {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin; xMap <= bounds.xMax; xMap++) {
            for (int yMap = bounds.yMin; yMap <= bounds.yMax; yMap++) {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                TileBase tileAt = groundMap.GetTile(pos);
                if (tileAt != null && Random.Range(1,100) < 2 && tileAt.name != "bossBackground") {
                    addMap.SetTile(pos, treasureTile);
                }
            }
        }
    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                if (tile == null) {
                    pitMap.SetTile(pos, pitTile);
                    if (tileBelow != null) {
                        wallMap.SetTile(pos, topWallTile);
                    }
                    else if (tileAbove != null) {
                        wallMap.SetTile(pos, botWallTile);
                    }
                }
            }
        }
    }

    bool bossExist = false;

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes) {
            routeCount++;
            while (++routeLength < maxRouteLength) {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3
                int roomSize = 1; //Hallway size
                if (Random.Range(1, 100) <= roomRate)
                    roomSize = Random.Range(3, 12);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= deviationRate) {
                    if (routeUsed) {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= deviationRate) {
                    if (routeUsed) {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= deviationRate) {
                    if (routeUsed) {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else {
                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed) {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
            if (!bossExist) {
                Debug.Log("Generating room");
                bossExist = true;
                GenerateBossRoom(x, y);
            }
        }
    }

    private void GenerateSquare(int x, int y, int radius) {
        for (int tileX = x - radius; tileX <= x + radius; tileX++) {
            for (int tileY = y - radius; tileY <= y + radius; tileY++) {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, groundTile);
            }
        }
    }

    private void GenerateBossRoom(int x, int y) {
        int radius = 10;
        for (int tileX = x - radius; tileX <= x + radius; tileX++) {
            for (int tileY = y - radius; tileY <= y + radius; tileY++) {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, bossBackgroundTile);
            }
        }
        Vector3Int centerPos = new Vector3Int(x, y, 0);
        addMap.SetTile(centerPos, bossTile);
    }
}