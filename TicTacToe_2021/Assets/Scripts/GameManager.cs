//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    [SerializeField] public int squareMatrixSize;

//    #region SINGLETON
//    public static GameManager _instance;
//    public static GameManager instance
//    {
//        get
//        {
//            if(_instance == null)
//            {
//                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
//            }
//            return _instance;
//        }
//        set { _instance = value; }
//    }
//    #endregion

//    // Start is called before the first frame update
//    void Start()
//    {
//        DontDestroyOnLoad(transform.gameObject);
//    }
//}




using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance for global access

    [SerializeField] private string playerX = "X"; // Player X identifier
    [SerializeField] private string playerO = "O"; // Player O identifier
    public string currentPlayer; // Keeps track of whose turn it is

    [SerializeField] private PopulateField populateField; // Reference to PopulateField script
    public int squareMatrixSize = 3; // Size of the grid (e.g., 3x3, 4x4, etc.)

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // Optional: Persist GameManager across scenes
    }

    private void Start()
    {
        currentPlayer = playerX; // Start with Player X
    }

    /// <summary>
    /// Switches to the next player's turn.
    /// </summary>
    public void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == playerX) ? playerO : playerX;
        Debug.Log($"It's now {currentPlayer}'s turn.");
    }

    /// <summary>
    /// Ends the game and displays the winner.
    /// </summary>
    public void EndGame(string winner)
    {
        Debug.Log($"{winner} wins the game!");

        // Optional: Display a UI message or restart the game
        // Example: Show win screen or restart the game
        // UIManager.instance.ShowWinScreen(winner);
        RestartGame();
    }

    /// <summary>
    /// Restarts the game by reloading the grid and resetting the state.
    /// </summary>
    public void RestartGame()
    {
        Debug.Log("Restarting the game...");

        // Reset the grid and start a new game
        currentPlayer = playerX;

        if (populateField != null)
        {
            foreach (Transform child in populateField.transform)
            {
                Destroy(child.gameObject);
            }

            populateField.Start(); // Regenerate the grid
        }
    }
}
