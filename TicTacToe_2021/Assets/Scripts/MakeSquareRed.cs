using System.Collections.Generic;
using UnityEngine;

public class MakeSquareRed : MonoBehaviour
{
    public GameObject gameFieldPanel;
    public string player;
    public int xPos;
    public int yPos;
    public List<int> xPositions;
    public List<int> yPositions;
    public List<GameObject> tilesInRow;
    public Vector3 start;
    public Vector3 end;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilesInRow = new List<GameObject>();
        xPositions = new List<int>();
        yPositions = new List<int>();
        gameFieldPanel = GameObject.Find("GameFieldPanel");
    }

    public void OnMouseDown()
    {
        if (GameManager.instance.currentPlayer == "X" && this.gameObject.tag == "Untagged") 
        {
            //this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = PopulateField.instance.player1Image;
            this.gameObject.tag = "Player1";
            GameManager.instance.SwitchPlayer(); 
            
            if (!GameManager.instance.IsMultiplayerGame)
            {
                AIPlay();
                //GameManager.instance.SwitchPlayer();
            }
        }
        
        if (GameManager.instance.currentPlayer == "O" && this.gameObject.tag == "Untagged")
        {
            
            if (GameManager.instance.IsMultiplayerGame) 
            { 
            this.gameObject.GetComponent<SpriteRenderer>().sprite = PopulateField.instance.player2Image;
            this.gameObject.tag = "Player2";
            GameManager.instance.SwitchPlayer();
            }
        }

        //Chech all directions for 5 consecutive tiles
        CheckIfWin();
    }

    private void CheckIfWin()
    {
        string playerTag = this.gameObject.tag;
        int x = 0;
        int y = 0;
        
        // Horizontal
        for(int i = 0; i < PopulateField.instance.gridHeight; i++)
        {
            CountTiles(x, i, 1, 0, Color.yellow, playerTag);
        }

        // Vertical
        for (int j = 0; j < PopulateField.instance.gridHeight; j++)
        {
            CountTiles(j, y, 0, 1, Color.green, playerTag);
        }

        // Diagonal top-left to bottom-right
        for (int k = 0; k < PopulateField.instance.gridHeight; k++)
        {
            CountTiles(k, y, 1, 1, Color.blue, playerTag);
        }

        // Diagonal top-left to bottom-right
        for (int l = 0; l < PopulateField.instance.gridHeight; l++)
        {
            CountTiles(0, l, 1, 1, Color.blue, playerTag);
        }

        // Diagonal bottom-left to top-right
        for (int m = 0; m < PopulateField.instance.gridHeight; m++)
        {
            CountTiles(m, PopulateField.instance.gridHeight - 1, 1, -1, Color.cyan, playerTag);
        }
        
        // Diagonal bottom-left to top-right
        for (int n = 0; n < PopulateField.instance.gridHeight; n++)
        {
            CountTiles(0, PopulateField.instance.gridHeight - (1 + n), 1, -1, Color.cyan, playerTag);
        }
    }
    private bool CheckIfWin2()
    {
        string playerTag = this.gameObject.tag;
        int x = 0;
        int y = 0;
        bool aiWins = false;

        // Horizontal
        for(int i = 0; i < PopulateField.instance.gridHeight; i++)
        {
            aiWins = CountTiles2(x, i, 1, 0, Color.yellow, playerTag);
        }

        // Vertical
        for (int j = 0; j < PopulateField.instance.gridHeight; j++)
        {
            aiWins = CountTiles2(j, y, 0, 1, Color.green, playerTag);
        }

        // Diagonal top-left to bottom-right
        for (int k = 0; k < PopulateField.instance.gridHeight; k++)
        {
            aiWins = CountTiles2(k, y, 1, 1, Color.blue, playerTag);
        }

        // Diagonal top-left to bottom-right
        for (int l = 0; l < PopulateField.instance.gridHeight; l++)
        {
            aiWins = CountTiles2(0, l, 1, 1, Color.blue, playerTag);
        }

        // Diagonal bottom-left to top-right
        for (int m = 0; m < PopulateField.instance.gridHeight; m++)
        {
            aiWins = CountTiles2(m, PopulateField.instance.gridHeight - 1, 1, -1, Color.cyan, playerTag);
        }
        
        // Diagonal bottom-left to top-right
        for (int n = 0; n < PopulateField.instance.gridHeight; n++)
        {
            aiWins = CountTiles2(0, PopulateField.instance.gridHeight - (1 + n), 1, -1, Color.cyan, playerTag);
        }

        return aiWins;
    }

    private void CountTiles(int startX, int startY, int dx, int dy, Color color, string playerTag)
    {
        int count = 0;
        int x = startX;
        int y = startY;
        start = new Vector3(0, 0, 0);
        end = new Vector3(0, 0, 0);

        while (x >= 0 && x < PopulateField.instance.gridWidth && y >= 0 && y < PopulateField.instance.gridHeight)
        {
            GameObject temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];

            tilesInRow.Add(temp);

            count++;
            x += dx;
            y += dy;
        }

        int counter = 0;

        foreach(GameObject tile in tilesInRow)
        {
            if (tile.tag == playerTag)
            {
                counter++;
                if(counter == 1)
                {
                    start = tile.transform.position;
                }
                else if (counter > 1)
                {
                    end = tile.transform.position;
                }

                if (counter == PopulateField.instance.winCondition)
                {
                    Debug.Log("Player WON!");
                    PopulateField.instance.DrawLine(start, end);
                    GameManager.instance.EndGame(playerTag);
                }
            }
            else if (tile.tag != "Player")
            {
                counter = 0;
            }
        }
        tilesInRow.Clear();
    }
    private bool CountTiles2(int startX, int startY, int dx, int dy, Color color, string playerTag)
    {
        int count = 0;
        int x = startX;
        int y = startY;
        start = new Vector3(0, 0, 0);
        end = new Vector3(0, 0, 0);

        while (x >= 0 && x < PopulateField.instance.gridWidth && y >= 0 && y < PopulateField.instance.gridHeight)
        {
            GameObject temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];

            tilesInRow.Add(temp);

            count++;
            x += dx;
            y += dy;
        }

        int counter = 0;

        foreach(GameObject tile in tilesInRow)
        {
            if (tile.tag == playerTag)
            {
                counter++;
                if(counter == 1)
                {
                    start = tile.transform.position;
                }
                else if (counter > 1)
                {
                    end = tile.transform.position;
                }

                if (counter == PopulateField.instance.winCondition)
                {
                    //Debug.Log("Player WON!");
                    //PopulateField.instance.DrawLine(start, end);
                    //GameManager.instance.EndGame(playerTag);
                    return true;
                }
                return false;
            }
            else if (tile.tag != "Player")
            {
                counter = 0;
            }
        }
        tilesInRow.Clear();
        return false;
    }


    // =================== AI ========================


    private void AIPlay()
    {
        //// Check if AI can win
        //if (TryToCompleteWin("Player2")) return;

        // Check if AI can block Player1 from winning
        if (TryToBlockOpponent("Player1")) return;

        // Otherwise, play strategically
        PlayStrategically();
    }

    private bool TryToCompleteWin(string playerTag)
    {
        return TryMakeStrategicMove(playerTag, PopulateField.instance.winCondition - 1);
    }

    private bool TryToBlockOpponent(string opponentTag)
    {
        return TryMakeStrategicMove(opponentTag, PopulateField.instance.winCondition - 1);
    }

    private bool TryMakeStrategicMove(string targetTag, int neededInRow)
    {
        for (int x = 0; x < PopulateField.instance.gridWidth; x++)
        {
            for (int y = 0; y < PopulateField.instance.gridHeight; y++)
            {
                GameObject tile = PopulateField.instance.GameField[x, y];

                // Skip tiles that are already taken
                if (tile.tag == "Player1" || tile.tag == "Player2") continue;

                // Temporarily tag the tile and check if it creates a win/block condition
                string originalTag = tile.tag;
                tile.tag = targetTag;

                CheckIfWin();


                if (CheckWinCondition(targetTag, x, y, neededInRow))
                {
                    PlaceTileAsAI(tile);
                    return true;
                }

                // Revert the tag after checking
                tile.tag = originalTag;
            }
        }

        return false;
    }

    private void PlayStrategically()
    {
        // Try to play the center if available
        int center = PopulateField.instance.gridWidth / 2;
        GameObject centerTile = PopulateField.instance.GameField[center, center];

        if (centerTile.tag != "Player1" && centerTile.tag != "Player2")
        {
            PlaceTileAsAI(centerTile);
            return;
        }

        // Otherwise, play the first available tile
        for (int x = 0; x < PopulateField.instance.gridWidth; x++)
        {
            for (int y = 0; y < PopulateField.instance.gridHeight; y++)
            {
                GameObject tile = PopulateField.instance.GameField[x, y];

                if (tile.tag != "Player1" && tile.tag != "Player2")
                {
                    PlaceTileAsAI(tile);
                    return;
                }
            }
        }
    }

    private bool CheckWinCondition(string playerTag, int startX, int startY, int neededInRow)
    {
        // Check horizontal, vertical, and diagonal directions
        return CheckDirection(startX, startY, 1, 0, playerTag, neededInRow) || // Horizontal
               CheckDirection(startX, startY, 0, 1, playerTag, neededInRow) || // Vertical
               CheckDirection(startX, startY, 1, 1, playerTag, neededInRow) || // Diagonal TL-BR
               CheckDirection(startX, startY, 1, -1, playerTag, neededInRow);  // Diagonal BL-TR
    }

    private bool CheckDirection(int startX, int startY, int dx, int dy, string playerTag, int neededInRow)
    {
        int count = 0;

        for (int i = -neededInRow; i <= neededInRow; i++)
        {
            int x = startX + i * dx;
            int y = startY + i * dy;

            if (x >= 0 && x < PopulateField.instance.gridWidth && y >= 0 && y < PopulateField.instance.gridHeight)
            {
                GameObject tile = PopulateField.instance.GameField[x, y];
                if (tile.tag == playerTag)
                {
                    count++;
                    if (count > neededInRow) return true;
                }
                else
                {
                    count = 0;
                }
            }
        }

        return false;
    }

    private void PlaceTileAsAI(GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().sprite = PopulateField.instance.player2Image;
        tile.tag = "Player2";
        CheckIfWin();
        GameManager.instance.SwitchPlayer();
    }






}
