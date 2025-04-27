using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject StartMenu;
    public GameObject EndMenu;
    
   

    private bool _gameStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0; // Pause the game at start
        StartMenu.SetActive(true);
        EndMenu.SetActive(false);
    }

    public void StartGame()
    {
        _gameStarted = true;
        Time.timeScale = 1; // Resume the game
        StartMenu.SetActive(false);
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0; // Pause the game
        EndMenu.SetActive(true);
    }



    public void RestartGame()
    {
        Time.timeScale = 0; // Pause the game at start
        StartMenu.SetActive(true);
        EndMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

