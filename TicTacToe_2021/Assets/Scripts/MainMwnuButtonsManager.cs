using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMwnuButtonsManager : MonoBehaviour
{
    [SerializeField] private Button x3Button;
    [SerializeField] private Button x4Button;
    [SerializeField] private Button x5Button;
    [SerializeField] private Button x9Button;
    [SerializeField] public int gridSize;


    // Start is called before the first frame update
    void Start()
    {
        x3Button.onClick.AddListener(() => SelectLevel(x3Button.name));
        x4Button.onClick.AddListener(() => SelectLevel(x4Button.name));
        x5Button.onClick.AddListener(() => SelectLevel(x5Button.name));
        x9Button.onClick.AddListener(() => SelectLevel(x9Button.name));
        
    }


    private void SelectLevel(string buttonName)
    {
        switch (buttonName)
        {
            case "3x3Button":
                gridSize = 3;
                break;
            case "4x4Button":
                gridSize = 4;
                break;
            case "5x5Button":
                gridSize = 5;
                break;
            case "9x9Button":
                gridSize = 9;
                break;
        }
        GameManager.instance.squareMatrixSize = gridSize;
        SceneManager.LoadScene("SampleScene");

    }


}
