using UnityEngine;

public class Tile : MonoBehaviour
{
    //private bool isClaimed = false;

    //private void OnMouseDown()
    //{
    //    if (!isClaimed)
    //    {
    //        isClaimed = true;

    //        // Set the tag to "Player" and visually assign it to the current player
    //        gameObject.tag = "Player";
    //        AssignToPlayer(GameManager.instance.currentPlayer);

    //        // Check for a win condition
    //        PopulateField populateField = FindObjectOfType<PopulateField>();
    //        var winningTiles = populateField.CheckWinCondition(
    //            (int)transform.localPosition.x,
    //            (int)transform.localPosition.y,
    //            GameManager.instance.currentPlayer);

    //        if (winningTiles != null)
    //        {
    //            Debug.Log($"{GameManager.instance.currentPlayer} wins!");
    //            populateField.HighlightWinningTiles(winningTiles); // Highlight the win streak
    //        }
    //        else
    //        {
    //            GameManager.instance.SwitchPlayer();
    //        }
    //    }
    //}

    //private void AssignToPlayer(string player)
    //{
    //    Debug.Log($"Tile claimed by: {player}");
    //    if (player == "X")
    //    {
    //        GetComponent<SpriteRenderer>().color = Color.red;
    //    }
    //    else if (player == "O")
    //    {
    //        GetComponent<SpriteRenderer>().color = Color.blue;
    //    }
    //}
}
