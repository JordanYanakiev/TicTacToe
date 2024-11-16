using UnityEngine;

public class MakeSquareRed : MonoBehaviour
{
    public GameObject gameFieldPanel;
    public string player;
    public int xPos;
    public int yPos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        PopulateField.instance.CheckWinCondition(xPos, yPos, player);

        //Chech both diagonals for 5 consecutive tiles
        CheckDiagonalRight();
        CheckDiagonalLeft();
    }

    private void CheckDiagonalRight()
    {
        GameObject tile = gameFieldPanel.GetComponent<PopulateField>().GameField[4, 0];
        tile.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameObject temp = null;
        int x = 4;
        int xStart = x;
        int y = 0;
        int yStart = y;

        while (x <= 8)
        {
            while (x >= 0)
            {
                temp = gameFieldPanel.GetComponent<PopulateField>().GameField[x, y];
                temp.GetComponent<SpriteRenderer>().color = Color.cyan;
                if (temp.tag == "Player")
                    temp.GetComponent<SpriteRenderer>().color = Color.yellow;
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

}
