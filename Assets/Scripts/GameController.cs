using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    bool paused = false;
    bool gameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("GameController arrancado");
    }

     void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) { Debug.Log("No hay teclado"); return; }

        if (kb.pKey.wasPressedThisFrame) Debug.Log("P pulsada");

        if ((kb.escapeKey.wasPressedThisFrame || kb.pKey.wasPressedThisFrame) && !gameOver)
            TogglePause();

        if (kb.rKey.wasPressedThisFrame && (paused || gameOver))
            Restart();
    }

    void TogglePause()
    {
        paused = !paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f; // 0 = el tiempo se congela
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Restart()
    {
        Time.timeScale = 1f; // importante: timeScale no se reinicia solo al cambiar de escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}