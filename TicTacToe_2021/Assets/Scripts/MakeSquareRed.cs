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
        if (GameManager.instance.currentPlayer == "X") 
        {
            //this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = PopulateField.instance.player1Image;
            this.gameObject.tag = "Player1";
        }
        
        if (GameManager.instance.currentPlayer == "O")
        {
            //this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = PopulateField.instance.player2Image;
            this.gameObject.tag = "Player2";
        }

        //Chech all directions for 5 consecutive tiles
        CheckIfWin();
        GameManager.instance.SwitchPlayer();
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
}
