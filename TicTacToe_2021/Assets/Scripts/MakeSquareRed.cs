using UnityEngine;

public class MakeSquareRed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().color != Color.red) 
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red; 
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
