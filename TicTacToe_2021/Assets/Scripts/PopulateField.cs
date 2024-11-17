using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopulateField : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;      // Prefab of the tile
    [SerializeField] public GameObject[,] gameField;
    [SerializeField] private Transform parentTransform;  // Parent transform to hold all tiles
    [SerializeField] public int gridWidth = 3;           // Number of columns
    [SerializeField] public int gridHeight = 3;          // Number of rows
    [SerializeField] public int winCondition;            // Number of consecutive same tiles to win
    [SerializeField] private float gameFieldWidth = 5f;  // Width of the game field area
    [SerializeField] private float gameFieldHeight = 5f; // Height of the game field area
    [SerializeField] private float gridMultiplyer;       // Multyplier for tiles identation
    [SerializeField] private float tileSize;       
    [SerializeField] private RectTransform rectTransform; // Get the renderer component attached to the GameObject
    [SerializeField] private Button mainMenuButton; // Go to main menu button
    [SerializeField] private LineRenderer lineRenderer;

    public GameObject[,] GameField
    {
        get { return gameField; }
        set { gameField = value; }
    }


    public static PopulateField _instance;
    public static PopulateField instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PopulateField)) as PopulateField;
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }




    public void Start()
    {
        mainMenuButton.onClick.AddListener(MainMenu);
        rectTransform = GetComponent<RectTransform>();

        gameFieldWidth = rectTransform.rect.width;
        gameFieldHeight = rectTransform.rect.height;
        gameField = new GameObject[(int)gameFieldWidth, (int)gameFieldHeight];
        GenerateGrid();
    }

    private void GenerateGrid()
    {

        gridWidth = GameManager.instance.squareMatrixSize;
        gridHeight = gridWidth;

        if (gridWidth == 3)
        {
            gridMultiplyer = 2;
            winCondition = 3;
        }
        else if (gridWidth == 4)
        {
            gridMultiplyer = 5;
            winCondition = 4;
        }
        else if (gridWidth == 5)
        {
            gridMultiplyer = 8;
            winCondition = 4;
        }
        else if (gridWidth == 9)
        {
            gridMultiplyer = 20;
            winCondition = 5;
        }

        // Calculate the ideal size for each tile based on game field width and height
        float tileWidth = (gameFieldWidth / gridWidth) / 6;
        float tileHeight = (gameFieldHeight / gridHeight) / 6;

        // Use the smaller dimension as the base size for square tiles, with slight padding
        tileSize = Mathf.Min(tileWidth, tileHeight) * 0.9f; // 0.9f factor to add slight padding between tiles

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
                tile.GetComponent<MakeSquareRed>().xPos = x;
                tile.GetComponent<MakeSquareRed>().yPos = y;
                tile.name = "" + x + " " + y;
                gameField[x, y] = tile;
            }
        }
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        // Calculate position based on your logic
        float startX = -((gameFieldWidth / 2) - tileSize / 2);
        float startY = ((gameFieldHeight / 2) - tileSize / 2);

        float posX = startX + x * tileSize;
        float posY = startY - y * tileSize;

        return new Vector3(posX, posY, 0);
    }


    public void HighlightWinningTiles(List<GameObject> winningTiles)
    {
        foreach (GameObject tile in winningTiles)
        {
            // Highlight the tile visually (e.g., change its color)
            tile.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void DrawLine(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0));
        lineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0));
    }
    
    //public void DrawLine(int startX, int startY, int endX, int endY)
    //{
    //    lineRenderer.SetPosition(0, new Vector3(startX, startY, 0));
    //    lineRenderer.SetPosition(1, new Vector3(endX, endY, 0));
    //}





}
























