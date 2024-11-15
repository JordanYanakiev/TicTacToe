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
        CheckDiagonalRight();
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
                Debug.Log(x + " " + y);
                x--;
                y++;
            }
            x = xStart + 1;
            xStart = x;
            y = 0;
        }
    }



}
