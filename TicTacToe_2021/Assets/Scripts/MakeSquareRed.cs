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
        this.gameObject.tag = "Player";
        if (this.gameObject.GetComponent<SpriteRenderer>().color != Color.red) 
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red; 
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }

        CheckIfWin();

        //Chech both diagonals for 5 consecutive tiles
        //CheckDiagonalRight();
        //CheckDiagonalLeft();
    }

    private void CheckDiagonalRight()
    {
        xPositions.Clear();
        yPositions.Clear();
        GameObject tile = gameFieldPanel.GetComponent<PopulateField>().GameField[4, 0];
        tile.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameObject temp = null;
        int x = 4;
        int xStart = x;
        int y = 0;
        int yStart = y;
        xPositions.Add(x);
        yPositions.Add(y);

        while (x <= 8)
        {
            while (x >= 0)
            {
                temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];
                temp.GetComponent<SpriteRenderer>().color = Color.cyan;
                if (temp.tag == "Player")
                    temp.GetComponent<SpriteRenderer>().color = Color.yellow;

                if (tile.tag == temp.tag && temp.tag == "Player")
                {
                    xPositions.Add(x);
                    yPositions.Add(y);
                    if (xPositions.Count == 5)
                    {
                        Debug.Log("YOU WON!");
                        xPositions.Clear();
                        yPositions.Clear();
                    }
                    tile = temp;
                }
                else if (tile.tag != temp.tag)
                {
                    xPositions.Clear();
                    yPositions.Clear();
                    tile = temp;
                }

                if (xPositions.Count == 5)
                {
                    Debug.Log("YOU WON!");
                    xPositions.Clear();
                    yPositions.Clear();
                }
                else if (xPositions.Count != 5)
                {
                    xPositions.Clear();
                    yPositions.Clear();
                }
                //Debug.Log(x + " " + y);
                x--;
                y++;
            }
            x = xStart + 1;
            xStart = x;
            y = 0;
        }


        x = 8;
        xStart = x;
        yStart = 1;
        y = yStart;
        int end = 1;

        while (y < 5)
        {
            while (x >= end)
            {
                temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];
                temp.GetComponent<SpriteRenderer>().color = Color.cyan;
                if (temp.tag == "Player")
                    temp.GetComponent<SpriteRenderer>().color = Color.yellow;
                //Debug.Log(x + " " + y);
                x--;
                y++;
            }
            end++;
            x = 8;
            y = yStart + 1;
            yStart = y;
        }

    }

    private void CheckDiagonalLeft()
    {
        GameObject tile = gameFieldPanel.GetComponent<PopulateField>().GameField[0, 4];
        tile.GetComponent<SpriteRenderer>().color = Color.blue;
        GameObject temp = null;
        int x = 0;
        int xStart = x;
        int y = 4;
        int yStart = y;


        int end = 4;
        while (y >= 0)
        {
            while (x <= end)
            {
                temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];
                temp.GetComponent<SpriteRenderer>().color = Color.blue;
                if (temp.tag == "Player")
                    temp.GetComponent<SpriteRenderer>().color = Color.yellow;
                //Debug.Log(x + " BLUE " + y);
                x++;
                y++;
            }
            end++;
            x = 0;
            y = yStart - 1;
            yStart = y;
        }


        x = 1;
        xStart = x;
        y = 0;
        while (x <= 4)
        {
            while (x <= 8)
            {
                temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];
                temp.GetComponent<SpriteRenderer>().color = Color.blue;
                if (temp.tag == "Player")
                    temp.GetComponent<SpriteRenderer>().color = Color.yellow;
                //Debug.Log(x + " " + y);
                x++;
                y++;
            }
            x = xStart + 1;
            xStart = x;
            y = 0;
        }
    }


    private void CheckIfWin()
    {
        int x = 0;
        int y = 0;
        
        // Horizontal
        for(int i = 0; i < PopulateField.instance.gridHeight; i++)
        {
            CountTiles(x, i, 1, 0, Color.yellow);
        }

        // Vertical
        for (int j = 0; j < PopulateField.instance.gridHeight; j++)
        {
            CountTiles(j, y, 0, 1, Color.green);
        }

        // Diagonal top-left to bottom-right
        for (int k = 0; k < PopulateField.instance.gridHeight; k++)
        {
            CountTiles(k, y, 1, 1, Color.blue);
        }

        // Diagonal top-left to bottom-right
        for (int l = 0; l < PopulateField.instance.gridHeight; l++)
        {
            CountTiles(0, l, 1, 1, Color.blue);
        }

        // Diagonal bottom-left to top-right
        for (int m = 0; m < PopulateField.instance.gridHeight; m++)
        {
            CountTiles(m, PopulateField.instance.gridHeight - 1, 1, -1, Color.cyan);
        }
        
        // Diagonal bottom-left to top-right
        for (int n = 0; n < PopulateField.instance.gridHeight; n++)
        {
            CountTiles(0, PopulateField.instance.gridHeight - (1 + n), 1, -1, Color.cyan);
        }



    }

    private void CountTiles(int startX, int startY, int dx, int dy, Color color)
    {
        int count = 0;
        int x = startX /*+ dx*/;
        int y = startY /*+ dy*/;
        start = new Vector3(0, 0, 0);
        end = new Vector3(0, 0, 0);

        while (x >= 0 && x < PopulateField.instance.gridWidth && y >= 0 && y < PopulateField.instance.gridHeight /*&&*/
               //PopulateField.instance.gameField[x, y] != null &&
               //PopulateField.instance.gameField[x, y].CompareTag("Player") /*&&
               /*gameField[x, y].GetComponent<MakeSquareRed>().gameObject.tag == player*/)
        {
            GameObject temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];
            temp.GetComponent<SpriteRenderer>().color = color;

            tilesInRow.Add(temp);

            count++;
            x += dx;
            y += dy;
        }

        int counter = 0;

        foreach(GameObject tile in tilesInRow)
        {
            if (tile.tag == "Player")
            {
                tile.GetComponent<SpriteRenderer>().color = Color.red;
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
