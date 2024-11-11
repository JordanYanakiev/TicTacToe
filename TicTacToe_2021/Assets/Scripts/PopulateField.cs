//using UnityEngine;
//using System;
//using System.Collections;

//public class PopulateField : MonoBehaviour
//{
//    [SerializeField] private GameObject tile;
//    [SerializeField] private GameObject[,] gameField;
//    [SerializeField] private Transform parentTransform;



//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        gameField = new GameObject[3, 3];
//        //float offset = 0;
//        //int widthOffset = 0;

//        for (int width = 0; width < 3; width++)
//        {
//            //widthOffset += 1;
//            //width += widthOffset;
//            for (int height = -1; height < -1+3; height++)
//            {
//                Vector3 tempPosition = new Vector3(width, height /*+ offset*/, 0);
//                //GameObject brick = Instantiate(tile, tempPosition, Quaternion.identity);
//                GameObject brick = Instantiate(tile, parentTransform);

//                brick.transform.position = new Vector3(height, width, -.23f);

//                //offset += .5f;
//            }

//            //width -= widthOffset;
//            //offset = 0;
//        }
//    }




//    // Update is called once per frame
//    void Update()
//    {

//    }
//}






using UnityEngine;

public class PopulateField : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;      // Prefab of the tile
    [SerializeField] private Transform parentTransform;  // Parent transform to hold all tiles
    [SerializeField] private int gridWidth = 3;          // Number of columns
    [SerializeField] private int gridHeight = 3;         // Number of rows
    [SerializeField] private float gameFieldWidth = 5f;  // Width of the game field area
    [SerializeField] private float gameFieldHeight = 5f; // Height of the game field area
    [SerializeField] private float gridMultiplyer;       // Multyplier for tiles identation
    [SerializeField] private RectTransform rectTransform; // Get the renderer component attached to the GameObject

    void Start()
    {
        GenerateGrid();
        rectTransform = GetComponent<RectTransform>();

        gameFieldWidth = rectTransform.rect.width;
        gameFieldHeight = rectTransform.rect.height;
    }

    private void GenerateGrid()
    {
        if (gridWidth == 3)
        {
            gridMultiplyer = 2;
        }
        else if (gridWidth == 4)
        {
            gridMultiplyer = 5;
        }
        else if (gridWidth == 5)
        {
            gridMultiplyer = 8;
        }

        // Calculate the ideal size for each tile based on game field width and height
        float tileWidth = (gameFieldWidth / gridWidth) / 3;
        float tileHeight = (gameFieldHeight / gridHeight) / 3;

        // Use the smaller dimension as the base size for square tiles, with slight padding
        float tileSize = Mathf.Min(tileWidth, tileHeight) * 0.9f; // 0.9f factor to add slight padding between tiles

        // Calculate the starting point to center the grid within the game field
        float startX = -((tileSize * 4) + (tileSize * gridMultiplyer));
        float startY = ((tileSize * 4) + (tileSize * gridMultiplyer));



        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                //// Calculate position for each tile
                float posX = startX + (x + ((tileSize * 6) * x));
                float posY = startY + (y - ((tileSize * 6) * y));
                Vector3 tilePosition = new Vector3(posX, posY, 0);

                // Instantiate the tile, set its position, and scale it
                GameObject tile = Instantiate(tilePrefab, parentTransform);
                tile.transform.localPosition = tilePosition;
                tile.transform.localScale = new Vector3(tileSize, tileSize, 1);
            }
        }
    }
}
























