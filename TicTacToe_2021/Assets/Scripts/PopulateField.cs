using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopulateField : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;      // Prefab of the tile
    [SerializeField] public GameObject[,] gameField;
    [SerializeField] private Transform parentTransform;  // Parent transform to hold all tiles
    [SerializeField] private int gridWidth = 3;          // Number of columns
    [SerializeField] private int gridHeight = 3;         // Number of rows
    [SerializeField] private float gameFieldWidth = 5f;  // Width of the game field area
    [SerializeField] private float gameFieldHeight = 5f; // Height of the game field area
    [SerializeField] private float gridMultiplyer;       // Multyplier for tiles identation
    [SerializeField] private float tileSize;       
    [SerializeField] private RectTransform rectTransform; // Get the renderer component attached to the GameObject
    [SerializeField] private Button mainMenuButton; // Go to main menu button

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




    void Start()
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
        }
        else if (gridWidth == 4)
        {
            gridMultiplyer = 5;
        }
        else if (gridWidth == 5)
        {
            gridMultiplyer = 8;
        }
        else if (gridWidth == 9)
        {
            gridMultiplyer = 20;
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

    public bool CheckWinCondition(int x, int y, string player)
    {
        // Check all directions: horizontal, vertical, diagonal 1, diagonal 2
        return CheckDirection(x, y, player, 1, 0) ||  // Horizontal
               CheckDirection(x, y, player, 0, 1) ||  // Vertical
               CheckDirection(x, y, player, 1, 1) ||  // Diagonal top-left to bottom-right
               CheckDirection(x, y, player, 1, -1);   // Diagonal bottom-left to top-right
    }

    private bool CheckDirection(int x, int y, string player, int dx, int dy)
    {
        int count = 1;
        count += CountTiles(x, y, player, dx, dy);  // Check in the positive direction
        count += CountTiles(x, y, player, -dx, -dy);  // Check in the negative direction

        if(count == 3)
        {
            Debug.Log("You win!");
        } 


        return count >= 3;  // Or change to 4, 5 based on desired grid size
    }

    private int CountTiles(int startX, int startY, string player, int dx, int dy)
    {
        int count = 0;
        int x = startX + dx;
        int y = startY + dy;

        // Check within the grid bounds and match the player symbol
        while (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight &&
               gameField[x, y] != null &&
               gameField[x, y].GetComponent<MakeSquareRed>().player == player)
        {
            count++;
            x += dx;
            y += dy;
        }

        return count;
    }



}
























